namespace Zubeldia.Dtos.Validatiors.User
{
    using FluentValidation;
    using Zubeldia.Commons;
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
            .MustAsync(async (rootObject, dealId, context, cancellationToken) => await this.userDao.IsEmailTaken(rootObject.Email))
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
               .WithMessage("La contraseña debe tener al menos 8 caracteres")
               .Matches("[A-Z]")
               .WithMessage("La contraseña debe contener al menos una letra mayúscula")
               .Matches("[!@#$%^&*(),.?\":{}|<>]")
               .WithMessage("La contraseña debe contener al menos un carácter especial")
               .Matches("[0-9]")
               .WithMessage("La contraseña debe contener al menos un número");
        }
    }
}
