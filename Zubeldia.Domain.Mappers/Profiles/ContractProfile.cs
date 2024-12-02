namespace Zubeldia.Domain.Mappers.Profiles
{
    using System.IO;
    using AutoMapper;
    using Grogu.Domain;
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Domain.Dtos.Contract.GetContractDto;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Entities.Base;

    public class ContractProfile : Profile
    {
        public ContractProfile()
        {
            CreateMap<CreateContractRequest, Contract>()
                .ForMember(dest => dest.File, opt =>
                    opt.MapFrom((src, dest) =>
                    {
                        if (src.File == null || src.File.Length == 0)
                        {
                            return null;
                        }
                        using var streamReader = new StreamReader(src.File.OpenReadStream());
                        return streamReader.ReadToEndAsync().Result;
                    }))
                .ForMember(dest => dest.StartDate, opt => opt.Ignore())
                .ForMember(dest => dest.EndDate, opt => opt.Ignore());

            CreateMap<SearchResultPage<Contract>, SearchResultPage<GetContractsDto>>();

            CreateMap<Contract, GetContractsDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.DisplayValue()));

            CreateMap<Contract, GetContractDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.DisplayValue()))
                .ForPath(dest => dest.Player, opt => opt.MapFrom(src => new GetContractPlayerDto
                {
                    FirstName = src.Player.FirstName,
                    LastName = src.Player.LastName,
                    DocumentNumber = src.Player.DocumentNumber,
                }));

            CreateMap<ContractSalary, GetContractSalaryDto>()
                .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.Currency.Code));

            CreateMap<ContractObjective, GetContractObjectiveDto>()
                .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.Currency.Code));
        }
    }
}