using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Services;

namespace TLGames.Application.Services
{
    public abstract class ValidateService<T> where T : class //: IGetDataService<T>, IInsertDataService<T>, IUpdateSingleKeyDataService<T> where T : class
    {
        protected readonly IHashPassword hashPassword = GetProviderService.SystemServices.GetRequiredService<IHashPassword>();
        protected readonly IColumnService colService = GetProviderService.SystemServices.GetRequiredService<IColumnService>();
        protected readonly IStringConverter converter = GetProviderService.SystemServices.GetRequiredService<IStringConverter>();

        protected async Task<bool> IsExistObject(string input, Func<string, Task<T?>> daoFunc)
        {
            T? existingObject = await daoFunc(input);
            return existingObject != null;
        }

        protected async Task<bool> IsExistObject(int input, Func<int, Task<T?>> daoFunc)
        {
            T? existingObject = await daoFunc(input);
            return existingObject != null;
        }

        protected async Task<bool> DoAllIdsExistAsync(IEnumerable<string> ids, Func<IEnumerable<string>, Task<IEnumerable<T>>> daoFunc)
        {
            if (ids == null || !ids.Any())
                return false; 
            IEnumerable<T> existingObjects = await daoFunc(ids);
            return existingObjects.Count() == ids.Count();
        }

        protected async Task<bool> DoAllIdsExistAsync(IEnumerable<int> ids, Func<IEnumerable<int>, Task<IEnumerable<T>>> daoFunc)
        {
            if (ids == null || !ids.Any())
                return false; 
            IEnumerable<T> existingObjects = await daoFunc(ids);
            return existingObjects.Count() == ids.Count();
        }

        protected async Task<bool> DoNoneOfIdsExistAsync(IEnumerable<string> ids, Func<IEnumerable<string>, Task<IEnumerable<T>>> daoFunc)
        {
            if (ids == null || !ids.Any())
                return true; 
            IEnumerable<T> existingObjects = await daoFunc(ids);
            return !existingObjects.Any();
        }

        protected async Task<bool> DoNoneOfIdsExistAsync(IEnumerable<int> ids, Func<IEnumerable<int>, Task<IEnumerable<T>>> daoFunc)
        {
            if (ids == null || !ids.Any())
                return true;
            IEnumerable<T> existingObjects = await daoFunc(ids);
            return !existingObjects.Any();
        }
    }
}
