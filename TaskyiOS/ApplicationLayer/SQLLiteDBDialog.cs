using System;
using MonoTouch.UIKit;
using Tasky.Core;
using MonoTouch.Dialog;

namespace Tasky.ApplicationLayer
{
	public class SQLLiteDBDialog
	{
		public SQLLiteDBDialog (SQLLiteDB sqllitedb)
		{
			UserName = sqllitedb.UserName;
		}

		
		[Entry("User Name")]
		public string UserName { get; set; }

		
		[Section ("DATABASE")]
		[OnTap ("CreateDatabase")]
		[Alignment (UITextAlignment.Left)]
		public string CreateDatabase;
	}
}

