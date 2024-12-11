namespace Zubeldia.Domain.Validators.Contract
{
    using FluentValidation;
    using Zubeldia.Commons;
    using Zubeldia.Domain.Dtos.Contract;

    public class ContractSalaryValidator : AbstractValidator<CreateContractSalaryRequest>
    {
        public ContractSalaryValidator()
        {
            RuleFor(x => x.Amount)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Monto"))
                .GreaterThan(0)
                .WithMessage(MessageUtils.MustBeGreaterThanZero("Monto"));

            RuleFor(x => x.CurrencyId)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Moneda"));

            RuleFor(x => x.ExchangeRate)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Tipo de cambio"))
                .GreaterThan(0)
                .WithMessage(MessageUtils.MustBeGreaterThanZero("Tipo de cambio"));

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Fecha de inicio"))
                .LessThanOrEqualTo(x => x.EndDate)
                .When(x => x.EndDate != default)
                .WithMessage(MessageUtils.StartDateMustBeLessThanEndDate());

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Fecha de finalización"))
                .GreaterThan(x => x.StartDate)
                .WithMessage(MessageUtils.DateMustBeGreaterThan("Fecha de finalización"));
        }
    }
}