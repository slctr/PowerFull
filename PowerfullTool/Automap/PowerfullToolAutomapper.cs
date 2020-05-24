using AutoMapper;
using Powerfull.BLL.Models;
using PowerfullTool.Notifieds;

namespace PowerfullTool.Automap
{
	public static class PowerfullToolAutomapper
	{
		public static IMapper GetAutomapper()
		{
			IConfigurationProvider configurationProvider = new MapperConfiguration(
				cfg =>
				{
					cfg.CreateMap<LoanApplicationBll, LoanApplicationNotified>()
						.ForMember(dest => dest.ApplicationNumber,
							opt => opt.MapFrom(src => src.ApplicationNumber))
						.ForMember(dest => dest.ApplicationStatus,
							opt => opt.MapFrom(src => src.ApplicationStatus))
						.ForMember(dest => dest.ApplicantDetails,
							opt => opt.MapFrom(src => src.ApplicantDetails))
						.ForMember(dest => dest.AmountRequested,
							opt => opt.MapFrom(src => src.AmountRequested))
						.ForMember(dest => dest.AmountGranted,
							opt => opt.MapFrom(src => src.AmountGranted))
						.ForMember(dest => dest.DateOfSubmission,
							opt => opt.MapFrom(src => src.DateOfSubmission))
						.ReverseMap();

					cfg.CreateMap<LoanApplicationNotified, LoanApplicationNotified>()
						.ForMember(dest => dest.ApplicationNumber,
							opt => opt.MapFrom(src => src.ApplicationNumber))
						.ForMember(dest => dest.ApplicationStatus,
							opt => opt.MapFrom(src => src.ApplicationStatus))
						.ForMember(dest => dest.ApplicantDetails,
							opt => opt.MapFrom(src => src.ApplicantDetails))
						.ForMember(dest => dest.AmountRequested,
							opt => opt.MapFrom(src => src.AmountRequested))
						.ForMember(dest => dest.AmountGranted,
							opt => opt.MapFrom(src => src.AmountGranted))
						.ForMember(dest => dest.DateOfSubmission,
							opt => opt.MapFrom(src => src.DateOfSubmission))
						.ReverseMap();
				});

			IMapper mapper = new Mapper(configurationProvider);

			return mapper;
		}
	}
}
