using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Services
{
    public class StringConverter : IStringConverter
    {
        public string SnakeCaseToPascalCase(string snakeCase)
        {
            return string.Concat(snakeCase.Split('_')
                .Select(word => char.ToUpper(word[0]) + word.Substring(1)));
        }
    }
}
