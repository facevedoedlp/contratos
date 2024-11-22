namespace Zubeldia.Dtos.Models.Commons
{
    public class Error
    {
        private string propertyName;

        private string GetPropertyName()
        {
            return propertyName;
        }

        private void SetPropertyName(string value)
        {
            propertyName = value;
        }

        // UI uses firstLetterLower to map properties. if you change something here its going to break UI.
        public string PropertyName
        {
            get
            {
                return GetPropertyName();
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    SetPropertyName(null);
                }
                else
                {
                    var lastDotIndex = value.LastIndexOf('.');
                    SetPropertyName(char.ToLower(value[lastDotIndex + 1]) + value.Substring(lastDotIndex + 2));
                }
            }
        }

        public string AttemptedValue { get; set; }

        public string ErrorMessage { get; set; }
    }
}
