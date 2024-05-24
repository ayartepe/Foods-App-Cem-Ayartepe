using DataAccess.Contexts;

namespace Business.Services.Bases
{
	public abstract class ServiceBase
	{
		protected readonly Db _db;

		protected ServiceBase(Db db)
		{
			_db = db;
		}
	}
}
