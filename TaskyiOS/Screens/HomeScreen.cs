using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using Tasky.Core;
using Tasky.ApplicationLayer;

namespace Tasky.Screens {

	/// <summary>
	/// A UITableViewController that uses MonoTouch.Dialog - displays the list of Tasks
	/// </summary>
	public class HomeScreen : DialogViewController {
		// 
		List<Task> tasks;
		List<SQLLiteDB> sqllitedbs;
		
		// MonoTouch.Dialog individual TaskDetails view (uses /AL/TaskDialog.cs wrapper class)
		BindingContext context;
		TaskDialog taskDialog;
		Task currentTask;
		DialogViewController detailsScreen;

		BindingContext contextSQL;
		SQLLiteDBDialog sqlliteDialog;
		SQLLiteDB currentsqllitedb;
		DialogViewController detailsSQLScreen;

		public HomeScreen () : base (UITableViewStyle.Plain, null)
		{
			Initialize ();
		}
		
		protected void Initialize()
		{
			NavigationItem.SetRightBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Add), false);
			NavigationItem.SetLeftBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Play), false);


			NavigationItem.RightBarButtonItem.Clicked += (sender, e) => { ShowTaskDetails(new Task()); };
			NavigationItem.LeftBarButtonItem.Clicked += (sender, e) => 
			{ 
				ShowSQLDBDetails(new SQLLiteDB()); 
			};
		}

		protected void ShowSQLDBDetails(SQLLiteDB sqllitedb)
		{
			currentsqllitedb = sqllitedb;
			sqlliteDialog = new SQLLiteDBDialog (sqllitedb);
			contextSQL = new BindingContext (this, sqlliteDialog, "SQL Lite Details");
			detailsSQLScreen = new DialogViewController (contextSQL.Root, true);
			ActivateController(detailsSQLScreen);
		}
		
		public void CreateDatabase()
		{
			contextSQL.Fetch (); // re-populates with updated values
			currentsqllitedb.UserName = sqlliteDialog.UserName;
			TaskManager.SaveTaskDB(currentsqllitedb);

			var person = new Person { FirstName = "John " + DateTime.Now.Ticks, LastName = "Doe"};
			using (var db = new SQLite.SQLiteConnection(_pathToDatabase ))
			{
				db.Insert(person);
			}

			NavigationController.PopViewControllerAnimated (true);
		}

		#region TASK
		protected void ShowTaskDetails(Task task)
		{
			currentTask = task;
			taskDialog = new TaskDialog (task);
			context = new BindingContext (this, taskDialog, "Task Details");
			detailsScreen = new DialogViewController (context.Root, true);
			ActivateController(detailsScreen);
		}
		public void SaveTask()
		{
			context.Fetch (); // re-populates with updated values
			currentTask.Name = taskDialog.Name;
			currentTask.Notes = taskDialog.Notes;
			currentTask.Done = taskDialog.Done;
			TaskManager.SaveTask(currentTask);
			NavigationController.PopViewControllerAnimated (true);
		}
		public void DeleteTask ()
		{
			if (currentTask.ID >= 0)
				TaskManager.DeleteTask (currentTask.ID);
			NavigationController.PopViewControllerAnimated (true);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			
			// reload/refresh
			PopulateTable();			
		}
		
		protected void PopulateTable()
		{
			tasks = TaskManager.GetTasks().ToList ();
			Root = new RootElement("Tasky") {
				new Section() {
					from t in tasks
						select (Element) new CheckboxElement((t.Name==""?"<new task>":t.Name), t.Done)
				}
			}; 
		}
		public override void Selected (MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var task = tasks[indexPath.Row];
			ShowTaskDetails(task);
		}
		public override Source CreateSizingSource (bool unevenRows)
		{
			return new EditingSource (this);
		}
		public void DeleteTaskRow(int rowId)
		{
			TaskManager.DeleteTask(tasks[rowId].ID);
		}
		#endregion
	}
}