namespace ProjectShop.Server.Core.Interfaces.IServices
{
    internal interface IBaseDeleteDataService
    {
        Task<int> DeleteAsync(string id);
    }
}
