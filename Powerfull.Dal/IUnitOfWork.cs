using NHibernate;
using System;
using System.Threading.Tasks;

namespace Powerfull.Dal
{
	public interface IUnitOfWork : IDisposable
	{
		ITransaction Context { get; }

		void Commit();

		Task CommitAsync();
	}
}
