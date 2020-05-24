using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PowerfullTool.Notifieds
{
	public class LoanApplicationNotified : INotifyPropertyChanged
	{
		private long _applicationNumber;
		private ApplicationStatusEnumNotified _applicationStatus;
		private string _applicantDetails;
		private decimal _amountRequested;
		private decimal _amountGranted;
		private DateTime _dateOfSubmission;
		private DateTime? _deletedDate;


		public long ApplicationNumber
		{
			get { return this._applicationNumber; }
			set
			{
				this._applicationNumber = value;
				this.OnPropertyChanged(nameof(this.ApplicationNumber));
			}
		}
		public ApplicationStatusEnumNotified ApplicationStatus
		{
			get { return this._applicationStatus; }
			set
			{
				this._applicationStatus = value;
				this.OnPropertyChanged(nameof(this.ApplicationStatus));
			}
		}
		public string ApplicantDetails
		{
			get { return this._applicantDetails; }
			set
			{
				this._applicantDetails = value;
				this.OnPropertyChanged(nameof(this.ApplicantDetails));
			}
		}
		public decimal AmountRequested
		{
			get { return this._amountRequested; }
			set
			{
				this._amountRequested = value;
				this.OnPropertyChanged(nameof(this.AmountRequested));
			}
		}
		public decimal AmountGranted
		{
			get { return this._amountGranted; }
			set
			{
				this._amountGranted = value;
				this.OnPropertyChanged(nameof(this.AmountGranted));
			}
		}
		public DateTime DateOfSubmission
		{
			get { return this._dateOfSubmission; }
			set
			{
				this._dateOfSubmission = value;
				this.OnPropertyChanged(nameof(this.DateOfSubmission));
			}
		}
		public DateTime? DeletedDate
		{
			get { return this._deletedDate; }
			set
			{
				this._deletedDate = value;
				this.OnPropertyChanged(nameof(this.DeletedDate));
			}
		}


		public event PropertyChangedEventHandler PropertyChanged = (_, __) => { };
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
