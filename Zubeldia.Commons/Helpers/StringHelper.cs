namespace Zubeldia.Commons.Helpers
{
    public static class StringHelper
    {
        public static bool EqualsIgnoreCase(string? first, string? second) => string.Equals(first, second, StringComparison.OrdinalIgnoreCase);
    }
}
