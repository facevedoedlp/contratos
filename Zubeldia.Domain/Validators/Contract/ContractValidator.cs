namespace Zubeldia.Domain.Validators.Contract
{
    using FluentValidation;
    using Zubeldia.Commons;
    using Zubeldia.Domain.Dtos.Contract;
    using Zubeldia.Domain.Interfaces.Providers;

    public class ContractValidator : AbstractValidator<CreateContractRequest>
    {
        private readonly IContractDao contractDao;
        public ContractValidator(IContractDao contractDao)
        {
            this.contractDao = contractDao;

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
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(MessageUtils.MandatoryField("Fecha de finalización"))
                .GreaterThan(x => x.StartDate)
                .WithMessage(MessageUtils.DateMustBeGreaterThan("Fecha de finalización"));

            RuleFor(x => x.EarlyTerminationDate)
                .Must((request, terminationDate) => !terminationDate.HasValue || (terminationDate >= request.StartDate && terminationDate <= request.EndDate))
                .When(x => x.EarlyTerminationDate.HasValue)
                .WithMessage("La fecha de terminación anticipada debe estar entre la fecha de inicio y fin del contrato");

            RuleFor(x => x.ReleaseClause)
                .Must(x => !x.HasValue || x.Value > 0)
                .WithMessage(MessageUtils.MustBeGreaterThanZero("Cláusula de rescisión"));

            RuleFor(x => x.File)
                .Must((request, file) => {
                    if (!request.Id.HasValue || request.Id == 0)
                    {
                        return file != null;
                    }

                    return true;
                })
                .WithMessage(MessageUtils.MandatoryField("Archivo"));

            RuleForEach(x => x.Objectives)
                .NotEmpty()
                .SetValidator(m => new ContractObjectiveValidator());

            RuleForEach(x => x.Salaries)
                .NotEmpty()
                .SetValidator(m => new ContractSalaryValidator());

            RuleForEach(x => x.Trajectories)
                .NotEmpty()
                .SetValidator(m => new ContractTrajectoryValidator());
        }
    }
}