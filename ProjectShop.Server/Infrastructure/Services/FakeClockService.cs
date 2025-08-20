using ProjectShop.Server.Core.Interfaces.IValidate;
using System.Threading;

namespace ProjectShop.Server.Infrastructure.Services
{
    public class FakeClockService : IClock
    {
        public DateTime UtcNow { get; set; }
    }
}
