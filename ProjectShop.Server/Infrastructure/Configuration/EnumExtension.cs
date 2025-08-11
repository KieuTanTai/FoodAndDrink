namespace ProjectShop.Server.Infrastructure.Configuration
{
    public static class EnumExtensions
    {
        // Enum uppercase -> lowercase string cho DB
        public static string ToDbValue<T>(this T enumValue) where T : Enum
        {
            return enumValue.ToString().ToLower();
        }

        // lowercase string từ DB -> Enum uppercase
        public static T ToEnumValue<T>(this string dbValue) where T : struct, Enum
        {
            return Enum.Parse<T>(dbValue.ToUpper());
        }
    }

}
