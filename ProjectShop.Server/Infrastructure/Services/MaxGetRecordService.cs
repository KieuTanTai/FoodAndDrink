using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Services
{
    public class MaxGetRecordService : IMaxGetRecord
    {
        public uint MaxGetRecord { get; set; }
    }
}
