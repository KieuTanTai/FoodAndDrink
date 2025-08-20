using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Services
{
    public class SystemClockService : IClock
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
