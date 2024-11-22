namespace Zubeldia.Domain.Entities
{
    using System.Diagnostics.CodeAnalysis;
    using Zubeldia.Domain.Entities.Base;

    [ExcludeFromCodeCoverage]

    public class User : Entity<int>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}


/* TODO: Envio de Mail de confirmacion
 * No se puede tener un Email repetido
 * Mail solo del club estudiantesdelaplata.com
 * Registro, Logeo, JWT, ME */
