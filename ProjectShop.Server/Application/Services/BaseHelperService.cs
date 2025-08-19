using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Entities.GetNavigationPropertyOptions;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Services;

namespace TLGames.Application.Services
{
    public abstract class BaseHelperService<TEntity> where TEntity : class //: IGetDataService<TEntity>, IInsertDataService<TEntity>, IUpdateSingleKeyDataService<TEntity> where TEntity : class
    {
        protected readonly IHashPassword hashPassword = GetProviderService.SystemServices.GetRequiredService<IHashPassword>();
        protected readonly IColumnService colService = GetProviderService.SystemServices.GetRequiredService<IColumnService>();
        protected readonly IStringConverter converter = GetProviderService.SystemServices.GetRequiredService<IStringConverter>();

        protected async Task<bool> IsExistObject(string input, Func<string, Task<TEntity?>> daoFunc)
        {
            try
            {
                TEntity? existingObject = await daoFunc(input);
                return existingObject != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected async Task<bool> IsExistObject(int input, Func<int, Task<TEntity?>> daoFunc)
        {
            try
            {
                TEntity? existingObject = await daoFunc(input);
                return existingObject != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected async Task<bool> DoAllIdsExistAsync(IEnumerable<string> ids, Func<IEnumerable<string>, Task<IEnumerable<TEntity>>> daoFunc)
        {
            if (ids == null || !ids.Any())
                return false;
            try
            {
                IEnumerable<TEntity> existingObjects = await daoFunc(ids);
                return existingObjects.Count() == ids.Count();
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected async Task<bool> DoAllIdsExistAsync(IEnumerable<int> ids, Func<IEnumerable<int>, Task<IEnumerable<TEntity>>> daoFunc)
        {
            if (ids == null || !ids.Any())
                return false;
            try
            {
                IEnumerable<TEntity> existingObjects = await daoFunc(ids);
                return existingObjects.Count() == ids.Count();
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected async Task<bool> DoNoneOfIdsExistAsync(IEnumerable<string> ids, Func<IEnumerable<string>, Task<IEnumerable<TEntity>>> daoFunc)
        {
            if (ids == null || !ids.Any())
                return false;
            try
            {
                IEnumerable<TEntity> existingObjects = await daoFunc(ids);
                return !existingObjects.Any();
            }
            catch (Exception)
            {
                return true;
            }
        }

        protected async Task<bool> DoNoneOfIdsExistAsync(IEnumerable<int> ids, Func<IEnumerable<int>, Task<IEnumerable<TEntity>>> daoFunc)
        {
            if (ids == null || !ids.Any())
                return false;
            try
            {
                IEnumerable<TEntity> existingObjects = await daoFunc(ids);
                return !existingObjects.Any();
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
