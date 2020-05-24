using AutoMapper;
using NHibernate;
using Powerfull.BLL.Automap;
using Powerfull.BLL.Models;
using Powerfull.Dal;
using Powerfull.Dal.Models;
using Powerfull.Dal.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Powerfull.BLL.Services
{
	public class LoanApplicationService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly LoanApplicationRepository _repository;


		public LoanApplicationService()
		{
			this._mapper = PowerfullBLLAutomapper.GetAutomapper();
			ISession session = NHibernateHelper.OpenSession();
			this._unitOfWork = new UnitOfWork(session.BeginTransaction());
			this._repository = new LoanApplicationRepository(session);
		}


		public IEnumerable<LoanApplicationBll> GetAll()
		{
			IEnumerable<LoanApplicationModel> loanApplications = this._repository.GetAll();

			IEnumerable<LoanApplicationBll> loanApplicationBlls = loanApplications
				.Select(x => this._mapper.Map<LoanApplicationBll>(x))
				.ToList();
			return loanApplicationBlls;
		}

		public async Task<LoanApplicationBll> CreateAsync(LoanApplicationBll loanApplication)
		{
			LoanApplicationModel model = this._mapper.Map<LoanApplicationModel>(loanApplication);

			LoanApplicationModel updatedModel = await this._repository.CreateAsync(model);
			await this._unitOfWork.CommitAsync();

			LoanApplicationBll returnModel = this._mapper.Map<LoanApplicationBll>(updatedModel);
			return returnModel;
		}

		public async Task<LoanApplicationBll> EditAsync(LoanApplicationBll loanApplication)
		{
			LoanApplicationModel model = this._mapper.Map<LoanApplicationModel>(loanApplication);

			LoanApplicationModel updatedModel = await this._repository.UpdateAsync(model);
			await this._unitOfWork.CommitAsync();

			LoanApplicationBll returnModel = this._mapper.Map<LoanApplicationBll>(updatedModel);
			return returnModel;
		}

		public async Task<LoanApplicationBll> RemoveAsync(LoanApplicationBll loanApplication)
		{
			LoanApplicationModel model = this._mapper.Map<LoanApplicationModel>(loanApplication);

			LoanApplicationModel deletedModel = this._repository.SoftDelete(model);
			await this._unitOfWork.CommitAsync();

			LoanApplicationBll returnModel = this._mapper.Map<LoanApplicationBll>(deletedModel);
			return returnModel;
		}
	}
}
