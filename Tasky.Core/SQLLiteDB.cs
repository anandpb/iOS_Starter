using System;
using Mono.Data.Sqlite;
using Tasky.Core.Contracts;

namespace Tasky.Core
{
	public class SQLLiteDB : ISQLBusinessEntity
	{
		public SQLLiteDB ()
		{
		}

		public int UserID { get; set; }
		public string UserName { get; set; }
	}

	public class Person
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}

