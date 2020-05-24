using AutoMapper;
using Powerfull.BLL.Models;
using Powerfull.BLL.Services;
using PowerfullTool.Automap;
using PowerfullTool.Notifieds;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerfullTool.Services
{
	public class LoanApplicationNotifiedService
	{
		private readonly IMapper _mapper;
		private readonly LoanApplicationService _loanApplicationService;


		public LoanApplicationNotifiedService(
			LoanApplicationService loanApplicationService)
		{
			this._mapper = PowerfullToolAutomapper.GetAutomapper();
			this._loanApplicationService = loanApplicationService;
		}


		public IEnumerable<LoanApplicationNotified> GetAll()
		{
			IEnumerable<LoanApplicationBll> loanApplicationBlls = this._loanApplicationService.GetAll();
			IEnumerable<LoanApplicationNotified> loanApplicationNotifieds = loanApplicationBlls
				.Select(x => this._mapper.Map<LoanApplicationNotified>(x))
				.ToList();
			return loanApplicationNotifieds;
		}

		public async Task<LoanApplicationNotified> CreateAsync(LoanApplicationNotified loanApplicationNotified)
		{
			LoanApplicationBll modelBll = this._mapper.Map<LoanApplicationBll>(loanApplicationNotified);

			LoanApplicationBll createdModel = await this._loanApplicationService.CreateAsync(modelBll);

			LoanApplicationNotified returnModel = this._mapper.Map<LoanApplicationNotified>(createdModel);
			return returnModel;
		}

		public async Task<LoanApplicationNotified> EditAsync(LoanApplicationNotified loanApplicationNotified)
		{
			LoanApplicationBll modelBll = this._mapper.Map<LoanApplicationBll>(loanApplicationNotified);

			LoanApplicationBll createdModel = await this._loanApplicationService.EditAsync(modelBll);

			LoanApplicationNotified returnModel = this._mapper.Map<LoanApplicationNotified>(createdModel);
			return returnModel;
		}

		public async Task<LoanApplicationNotified> RemoveAsync(LoanApplicationNotified loanApplicationNotified)
		{
			LoanApplicationBll modelBll = this._mapper.Map<LoanApplicationBll>(loanApplicationNotified);

			LoanApplicationBll removedModel = await this._loanApplicationService.RemoveAsync(modelBll);

			LoanApplicationNotified returnModel = this._mapper.Map<LoanApplicationNotified>(removedModel);
			return returnModel;
		}
	}
}
