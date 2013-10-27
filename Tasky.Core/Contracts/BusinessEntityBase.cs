using System;
//using Tasky.Core.SQLite;

namespace Tasky.Core.Contracts {
	/// <summary>
	/// Business entity base class. Provides the ID property.
	/// </summary>
	public abstract class BusinessEntityBase : IBusinessEntity {
		public BusinessEntityBase ()
		{
		}
		
		/// <summary>
		/// Gets or sets the Database ID.
		/// </summary>
//		[PrimaryKey, AutoIncrement]
        public int ID { get; set; }
	}

	public abstract class SQLBusinessEntityBase : ISQLBusinessEntity {
		public SQLBusinessEntityBase ()
		{
		}

		/// <summary>
		/// Gets or sets the Database ID.
		/// </summary>
		//		[PrimaryKey, AutoIncrement]
		public int UserID { get; set; }
		public string UserName { get; set; }
	}
}

