namespace Zubeldia.Dtos.Validatiors.User
{
    using FluentValidation;
    using Zubeldia.Commons;
    using Zubeldia.Commons.Constants;
    using Zubeldia.Domain.Interfaces.Providers;
    using Zubeldia.Dtos.Models.User;

    public class UserValidator : AbstractValidator<UserDto>
    {
        private readonly IUserDao userDao;

        public UserValidator(IUserDao userDao)
        {
            this.userDao = userDao;

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField(nameof(UserDto.Email)))
                .EmailAddress()
                .WithMessage(MessageUtils.InvalidValue(nameof(UserDto.Email)))
                .Must(email => email.EndsWith(ValidationConstants.DomainEmail))
                .WithMessage(MessageUtils.MustContainEmailDomain(nameof(UserDto.Email)))
                .MustAsync(async (rootObject, dealId, context, cancellationToken) => !await this.userDao.IsEmailTakenAsync(rootObject.Email))
                .WithMessage(MessageUtils.AlreadyExists(nameof(UserDto.Email)));

            RuleFor(x => x.Name)
              .NotEmpty()
              .WithMessage(MessageUtils.MandatoryField(nameof(UserDto.Name)));

            RuleFor(x => x.LastName)
              .NotEmpty()
              .WithMessage(MessageUtils.MandatoryField(nameof(UserDto.LastName)));

            RuleFor(x => x.Password)
              .NotEmpty()
              .WithMessage(MessageUtils.MandatoryField(nameof(UserDto.Password)))
              .MinimumLength(8)
              .WithMessage(MessageUtils.MustContainMinLength(nameof(UserDto.Password), 8))
              .Matches(ValidationConstants.UppercasePattern)
              .WithMessage(MessageUtils.MustContainUppercase(nameof(UserDto.Password)))
              .Matches(ValidationConstants.SpecialCharactersPattern)
              .WithMessage(MessageUtils.MustContainSpecialChar(nameof(UserDto.Password)))
              .Matches(ValidationConstants.NumberPattern)
              .WithMessage(MessageUtils.MustContainNumber(nameof(UserDto.Password)));
        }
    }
}
