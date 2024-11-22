namespace Zubeldia.Domain.Mappers.Profiles
{
    using System.IO;
    using AutoMapper;
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Domain.Entities;

    public class ContractProfile : Profile
    {
        public ContractProfile()
        {
            CreateMap<CreateContractRequest, Contract>()
                .ForMember(dest => dest.File, opt =>
                {
                    opt.MapFrom((src, dest) =>
                    {
                        if (src.File == null || src.File.Length == 0)
                        {
                            return null;
                        }

                        using var memoryStream = new MemoryStream();
                        src.File.CopyTo(memoryStream);
                        return memoryStream.ToArray();
                    });
                })
                .ForMember(dest => dest.StartDate, opt => opt.Ignore())
                .ForMember(dest => dest.EndDate, opt => opt.Ignore());
        }
    }
}