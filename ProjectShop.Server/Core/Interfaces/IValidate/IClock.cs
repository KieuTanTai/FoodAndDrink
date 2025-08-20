namespace ProjectShop.Server.Core.Interfaces.IValidate
{
    public interface IClock
    {
        DateTime UtcNow { get; }
    }
}
