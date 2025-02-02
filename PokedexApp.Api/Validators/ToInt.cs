namespace PokedexApp.Validators
{
    public static class StringExtensions
    {
        public static int ToInt(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return 0;

            return int.TryParse(str, out int result) ? result : 0;
        }
    }
}
