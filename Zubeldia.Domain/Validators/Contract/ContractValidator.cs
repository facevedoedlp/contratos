namespace Zubeldia.Domain.Validators.Contract
{
    using FluentValidation;
    using Zubeldia.Commons;
    using Zubeldia.Domain.Dtos.Contract;

    public class ContractValidator : AbstractValidator<CreateContractRequest>
    {
        public ContractValidator()
        {
            RuleFor(x => x.Type)
              .Cascade(CascadeMode.Stop)
              .IsInEnum()
              .WithMessage(MessageUtils.NotExistInEnum("Tipo de contrato"));

            RuleFor(x => x.PlayerId)
              .NotEmpty()
              .WithMessage(MessageUtils.MandatoryField("Jugador"));

            RuleFor(x => x.StartDate)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Fecha de inicio"))
                .LessThanOrEqualTo(x => x.EndDate)
                .When(x => x.EndDate != null)
                .WithMessage(MessageUtils.StartDateMustBeLessThanEndDate());

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Fecha de finalizacion"));

            RuleFor(x => x.File)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Archivo"));

            RuleForEach(x => x.Objectives)
                .NotEmpty()
                .SetValidator(m => new ContractObjectiveValidator());

            RuleForEach(x => x.Salaries)
                .NotEmpty()
                .SetValidator(m => new ContractSalaryValidator());
        }
    }
}