using System;

namespace Powerfull.Dal.Models
{
	[Serializable]
	public class LoanApplicationModel : IEntity<long>, ISoftDeletable
	{
		public virtual long Id { get; set; }

		public virtual ApplicationStatusEnum ApplicationStatus { get; set; }

		public virtual string ApplicantDetails { get; set; }

		public virtual decimal AmountRequested { get; set; }

		public virtual decimal AmountGranted { get; set; }

		public virtual DateTime DateOfSubmission { get; set; }

		public virtual DateTime? DeletedDate { get; set; }
	}
}
