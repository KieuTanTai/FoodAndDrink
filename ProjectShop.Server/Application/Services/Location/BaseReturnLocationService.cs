using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Application.Services.Location
{
    public class BaseReturnLocationService : IBaseGetNavigationPropertyServices<LocationModel, LocationNavigationOptions>
    {
        private readonly ILogService _logger;
        private readonly IDAO<LocationCityModel> _baseCityDAO;
        private readonly IDAO<LocationDistrictModel> _baseDistrictDAO;
        private readonly IDAO<LocationWardModel> _baseWardDAO;
        private readonly IDAO<LocationTypeModel> _baseTypeDAO;
        private readonly IInventoryDAO<InventoryModel> _inventoryDAO;
        private readonly IInventoryMovementDAO<InventoryMovementModel> _inventoryMovementDAO;
        private readonly IDisposeProductDAO<DisposeProductModel> _disposeProductDAO;
        private readonly IServiceResultFactory<BaseReturnLocationService> _serviceResultFactory;

        public BaseReturnLocationService(ILogService logger, IDAO<LocationCityModel> baseCityDAO,
            IDAO<LocationDistrictModel> baseDistrictDAO, IDAO<LocationWardModel> baseWardDAO,
            IDAO<LocationTypeModel> baseTypeDAO, IInventoryDAO<InventoryModel> inventoryDAO,
            IInventoryMovementDAO<InventoryMovementModel> inventoryMovementDAO,
            IDisposeProductDAO<DisposeProductModel> disposeProductDAO, IServiceResultFactory<BaseReturnLocationService> serviceResultFactory)
        {
            _logger = logger;
            _baseCityDAO = baseCityDAO;
            _baseDistrictDAO = baseDistrictDAO;
            _baseWardDAO = baseWardDAO;
            _baseTypeDAO = baseTypeDAO;
            _inventoryDAO = inventoryDAO;
            _inventoryMovementDAO = inventoryMovementDAO;
            _disposeProductDAO = disposeProductDAO;
            _serviceResultFactory = serviceResultFactory;
        }

        public async Task<ServiceResult<LocationModel>> GetNavigationPropertyByOptionsAsync(LocationModel entity, LocationNavigationOptions options,
            [CallerMemberName] string? methodCall = null)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResults<LocationModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<LocationModel> entities, LocationNavigationOptions options,
            [CallerMemberName] string? methodCall = null)
        {
            throw new NotImplementedException();
        }

        // NOTE: private method for navigation properties (single param)
        private async Task<ServiceResult<LocationCityModel>> TryLoadLocationCityAsync(uint locationCityId)
            => await TryLoadEntityAsync<LocationCityModel>(locationCityId, (id) => _baseCityDAO.GetSingleDataAsync(id.ToString()),
                () => new LocationCityModel(), nameof(TryLoadLocationCityAsync));

        private async Task<ServiceResult<LocationDistrictModel>> TryLoadLocationDistrictAsync(uint locationDistrictId)
            => await TryLoadEntityAsync<LocationDistrictModel>(locationDistrictId, (id) => _baseDistrictDAO.GetSingleDataAsync(id.ToString()),
                () => new LocationDistrictModel(), nameof(TryLoadLocationDistrictAsync));

        private async Task<ServiceResult<LocationWardModel>> TryLoadLocationWardAsync(uint locationWardId)
            => await TryLoadEntityAsync<LocationWardModel>(locationWardId, (id) => _baseWardDAO.GetSingleDataAsync(id.ToString()),
                () => new LocationWardModel(), nameof(TryLoadLocationWardAsync));
        private async Task<ServiceResult<LocationTypeModel>> TryLoadLocationTypeAsync(uint locationTypeId)
            => await TryLoadEntityAsync<LocationTypeModel>(locationTypeId, (id) => _baseTypeDAO.GetSingleDataAsync(id.ToString()),
                () => new LocationTypeModel(), nameof(TryLoadLocationTypeAsync));

        private async Task<ServiceResult<InventoryModel>> TryLoadInventoryAsync(uint locationId)
            => await TryLoadEntityAsync<InventoryModel>(locationId, _inventoryDAO.GetByLocationIdAsync, () => new InventoryModel(), nameof(TryLoadInventoryAsync));

        private async Task<ServiceResults<InventoryMovementModel>> TryLoadSourceInventoryMovementsAsync(uint locationId)
            => await TryLoadICollectionEntitiesAsync<InventoryMovementModel>(locationId, (id) => _inventoryMovementDAO.GetBySourceLocationIdAsync(id, null),
                nameof(TryLoadSourceInventoryMovementsAsync));

        private async Task<ServiceResults<InventoryMovementModel>> TryLoadDestinationInventoryMovementsAsync(uint locationId)
            => await TryLoadICollectionEntitiesAsync<InventoryMovementModel>(locationId, (id) => _inventoryMovementDAO.GetByDestinationLocationIdAsync(id, null),
                nameof(TryLoadDestinationInventoryMovementsAsync));

        private async Task<ServiceResults<DisposeProductModel>> TryLoadDisposeProductsAsync(uint locationId)
            => await TryLoadICollectionEntitiesAsync<DisposeProductModel>(locationId, (id) => _disposeProductDAO.GetByLocationIdAsync(id, null),
                nameof(TryLoadDisposeProductsAsync));

        // NOTE: private method for navigation properties (IEnumerable param)
        private async Task<IDictionary<uint, ServiceResult<LocationCityModel>>> TryLoadLocationCitiesAsync(IEnumerable<uint> locationCityIds)
            => await TryLoadEntitiesAsync(locationCityIds, (ids) => _baseCityDAO.GetByInputsAsync(ids.Select(id => id.ToString())),
                () => new LocationCityModel(), () => 0, nameof(TryLoadLocationCitiesAsync));

        private async Task<IDictionary<uint, ServiceResult<LocationDistrictModel>>> TryLoadLocationDistrictsAsync(IEnumerable<uint> locationDistrictIds)
            => await TryLoadEntitiesAsync(locationDistrictIds, (ids) => _baseDistrictDAO.GetByInputsAsync(ids.Select(id => id.ToString())),
                () => new LocationDistrictModel(), () => 0, nameof(TryLoadLocationDistrictsAsync));

        private async Task<IDictionary<uint, ServiceResult<LocationWardModel>>> TryLoadLocationWardsAsync(IEnumerable<uint> locationWardIds)
            => await TryLoadEntitiesAsync(locationWardIds, (ids) => _baseWardDAO.GetByInputsAsync(ids.Select(id => id.ToString())),
                () => new LocationWardModel(), () => 0, nameof(TryLoadLocationWardsAsync));

        private async Task<IDictionary<uint, ServiceResult<LocationTypeModel>>> TryLoadLocationTypesAsync(IEnumerable<uint> locationTypeIds)
            => await TryLoadEntitiesAsync(locationTypeIds, (ids) => _baseTypeDAO.GetByInputsAsync(ids.Select(id => id.ToString())),
                () => new LocationTypeModel(), () => 0, nameof(TryLoadLocationTypesAsync));

        private async Task<IDictionary<uint, ServiceResult<InventoryModel>>> TryLoadInventoriesAsync(IEnumerable<uint> locationIds)
            => await TryLoadEntitiesAsync(locationIds, (ids) => _inventoryDAO.GetByLocationIdsAsync(ids),
                () => new InventoryModel(), () => 0, nameof(TryLoadInventoriesAsync));

        private async Task<IDictionary<uint, ServiceResults<InventoryMovementModel>>> TryLoadSourceInventoryMovementsAsync(IEnumerable<uint> locationIds)
            => await TryLoadICollectionEntitiesAsync(locationIds, (ids) => _inventoryMovementDAO.GetBySourceLocationIdsAsync(ids, null),
                () => new InventoryMovementModel(), () => 0, nameof(TryLoadSourceInventoryMovementsAsync));

        private async Task<IDictionary<uint, ServiceResults<InventoryMovementModel>>> TryLoadDestinationInventoryMovementsAsync(IEnumerable<uint> locationIds)
            => await TryLoadICollectionEntitiesAsync(locationIds, (ids) => _inventoryMovementDAO.GetByDestinationLocationIdsAsync(ids, null),
                () => new InventoryMovementModel(), () => 0, nameof(TryLoadDestinationInventoryMovementsAsync));

        private async Task<IDictionary<uint, ServiceResults<DisposeProductModel>>> TryLoadDisposeProductsAsync(IEnumerable<uint> locationIds)
            => await TryLoadICollectionEntitiesAsync(locationIds, (ids) => _disposeProductDAO.GetByLocationIdsAsync(ids, null),
                () => new DisposeProductModel(), () => 0, nameof(TryLoadDisposeProductsAsync));

        // DRY:
        private async Task<ServiceResult<Entity>> TryLoadEntityAsync<Entity>(uint id, Func<uint, Task<Entity?>> daoFunc,
            Func<Entity> constructorDelegate,
            [CallerMemberName] string? methodCall = null) where Entity : class
        {
            ServiceResult<Entity> serviceResult = new();
            try
            {
                Entity? entity = await daoFunc(id);
                bool isEntityFound = entity != null;
                entity ??= constructorDelegate();
                string message = isEntityFound ? $"Successfully retrieved entity : {id}." : $"Entity not found for id: {id}.";
                return _serviceResultFactory.CreateServiceResult<Entity>(message, entity, isEntityFound, methodCall: methodCall);
            }
            catch (Exception ex)
            {

                return _serviceResultFactory.CreateServiceResult<Entity>(
                    $"Error occurred while retrieving entity for id: {id}.", constructorDelegate(), false, ex, methodCall: methodCall);
            }
        }

        private async Task<IDictionary<uint, ServiceResult<Entity>>> TryLoadEntitiesAsync<Entity>(IEnumerable<uint> ids,
            Func<IEnumerable<uint>, Task<IEnumerable<Entity>>> daoFunc,
            Func<Entity> constructorDelegate,
            Func<uint> fieldSelector,
            [CallerMemberName] string? methodCall = null) where Entity : class
        {
            uint firstId = ids.FirstOrDefault();
            try
            {
                IEnumerable<Entity> entities = await daoFunc(ids);
                if (!entities.Any())
                {
                    return new Dictionary<uint, ServiceResult<Entity>>()
                    {
                        [firstId] = _serviceResultFactory.CreateServiceResult<Entity>($"No entities found for given ids.", constructorDelegate(), false, methodCall: methodCall)
                    };
                }

                return entities.ToDictionary(entity => fieldSelector(),
                    entity => _serviceResultFactory.CreateServiceResult<Entity>($"Successfully retrieved entities for given id: {fieldSelector()}.",
                    entity, true, methodCall: methodCall));
            }
            catch (Exception ex)
            {
                return new Dictionary<uint, ServiceResult<Entity>>()
                {
                    [firstId] = _serviceResultFactory.CreateServiceResult<Entity>(
                        $"Error occurred while retrieving entities for given ids.", constructorDelegate(), false, ex, methodCall: methodCall)
                };
            }
        }

        private async Task<ServiceResults<Entity>> TryLoadICollectionEntitiesAsync<Entity>(uint id, Func<uint, Task<IEnumerable<Entity>>> daoFunc,
            [CallerMemberName] string? methodCall = null) where Entity : class
        {
            List<JsonLogEntry> logEntries = new();
            try
            {
                IEnumerable<Entity> entities = await daoFunc(id);
                bool isEntityFound = entities.Any();
                if (!isEntityFound)
                {
                    logEntries.Add(_logger.JsonLogWarning<Entity, BaseReturnLocationService>($"No entities found for given id: {id}.", methodCall: methodCall));
                    return _serviceResultFactory.CreateServiceResults<Entity>(entities, logEntries);
                }
                logEntries.Add(_logger.JsonLogInfo<Entity, BaseReturnLocationService>($"Successfully retrieved entities for given id: {id}.", methodCall: methodCall));
                return _serviceResultFactory.CreateServiceResults<Entity>(entities, logEntries);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<Entity>(
                    $"Error occurred while retrieving entities for given id: {id}.", [], false, ex, methodCall: methodCall);
            }
        }


        private async Task<IDictionary<uint, ServiceResults<Entity>>> TryLoadICollectionEntitiesAsync<Entity>(IEnumerable<uint> ids,
            Func<IEnumerable<uint>, Task<IEnumerable<Entity>>> daoFunc,
            Func<Entity> constructorDelegate,
            Func<uint> fieldSelector,
            [CallerMemberName] string? methodCall = null) where Entity : class
        {
            uint firstId = ids.FirstOrDefault();
            List<JsonLogEntry> logEntries = new();
            try
            {
                IEnumerable<Entity> entities = await daoFunc(ids);
                if (!entities.Any())
                {
                    logEntries.Add(_logger.JsonLogWarning<Entity, BaseReturnLocationService>($"No entities found for given ids.", methodCall: methodCall));
                    return new Dictionary<uint, ServiceResults<Entity>>()
                    {
                        [firstId] = _serviceResultFactory.CreateServiceResults<Entity>([], logEntries)
                    };
                }
                logEntries.Add(_logger.JsonLogInfo<Entity, BaseReturnLocationService>($"Successfully retrieved entities for given ids.", methodCall: methodCall));
                return new Dictionary<uint, ServiceResults<Entity>>()
                {
                    [firstId] = _serviceResultFactory.CreateServiceResults<Entity>(entities, logEntries)
                };
            }
            catch (Exception ex)
            {
                return new Dictionary<uint, ServiceResults<Entity>>()
                {
                    [firstId] = _serviceResultFactory.CreateServiceResults<Entity>(
                        $"Error occurred while retrieving entities for given ids.", [], false, ex, methodCall: methodCall)
                };
            }
        }

        //DRY helper:
        private async Task LoadEntityAsync<Entity>(Action<Entity> assignResult, Func<uint> selectorField, Func<uint, Task<ServiceResult<Entity>>> tryLoadFunc,
            List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null) where Entity : class
        {
            var result = await tryLoadFunc(selectorField());
            logEntries.AddRange(result.LogEntries ?? []);
            if (result.Data != null)
                assignResult(result.Data);
            else
                logEntries.Add(_logger.JsonLogWarning<Entity, BaseReturnLocationService>($"Entity not found for id {selectorField()}.",
                    methodCall: methodCall));
        }

        private async Task LoadIcollectionEntitiesAsync<Entity>(Action<IEnumerable<Entity>> assignResult, Func<uint> selectorField, Func<uint, Task<ServiceResults<Entity>>> tryLoadFunc,
            List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null) where Entity : class
        {
            var result = await tryLoadFunc(selectorField());
            logEntries.AddRange(result.LogEntries ?? []);
            if (result.Data != null && result.Data.Any())
                assignResult(result.Data);
            else
                logEntries.Add(_logger.JsonLogWarning<Entity, BaseReturnLocationService>($"No entities found for id {selectorField()}.",
                    methodCall: methodCall));
        }

        private async Task LoadICollectionEntitiesAsync<Entity>(IEnumerable<LocationModel> locations, Action<LocationModel, IEnumerable<Entity>> assignResult,
            Func<LocationModel, uint> selectorField,
            Func<IEnumerable<uint>, Task<IDictionary<uint, ServiceResults<Entity>>>> tryLoadFunc,
            List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null) where Entity : class
        {
            var locationList = locations.ToList();
            IEnumerable<uint> ids = locationList.Select(selectorField).Distinct();
            var results = await tryLoadFunc(ids);
            foreach (var location in locationList)
            {
                if (!results.TryGetValue(selectorField(location), out var serviceResults) || serviceResults == null || serviceResults.Data == null)
                    continue;
                logEntries.AddRange(serviceResults.LogEntries ?? []);
                assignResult(location, serviceResults.Data);
            }
            logEntries.Add(_logger.JsonLogInfo<Entity, BaseReturnLocationService>($"Loaded ICollection entities for locations.", methodCall: methodCall));
        }

        private async Task LoadEntitiesAsync<Entity>(IEnumerable<LocationModel> locations, Action<LocationModel, Entity> assignResult,
            Func<LocationModel, uint> selectorField,
            Func<IEnumerable<uint>, Task<IDictionary<uint, ServiceResult<Entity>>>> tryLoadFunc,
            List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null) where Entity : class
        {
            var locationList = locations.ToList();
            IEnumerable<uint> ids = locationList.Select(selectorField).Distinct();
            var results = await tryLoadFunc(ids);
            foreach (var location in locationList)
            {
                if (!results.TryGetValue(selectorField(location), out var serviceResult) || serviceResult == null || serviceResult.Data == null)
                    continue;
                logEntries.AddRange(serviceResult.LogEntries ?? []);
                assignResult(location, serviceResult.Data);
            }
            logEntries.Add(_logger.JsonLogInfo<Entity, BaseReturnLocationService>($"Loaded entities for locations.", methodCall: methodCall));
        }

        // NOTE: Helper methods
        private async Task LoadLocationCityAsync(LocationModel location, List<JsonLogEntry> logEntries)
            => await LoadEntityAsync<LocationCityModel>(city => location.LocationCity = city,
                () => location.LocationCityId, TryLoadLocationCityAsync, logEntries, nameof(LoadLocationCityAsync));

        private async Task LoadLocationDistrictAsync(LocationModel location, List<JsonLogEntry> logEntries)
            => await LoadEntityAsync<LocationDistrictModel>(district => location.LocationDistrict = district,
                () => location.LocationDistrictId, TryLoadLocationDistrictAsync, logEntries, nameof(LoadLocationDistrictAsync));

        private async Task LoadLocationWardAsync(LocationModel location, List<JsonLogEntry> logEntries)
            => await LoadEntityAsync<LocationWardModel>(ward => location.LocationWard = ward,
                () => location.LocationWardId, TryLoadLocationWardAsync, logEntries, nameof(LoadLocationWardAsync));

        private async Task LoadLocationTypeAsync(LocationModel location, List<JsonLogEntry> logEntries)
            => await LoadEntityAsync<LocationTypeModel>(type => location.LocationType = type,
                () => location.LocationTypeId, TryLoadLocationTypeAsync, logEntries, nameof(LoadLocationTypeAsync));

        private async Task LoadInventoryAsync(LocationModel location, List<JsonLogEntry> logEntries)
            => await LoadEntityAsync<InventoryModel>(inventory => location.Inventory = inventory,
                () => location.LocationId, TryLoadInventoryAsync, logEntries, nameof(LoadInventoryAsync));

        private async Task LoadSourceInventoryMovementsAsync(LocationModel location, List<JsonLogEntry> logEntries)
            => await LoadIcollectionEntitiesAsync<InventoryMovementModel>(movements => location.SourceInventoryMovements = [.. movements],
                () => location.LocationId, TryLoadSourceInventoryMovementsAsync, logEntries, nameof(LoadSourceInventoryMovementsAsync));

        private async Task LoadDestinationInventoryMovementsAsync(LocationModel location, List<JsonLogEntry> logEntries)
            => await LoadIcollectionEntitiesAsync<InventoryMovementModel>(movements => location.DestinationInventoryMovements = [.. movements],
                () => location.LocationId, TryLoadDestinationInventoryMovementsAsync, logEntries, nameof(LoadDestinationInventoryMovementsAsync));

        private async Task LoadDisposeProductsAsync(LocationModel location, List<JsonLogEntry> logEntries)
            => await LoadIcollectionEntitiesAsync<DisposeProductModel>(disposeProducts => location.DisposeProducts = [.. disposeProducts],
                () => location.LocationId, TryLoadDisposeProductsAsync, logEntries, nameof(LoadDisposeProductsAsync));

        private async Task LoadLocationCititesAsync(IEnumerable<LocationModel> locations, List<JsonLogEntry> logEntries)
            => await LoadEntitiesAsync<LocationCityModel>(locations, (loc, city) => loc.LocationCity = city,
                loc => loc.LocationCityId, TryLoadLocationCitiesAsync, logEntries, nameof(LoadLocationCititesAsync));

        private async Task LoadLocationWardsAsync(IEnumerable<LocationModel> locations, List<JsonLogEntry> logEntries)
            => await LoadEntitiesAsync<LocationWardModel>(locations, (loc, ward) => loc.LocationWard = ward,
                loc => loc.LocationWardId, TryLoadLocationWardsAsync, logEntries, nameof(LoadLocationWardsAsync));

        private async Task LoadLocationTypesAsync(IEnumerable<LocationModel> locations, List<JsonLogEntry> logEntries)
            => await LoadEntitiesAsync<LocationTypeModel>(locations, (loc, type) => loc.LocationType = type,
                loc => loc.LocationTypeId, TryLoadLocationTypesAsync, logEntries, nameof(LoadLocationTypesAsync));

        private async Task LoadInventoriesAsync(IEnumerable<LocationModel> locations, List<JsonLogEntry> logEntries)
            => await LoadEntitiesAsync<InventoryModel>(locations, (loc, inventory) => loc.Inventory = inventory,
                loc => loc.LocationId, TryLoadInventoriesAsync, logEntries, nameof(LoadInventoriesAsync));

        private async Task LoadSourceInventoryMovementsAsync(IEnumerable<LocationModel> locations, List<JsonLogEntry> logEntries)
            => await LoadICollectionEntitiesAsync<InventoryMovementModel>(locations, (loc, movements) => loc.SourceInventoryMovements = [.. movements],
                loc => loc.LocationId, TryLoadSourceInventoryMovementsAsync, logEntries, nameof(LoadSourceInventoryMovementsAsync));

        private async Task LoadDestinationInventoryMovementsAsync(IEnumerable<LocationModel> locations, List<JsonLogEntry> logEntries)
            => await LoadICollectionEntitiesAsync<InventoryMovementModel>(locations, (loc, movements) => loc.DestinationInventoryMovements = [.. movements],
                loc => loc.LocationId, TryLoadDestinationInventoryMovementsAsync, logEntries, nameof(LoadDestinationInventoryMovementsAsync));

        private async Task LoadDisposeProductsAsync(IEnumerable<LocationModel> locations, List<JsonLogEntry> logEntries)
            => await LoadICollectionEntitiesAsync<DisposeProductModel>(locations, (loc, disposeProducts) => loc.DisposeProducts = [.. disposeProducts],
                loc => loc.LocationId, TryLoadDisposeProductsAsync, logEntries, nameof(LoadDisposeProductsAsync));
    }
}
