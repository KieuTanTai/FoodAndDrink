namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IDeleteAsync
    {
        Task<int> DeleteAsync(string id);
        Task<int> DeleteManyAsync(IEnumerable<string> ids);
    }
}
