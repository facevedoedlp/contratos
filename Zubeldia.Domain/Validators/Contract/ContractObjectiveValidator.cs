namespace Zubeldia.Domain.Validators.Contract
{
    using FluentValidation;
    using Zubeldia.Commons;
    using Zubeldia.Domain.Dtos.Contract;

    public class ContractObjectiveValidator : AbstractValidator<CreateContractObjectiveRequest>
    {
        public ContractObjectiveValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Descripción"))
                .MaximumLength(500)
                .WithMessage(MessageUtils.ExceedMaximumLength("Descripción", 500));

            RuleFor(x => x.Amount)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Monto"))
                .GreaterThan(0)
                .WithMessage(MessageUtils.MustBeGreaterThanZero("Monto"));

            RuleFor(x => x.CurrencyId)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Moneda"));

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Fecha de inicio"))
                .LessThanOrEqualTo(x => x.EndDate)
                .When(x => x.EndDate != default)
                .WithMessage(MessageUtils.StartDateMustBeLessThanEndDate());

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Fecha de finalización"));
        }
    }
}