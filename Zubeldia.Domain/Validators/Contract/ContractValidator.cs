namespace Zubeldia.Domain.Validators.Contract
{
    using FluentValidation;
    using Zubeldia.Commons;
    using Zubeldia.Commons.Constants;
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Dtos.Models.User;

    public class ContractValidator : AbstractValidator<CreateContractRequest>
    {
        public ContractValidator()
        {
            RuleFor(x => x.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField(nameof(CreateContractRequest.Title)))
                .MaximumLength(50)
                .WithMessage(MessageUtils.ExceedMaximumLength(nameof(CreateContractRequest.Title), 50));

            RuleFor(x => x.Type)
              .Cascade(CascadeMode.Stop)
              .NotEmpty()
              .WithMessage(MessageUtils.MandatoryField(nameof(CreateContractRequest.Type)))
              .IsInEnum()
              .WithMessage(MessageUtils.NotExistInEnum(nameof(CreateContractRequest.Type)));

            RuleFor(x => x.PlayerId)
              .NotEmpty()
              .WithMessage(MessageUtils.MandatoryField(nameof(CreateContractRequest.PlayerId)));

            // TODO: Validar archivo
        }
    }
}
