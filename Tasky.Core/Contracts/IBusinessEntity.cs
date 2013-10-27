using System;

namespace Tasky.Core.Contracts {
	public interface IBusinessEntity {
		int ID { get; set; }
	}

	public interface ISQLBusinessEntity {
		int UserID { get; set; }
		string UserName { get; set; }
	}
}

