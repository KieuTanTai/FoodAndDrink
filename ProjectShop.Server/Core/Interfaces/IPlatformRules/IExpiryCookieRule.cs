namespace ProjectShop.Server.Core.Interfaces.IPlatformRules
{
    public interface IExpiryCookieRule
    {
        uint MaxAgeDays { get; }
    }
}
