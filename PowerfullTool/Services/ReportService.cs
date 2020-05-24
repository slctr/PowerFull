using AutoMapper;
using Powerfull.BLL.Models;
using Powerfull.BLL.Services;
using PowerfullTool.Automap;
using PowerfullTool.Notifieds;
using System.Collections.Generic;
using System.Linq;

namespace PowerfullTool.Services
{
	public class ReportService
	{
		private readonly IMapper _mapper;


		public ReportService()
		{
			this._mapper = PowerfullToolAutomapper.GetAutomapper();
		}


		public string CreateReport(IEnumerable<LoanApplicationNotified> loanApplications)
		{
			LoanApplicationBll[] loanApplicationModels = loanApplications
				.Select(x => this._mapper.Map<LoanApplicationBll>(x))
				.ToArray();

			string reportFileSource = HtmlReportGenerator.Generate(loanApplicationModels);

			return reportFileSource;
		}
	}
}
