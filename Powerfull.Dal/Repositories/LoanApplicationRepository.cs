using NHibernate;
using Powerfull.Dal.Models;

namespace Powerfull.Dal.Repositories
{
	public class LoanApplicationRepository
		: SoftDeleteRepository<LoanApplicationModel, long>
	{
		public LoanApplicationRepository(
			ISession session)
			: base(session)
		{
		}
	}
}
