namespace Zubeldia.Commons
{
    using System.Collections.Generic;

    public static class MessageUtils
    {
        public const string Required = "El campo {0} es requerido.";
        public const string NoRecordsFound = "No hay registros para esta búsqueda.";
        private const string MessageStartDateMustBeLessThanEndDate = "La fecha de inicio debe ser menor o igual a la fecha de fin.";
        private const string MessageDateMustBeGreaterThanDate = "La fecha debe ser mayor a {0}";
        private const string MessageDateGreaterThanOrEqualToDate = "La fecha debe ser mayor o igual a {0}";
        private const string MessageDateGreaterThanOrEqualToLastDate = "La fecha debe ser mayor o igual a la última {0}";
        private const string MessageDateGreaterThanDate = "La fecha no puede ser mayor que {0}";
        private const string MessageDateBetween = "El {0} debe estar entre 10 días menor o igual que hoy";
        private const string MessageMustBePositiveNumber = "El campo {0} debe ser positivo.";
        private const string ValueNotExistInEnum = "El valor del campo {0} no existe en la estructura de datos enum.";
        private const string MessageMustBeGreaterThanZero = "El campo {0} debe ser mayor que 0.";
        private const string MessageMustBeGreaterOrEqualThanZero = "El campo {0} debe ser mayor o igual a 0.";
        private const string MessageIsInexistent = "El {0} es inexistente";
        private const string MessageRequired = "El campo {0} es requerido.";
        private const string MessageInvalidValue = "El campo {0} tiene un valor inválido.";
        private const string MessageInvalidDateFormat = "El campo {0} tiene un formato de fecha inválido. {1}";
        private const string MessageInvalidTimeFormat = "El campo {0} tiene un formato de hora inválido. {1}";
        private const string MessageMaximumLength = "El campo {0} debe tener {1} caracteres o menos.";
        private const string MessageLength = "El campo {0} debe tener {1} caracteres.";
        private const string MessageAlreadyExists = "El {0} ya existe.";
        private const string MessageDuplicatedInList = "Tienes {0} duplicado en tu solicitud";
        private const string MessageMustHaveAtLeastOneElement = "{0} debe tener al menos un elemento.";
        private const string MessageExcededMaxDecimals = "El campo {0} ha excedido el máximo de {1} lugares decimales.";
        private const string MessageMustContainEmailDomain = "El campo {0} debe pertenecer al dominio del club";
        private const string MessageMustContainMinLength = "El campo {0} debe tener al menos {1} caracteres";
        private const string MessageMustContainUppercase = "El campo {0} debe contener al menos una letra mayúscula";
        private const string MessageMustContainSpecialChar = "El campo {0} debe contener al menos un carácter especial";
        private const string MessageMustContainNumber = "El campo {0} debe contener al menos un número";

        public static string MandatoryField(string fieldName) => string.Format(MessageRequired, fieldName);
        public static string NotExistInEnum(string fieldName) => string.Format(ValueNotExistInEnum, fieldName);
        public static string FirstLetterToLower(string text) => char.ToLower(text[0]) + text[1..];
        public static string InvalidValue(string fieldName) => string.Format(MessageInvalidValue, fieldName);
        public static string InvalidDateFormat(string fieldName, string dateFormat = "") => string.Format(MessageInvalidDateFormat, fieldName, dateFormat);
        public static string InvalidTimeFormat(string fieldName, string timeFormat) => string.Format(MessageInvalidTimeFormat, fieldName, timeFormat);
        public static string InvalidLength(string fieldName, short length) => string.Format(MessageLength, fieldName, length);
        public static string ExceedMaximumLength(string fieldName, short maxLength) => string.Format(MessageMaximumLength, fieldName, maxLength);
        public static string ExeedMaximumDecimalsPlaces(string fieldName, short maxLength) => string.Format(MessageExcededMaxDecimals, fieldName, maxLength);
        public static string AlreadyExists(string fieldName) => string.Format(MessageAlreadyExists, fieldName);
        public static string MustBePositiveValue(string fieldName) => string.Format(MessageMustBePositiveNumber, fieldName);
        public static string MustBeGreaterThanZero(string fieldName) => string.Format(MessageMustBeGreaterThanZero, fieldName);
        public static string MustBeGreaterOrEqualThanZero(string fieldName) => string.Format(MessageMustBeGreaterOrEqualThanZero, fieldName);
        public static string StartDateMustBeLessThanEndDate() => MessageStartDateMustBeLessThanEndDate;
        public static string DateNotBeGreaterThan(string fieldName) => string.Format(MessageDateGreaterThanDate, fieldName);
        public static string DateMustBeGreaterThanOrEqualTo(string fieldName) => string.Format(MessageDateGreaterThanOrEqualToDate, fieldName);
        public static string DateMustBeGreaterThan(string fieldName) => string.Format(MessageDateMustBeGreaterThanDate, fieldName);
        public static string DateMustBeGreaterThanOrEqualToLastDate(string fieldName) => string.Format(MessageDateGreaterThanOrEqualToLastDate, fieldName);
        public static string DateBetween(string fieldName) => string.Format(MessageDateBetween, fieldName);
        public static string IsInexistent(string fieldName) => string.Format(MessageIsInexistent, fieldName);
        public static string DuplicatedInList(string fieldName) => string.Format(MessageDuplicatedInList, fieldName);
        public static string MustHaveAtLeastOneElement(string fieldName) => string.Format(MessageMustHaveAtLeastOneElement, fieldName);
        public static string OneOfTheFollowingListsMustBeCompleted(List<string> listNames) => $"One of the following lists must be completed: {string.Join(", ", listNames)}";
        public static string MustContainEmailDomain(string fieldName) => string.Format(MessageMustContainEmailDomain, fieldName);
        public static string MustContainMinLength(string fieldName, int length) => string.Format(MessageMustContainMinLength, fieldName, length);
        public static string MustContainUppercase(string fieldName) => string.Format(MessageMustContainUppercase, fieldName);
        public static string MustContainSpecialChar(string fieldName) => string.Format(MessageMustContainSpecialChar, fieldName);
        public static string MustContainNumber(string fieldName) => string.Format(MessageMustContainNumber, fieldName);
    }
}
