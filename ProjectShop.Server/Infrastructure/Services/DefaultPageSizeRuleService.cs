using ProjectShop.Server.Core.Interfaces.IPlatformRules;

namespace ProjectShop.Server.Infrastructure.Services
{
    public class DefaultPageSizeRuleService : IDefaultPageSizeRule
    {
        public uint DefaultPageSize { get; set; }
    }
}
