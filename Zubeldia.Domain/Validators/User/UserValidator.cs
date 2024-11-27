namespace Zubeldia.Dtos.Validatiors.User
{
    using FluentValidation;
    using Zubeldia.Commons;
    using Zubeldia.Commons.Constants;
    using Zubeldia.Domain.Interfaces.Providers;
    using Zubeldia.Dtos.Models.User;

    public class UserValidator : AbstractValidator<RegisterUserRequest>
    {
        private readonly IUserDao userDao;

        public UserValidator(IUserDao userDao)
        {
            this.userDao = userDao;

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField(nameof(RegisterUserRequest.Email)))
                .EmailAddress()
                .WithMessage(MessageUtils.InvalidValue(nameof(RegisterUserRequest.Email)))
                .Must(email => email.EndsWith(ValidationConstants.DomainEmail))
                .WithMessage(MessageUtils.MustContainEmailDomain(nameof(RegisterUserRequest.Email)))
                .MustAsync(async (rootObject, dealId, context, cancellationToken) => !await this.userDao.IsEmailTakenAsync(rootObject.Email))
                .WithMessage(MessageUtils.AlreadyExists(nameof(RegisterUserRequest.Email)));

            RuleFor(x => x.FirstName)
              .NotEmpty()
              .WithMessage(MessageUtils.MandatoryField(nameof(RegisterUserRequest.FirstName)));

            RuleFor(x => x.LastName)
              .NotEmpty()
              .WithMessage(MessageUtils.MandatoryField(nameof(RegisterUserRequest.LastName)));

            RuleFor(x => x.Password)
              .NotEmpty()
              .WithMessage(MessageUtils.MandatoryField(nameof(RegisterUserRequest.Password)))
              .MinimumLength(8)
              .WithMessage(MessageUtils.MustContainMinLength(nameof(RegisterUserRequest.Password), 8))
              .Matches(ValidationConstants.UppercasePattern)
              .WithMessage(MessageUtils.MustContainUppercase(nameof(RegisterUserRequest.Password)))
              .Matches(ValidationConstants.SpecialCharactersPattern)
              .WithMessage(MessageUtils.MustContainSpecialChar(nameof(RegisterUserRequest.Password)))
              .Matches(ValidationConstants.NumberPattern)
              .WithMessage(MessageUtils.MustContainNumber(nameof(RegisterUserRequest.Password)));
        }
    }
}
