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
                .WithMessage(MessageUtils.MandatoryField("Fecha de finalización"));

            When(x => x.TotalRecognition.HasValue, () =>
            {
                RuleFor(x => x.TotalRecognition)
                    .GreaterThan(0)
                    .WithMessage(MessageUtils.MustBeGreaterThanZero("Reconocimiento total"));

                RuleFor(x => x.InstallmentsCount)
                    .NotEmpty()
                    .WithMessage(MessageUtils.MandatoryField("Cantidad de cuotas"))
                    .GreaterThan(0)
                    .WithMessage(MessageUtils.MustBeGreaterThanZero("Cantidad de cuotas"));

                RuleFor(x => x.InstallmentRecognition)
                    .NotEmpty()
                    .WithMessage(MessageUtils.MandatoryField("Reconocimiento por cuota"))
                    .GreaterThan(0)
                    .WithMessage(MessageUtils.MustBeGreaterThanZero("Reconocimiento por cuota"));
            });
        }
    }
}