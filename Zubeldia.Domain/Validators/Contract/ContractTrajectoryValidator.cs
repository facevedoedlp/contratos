namespace Zubeldia.Domain.Validators.Contract
{
    using FluentValidation;
    using Zubeldia.Commons;
    using Zubeldia.Domain.Dtos.Contract;

    public class ContractTrajectoryValidator : AbstractValidator<CreateContractTrajectoryRequest>
    {
        public ContractTrajectoryValidator()
        {
            RuleFor(x => x.PaymentDate)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Fecha de pago"));

            RuleFor(x => x.Amount)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Monto"))
                .Must(x => x > 0)
                .WithMessage(MessageUtils.MustBeGreaterThanZero("Monto"));

            RuleFor(x => x.CurrencyId)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Moneda"));

            RuleFor(x => x.ExchangeRate)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Tipo de cambio"))
                .Must(x => x > 0)
                .WithMessage(MessageUtils.MustBeGreaterThanZero("Tipo de cambio"));
        }
    }
}