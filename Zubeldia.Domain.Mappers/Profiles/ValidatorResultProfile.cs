namespace Zubeldia.Domain.Mappers.Profiles
{
    using System.Diagnostics.CodeAnalysis;
    using AutoMapper;
    using FluentValidation.Results;
    using Zubeldia.Dtos.Models.Commons;

    [ExcludeFromCodeCoverageAttribute]
    public class ValidatorResultProfile : Profile
    {
        public ValidatorResultProfile()
        {
            CreateMap<ValidationResult, ValidatorResultDto>();

            CreateMap<ValidationFailure, Error>();
        }
    }
}
