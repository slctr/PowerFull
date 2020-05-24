using AutoMapper;
using Powerfull.BLL.Models;
using Powerfull.Dal.Models;

namespace Powerfull.BLL.Automap
{
	public static class PowerfullBLLAutomapper
	{
		public static IMapper GetAutomapper()
		{
			IConfigurationProvider configurationProvider = new MapperConfiguration(
				cfg =>
				{
					cfg.CreateMap<LoanApplicationBll, LoanApplicationModel>()
						.ForMember(dest => dest.Id,
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
						.ForMember(dest => dest.DeletedDate,
							opt => opt.MapFrom(src => src.DeletedDate))
						.ReverseMap();
				});

			IMapper mapper = new Mapper(configurationProvider);

			return mapper;
		}
	}
}
