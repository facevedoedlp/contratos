namespace Zubeldia.Domain.Validators.Player
{
    using FluentValidation;
    using Zubeldia.Commons;
    using Zubeldia.Commons.Enums;
    using Zubeldia.Domain.Dtos.Player;

    public class PlayerValidator : AbstractValidator<CreatePlayerRequest>
    {
        public PlayerValidator()
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Nombre"))
                .MaximumLength(50)
                .WithMessage(MessageUtils.ExceedMaximumLength("Nombre", 50));

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Apellido"))
                .MaximumLength(50)
                .WithMessage(MessageUtils.ExceedMaximumLength("Apellido", 50));

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Email"))
                .EmailAddress()
                .WithMessage("Email inválido")
                .MaximumLength(100)
                .WithMessage(MessageUtils.ExceedMaximumLength("Email", 100));

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Fecha de nacimiento"));

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Fecha de inicio"));

            RuleFor(x => x.Gender)
                .Cascade(CascadeMode.Stop)
                .IsInEnum()
                .WithMessage(MessageUtils.NotExistInEnum("Género"));

            RuleFor(x => x.CountryId)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("País"));

            RuleFor(x => x.StateId)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Estado/Provincia"));

            RuleFor(x => x.CityId)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Ciudad"));

            RuleFor(x => x.DisciplineId)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Disciplina"));

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Categoría"));

            RuleFor(x => x.Positions)
                .NotEmpty()
                .WithMessage("Debe seleccionar al menos una posición")
                .Must(positions => positions != null && positions.Any())
                .WithMessage("Debe seleccionar al menos una posición");

            RuleFor(x => x.DominanceFoot)
                .Cascade(CascadeMode.Stop)
                .IsInEnum()
                .WithMessage(MessageUtils.NotExistInEnum("Pie dominante"));

            RuleFor(x => x.DominanceEye)
                .Cascade(CascadeMode.Stop)
                .IsInEnum()
                .WithMessage(MessageUtils.NotExistInEnum("Ojo dominante"));
        }
    }
}