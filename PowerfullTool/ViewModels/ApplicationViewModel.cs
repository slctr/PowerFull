using AutoMapper;
using PowerfullTool.Automap;
using PowerfullTool.Commands;
using PowerfullTool.DI;
using PowerfullTool.Notifieds;
using PowerfullTool.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace PowerfullTool.ViewModels
{
	public class ApplicationViewModel : INotifyPropertyChanged
	{
		private readonly IMapper _mapper;

		private LoanApplicationNotified _editLoanApplication;
		private LoanApplicationNotified _selectedLoanApplication;
		private LoanApplicationNotified _newLoanApplication;

		public ObservableCollection<LoanApplicationNotified> LoanApplications { get; set; }
		public LoanApplicationNotified SelectedLoanApplication
		{
			get { return this._selectedLoanApplication; }
			set
			{
				this._selectedLoanApplication = value;
				this.OnPropertyChanged(nameof(this.SelectedLoanApplication));
			}
		}
		public LoanApplicationNotified NewLoanApplication
		{
			get { return this._newLoanApplication; }
			set
			{
				this._newLoanApplication = value;
				this.OnPropertyChanged(nameof(this.NewLoanApplication));
			}
		}

		private LoanApplicationNotifiedService _loanApplicationNotifiedService
		{
			get
			{
				return IocKernel.Get<LoanApplicationNotifiedService>();
			}
		}


		public ApplicationViewModel()
		{
			this._mapper = PowerfullToolAutomapper.GetAutomapper();

			IEnumerable<LoanApplicationNotified> loanApplicationNotifieds =
				this._loanApplicationNotifiedService.GetAll();
			this.LoanApplications = new ObservableCollection<LoanApplicationNotified>(loanApplicationNotifieds);

			this._newLoanApplication = new LoanApplicationNotified()
			{
				ApplicationStatus = ApplicationStatusEnumNotified.New,
				DateOfSubmission = DateTime.UtcNow
			};
		}


		public event PropertyChangedEventHandler PropertyChanged = (_, __) => { };
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
		}


		#region Commands

		private RelayCommand<LoanApplicationNotified> _addOrUpdateCommand;
		public RelayCommand<LoanApplicationNotified> AddOrUpdateCommand
		{
			get
			{
				return this._addOrUpdateCommand ??
					(this._addOrUpdateCommand = new RelayCommand<LoanApplicationNotified>(
						async loanApplication =>
						{
							if (this._editLoanApplication != null && this._editLoanApplication.DeletedDate == null)
							{
								// Update priviously selected to edit element
								this._editLoanApplication.ApplicationNumber = loanApplication.ApplicationNumber;
								this._editLoanApplication.ApplicationStatus = loanApplication.ApplicationStatus;
								this._editLoanApplication.ApplicantDetails = loanApplication.ApplicantDetails;
								this._editLoanApplication.AmountRequested = loanApplication.AmountRequested;
								this._editLoanApplication.AmountGranted = loanApplication.AmountGranted;
								this._editLoanApplication.DateOfSubmission = loanApplication.DateOfSubmission;
								this._editLoanApplication = null;

								await this._loanApplicationNotifiedService.EditAsync(loanApplication);
							}
							else
							{
								this.LoanApplications.Insert(0, loanApplication);
								// Case when was clicked edit and remove, need to add as new the element
								loanApplication.ApplicationNumber = default;
								LoanApplicationNotified createdLoanApplication =
									await this._loanApplicationNotifiedService.CreateAsync(loanApplication);

								loanApplication.ApplicationNumber = createdLoanApplication.ApplicationNumber;
							}

							this.NewLoanApplication = new LoanApplicationNotified();
						})
					);
			}
		}

		private RelayCommand<LoanApplicationNotified> _editCommand;
		public RelayCommand<LoanApplicationNotified> EditCommand
		{
			get
			{
				return this._editCommand ??
				  (this._editCommand = new RelayCommand<LoanApplicationNotified>(
					  loanApplication =>
					  {
						  this._editLoanApplication = loanApplication;
						  // Copy element fields
						  this.NewLoanApplication = this._mapper.Map<LoanApplicationNotified>(loanApplication);
					  },
					  loanApplication => loanApplication != null && loanApplication.DeletedDate == null)
				  );
			}
		}

		private RelayCommand<LoanApplicationNotified> _removeCommand;
		public RelayCommand<LoanApplicationNotified> RemoveCommand
		{
			get
			{
				return this._removeCommand ??
					(this._removeCommand = new RelayCommand<LoanApplicationNotified>(
						async loanApplication =>
						{
							LoanApplicationNotified removedLoanApplication;

							try
							{
								removedLoanApplication = await this._loanApplicationNotifiedService.RemoveAsync(loanApplication);
							}
							catch (ArgumentException ex)
							{
								MessageBox.Show(ex.Message);
								return;
							}

							loanApplication.DeletedDate = removedLoanApplication.DeletedDate;
						},
						_ => this.LoanApplications.Count > 0)
					);
			}
		}

		private RelayCommand<IEnumerable<LoanApplicationNotified>> _reportCommand;
		public RelayCommand<IEnumerable<LoanApplicationNotified>> ReportCommand
		{
			get
			{
				return this._reportCommand ??
					(this._reportCommand = new RelayCommand<IEnumerable<LoanApplicationNotified>>(
						loanApplications =>
						{
							ReportService reportService = new ReportService();
							string reportFileSource = reportService.CreateReport(loanApplications);

							MessageBoxResult result = MessageBox.Show($"Report successfully created by path: {reportFileSource}");
							if (result == MessageBoxResult.OK)
							{
								Process.Start(reportFileSource);
							}
						},
						loanApplications => loanApplications.Count() > 0)
					);
			}
		}

		#endregion Commands
	}
}
