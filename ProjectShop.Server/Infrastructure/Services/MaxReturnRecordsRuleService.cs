using ProjectShop.Server.Core.Interfaces.IPlatformRules;

namespace ProjectShop.Server.Infrastructure.Services
{
    public class MaxReturnRecordsRuleService : IMaxReturnRecordsRule
    {
        public uint MaxRecords { get; set; }
    }
}
