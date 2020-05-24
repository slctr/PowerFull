using FluentNHibernate.Mapping;
using Powerfull.Dal.Generators;
using Powerfull.Dal.Models;

namespace Powerfull.Dal.Maps
{
	public class LoanApplicationModelMap : ClassMap<LoanApplicationModel>
	{
		private const string c_TableName = "LoanApplicationModel";

		public LoanApplicationModelMap()
		{
			//this.Id(x => x.Id).GeneratedBy.Increment();
			this.Id(x => x.Id)
				.GeneratedBy.Custom<StartApplicantNumberIdGenerator>();
			this.Map(x => x.ApplicationStatus);
			this.Map(x => x.ApplicantDetails);
			this.Map(x => x.AmountRequested);
			this.Map(x => x.AmountGranted);
			this.Map(x => x.DateOfSubmission);
			this.Map(x => x.DeletedDate);
			this.Table(c_TableName);
		}
	}
}
