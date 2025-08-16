using ProjectShop.Server.Core.Interfaces.IData;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Services
{
    static class GetConnectionFactoryService
    {
        public static IDbConnectionFactory? ConnectionFactory { get; set; }

        public static void GetConnectionFactory()
        {
            try
            {
                ConnectionFactory = GetProviderService.SystemServices.GetRequiredService<IDbConnectionFactory>();
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when connect to db! {ex.Message}");
            }
        }
    }
}
