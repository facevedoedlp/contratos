namespace Grogu.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using Zubeldia.Domain.Dtos.Commons;

    [ExcludeFromCodeCoverage]
    public static class EnumExtension
    {
        public static List<KeyNameDTO> GetKeyNameFromEnum<T>(HashSet<string> excludeValues = null)
            where T : struct, IConvertible
        {
            var source = Enum.GetValues(typeof(T));
            var list = new List<KeyNameDTO>();

            foreach (T value in source)
            {
                string valueAsString = value.ToString();

                if (excludeValues != null && excludeValues.Contains(valueAsString))
                {
                    continue;
                }

                list.Add(new KeyNameDTO()
                {
                    Id = Convert.ToInt32(value),
                    Code = value.ToString(),
                    Name = DisplayValue<T>(value),
                });
            }

            return list;
        }

        public static KeyNameDTO GetKeyNameDTOFromEnumValue<T>(this T value)
            where T : struct, IConvertible
        {
            return new KeyNameDTO
            {
                Id = Convert.ToInt32(value),
                Code = value.ToString(),
                Name = DisplayValue<T>(value),
            };
        }

        public static string DisplayValue<T>(this T value)
            where T : struct, IConvertible
        {
            return GetDisplayValue(ref value);
        }

        public static IEnumerable<KeyNameDTO> GetSelectListFrom<T>(IList<T> source, bool getWithDisplayName = false)
            where T : struct, IConvertible
        {
            return GetSelectListFrom(source, null, false, getWithDisplayName);
        }

        public static IEnumerable<KeyNameDTO> GetSelectListFrom<T>(IList<T> source, object selectedValue, bool getWithDisplayName = false)
            where T : struct, IConvertible
        {
            return GetSelectListFrom(source, selectedValue, false);
        }

        public static T GetEnumFrom<T>(string value)
            where T : struct, IConvertible
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static IEnumerable<KeyNameDTO> GetSelectListFrom<T>(IList<T> source, object selectedValue, bool orderByName, bool getWithDisplayName = false)
            where T : struct, IConvertible
        {
            var items = new List<KeyNameDTO>();

            var displayAttributeType = typeof(DisplayAttribute);

            foreach (var value in source)
            {
                FieldInfo field = value.GetType().GetField(value.ToString());

                DisplayAttribute attrs = (DisplayAttribute)field.
                              GetCustomAttributes(displayAttributeType, false).First();

                var intValue = (int)Enum.Parse(typeof(T), value.ToString());

                items.Add(new KeyNameDTO()
                {
                    Id = Convert.ToInt32(value),
                    Code = value.ToString(),
                    Name = getWithDisplayName ? value.DisplayValue() : value.ToString(),
                });
            }

            return items;
        }

        private static string GetDisplayValue<T>(ref T value)
            where T : struct, IConvertible
        {
            var displayAttributeType = typeof(DisplayAttribute);

            FieldInfo field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                return string.Empty;
            }

            DisplayAttribute attrs = (DisplayAttribute)field.GetCustomAttributes(displayAttributeType, false).FirstOrDefault();

            return (attrs != null) ? attrs.GetName() : value.ToString();
        }
    }
}
