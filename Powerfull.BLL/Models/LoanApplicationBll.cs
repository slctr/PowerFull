using System;
using System.Xml.Serialization;

namespace Powerfull.BLL.Models
{
	[Serializable]
	public class LoanApplicationBll
	{
		public virtual long ApplicationNumber { get; set; }

		[XmlIgnore]
		public virtual ApplicationStatusEnumBll ApplicationStatus { get; set; }

		public virtual string ApplicantDetails { get; set; }

		public virtual decimal AmountRequested { get; set; }

		public virtual decimal AmountGranted { get; set; }

		public virtual DateTime DateOfSubmission { get; set; }

		[XmlIgnore]
		public virtual DateTime? DeletedDate { get; set; }


		[XmlElement("ApplicationStatus")]
		public string ApplicationStatusString
		{
			get { return Enum.GetName(typeof(ApplicationStatusEnumBll), this.ApplicationStatus); }
			set { this.ApplicationStatus = (ApplicationStatusEnumBll)Enum.Parse(typeof(ApplicationStatusEnumBll), value); }
		}
	}
}
