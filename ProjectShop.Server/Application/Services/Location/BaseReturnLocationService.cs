using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices._IBase;
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
        private readonly IDAO<LocationWardModel> _baseWardDAO;
        private readonly IDAO<LocationTypeModel> _baseTypeDAO;
        private readonly IInventoryDAO<InventoryModel> _inventoryDAO;
        private readonly IDAO<LocationDistrictModel> _baseDistrictDAO;
        private readonly IDisposeProductDAO<DisposeProductModel> _disposeProductDAO;
        private readonly IInventoryMovementDAO<InventoryMovementModel> _inventoryMovementDAO;
        private readonly IServiceResultFactory<BaseReturnLocationService> _serviceResultFactory;
        private readonly IBaseHelperReturnTEntityService<BaseReturnLocationService> _baseHelperReturnTEntityService;

        public BaseReturnLocationService(ILogService logger, IDAO<LocationCityModel> baseCityDAO,
            IDAO<LocationDistrictModel> baseDistrictDAO, IDAO<LocationWardModel> baseWardDAO,
            IDAO<LocationTypeModel> baseTypeDAO, IInventoryDAO<InventoryModel> inventoryDAO,
            IInventoryMovementDAO<InventoryMovementModel> inventoryMovementDAO,
            IBaseHelperReturnTEntityService<BaseReturnLocationService> baseHelperReturnTEntityService,
            IDisposeProductDAO<DisposeProductModel> disposeProductDAO, IServiceResultFactory<BaseReturnLocationService> serviceResultFactory)
        {
            _logger = logger;
            _baseCityDAO = baseCityDAO;
            _baseWardDAO = baseWardDAO;
            _baseTypeDAO = baseTypeDAO;
            _inventoryDAO = inventoryDAO;
            _baseDistrictDAO = baseDistrictDAO;
            _disposeProductDAO = disposeProductDAO;
            _serviceResultFactory = serviceResultFactory;
            _inventoryMovementDAO = inventoryMovementDAO;
            _baseHelperReturnTEntityService = baseHelperReturnTEntityService;
        }

        public async Task<ServiceResult<LocationModel>> GetNavigationPropertyByOptionsAsync(LocationModel entity, LocationNavigationOptions options,
            [CallerMemberName] string? methodCall = null)
        {
            List<JsonLogEntry> logEntries = [];
            bool isSuccess = true;

            if (options.IsGetInventory)
            {
                await LoadInventoryAsync(entity, logEntries);
                isSuccess = ValidateInventoryLoaded(entity, isSuccess);
            }

            if (options.IsGetLocationCity)
            {
                await LoadLocationCityAsync(entity, logEntries);
                isSuccess = ValidateLocationCityLoaded(entity, isSuccess);
            }

            if (options.IsGetLocationDistrict)
            {
                await LoadLocationDistrictAsync(entity, logEntries);
                isSuccess = ValidateLocationDistrictLoaded(entity, isSuccess);
            }

            if (options.IsGetLocationWard)
            {
                await LoadLocationWardAsync(entity, logEntries);
                isSuccess = ValidateLocationWardLoaded(entity, isSuccess);
            }

            if (options.IsGetLocationType)
            {
                await LoadLocationTypeAsync(entity, logEntries);
                isSuccess = ValidateLocationTypeLoaded(entity, isSuccess);
            }

            if (options.IsGetSourceInventoryMovements)
            {
                await LoadSourceInventoryMovementsAsync(entity, logEntries);
                isSuccess = ValidateSourceInventoryMovementsLoaded(entity, isSuccess);
            }

            if (options.IsGetDestinationInventoryMovements)
            {
                await LoadDestinationInventoryMovementsAsync(entity, logEntries);
                isSuccess = ValidateDestinationInventoryMovementsLoaded(entity, isSuccess);
            }

            if (options.IsGetDisposeProducts)
            {
                await LoadDisposeProductsAsync(entity, logEntries);
                isSuccess = ValidateDisposeProductsLoaded(entity, isSuccess);
            }

            AddFinalLogEntry(logEntries, isSuccess, methodCall, "location");
            return _serviceResultFactory.CreateServiceResult(entity, logEntries, isSuccess);
        }

        public async Task<ServiceResults<LocationModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<LocationModel> entities, LocationNavigationOptions options,
            [CallerMemberName] string? methodCall = null)
        {
            List<JsonLogEntry> logEntries = [];
            var locationList = entities.ToList();
            bool isSuccess = true;

            if (options.IsGetInventory)
            {
                await LoadInventoriesAsync(locationList, logEntries);
                isSuccess = ValidateInventoriesLoaded(locationList, isSuccess);
            }

            if (options.IsGetLocationCity)
            {
                await LoadLocationCitiesAsync(locationList, logEntries);
                isSuccess = ValidateLocationCitiesLoaded(locationList, isSuccess);
            }

            if (options.IsGetLocationDistrict)
            {
                await LoadLocationDistrictsAsync(locationList, logEntries);
                isSuccess = ValidateLocationDistrictsLoaded(locationList, isSuccess);
            }

            if (options.IsGetLocationWard)
            {
                await LoadLocationWardsAsync(locationList, logEntries);
                isSuccess = ValidateLocationWardsLoaded(locationList, isSuccess);
            }

            if (options.IsGetLocationType)
            {
                await LoadLocationTypesAsync(locationList, logEntries);
                isSuccess = ValidateLocationTypesLoaded(locationList, isSuccess);
            }

            if (options.IsGetSourceInventoryMovements)
            {
                await LoadSourceInventoryMovementsAsync(locationList, logEntries);
                isSuccess = ValidateSourceInventoryMovementsLoaded(locationList, isSuccess);
            }

            if (options.IsGetDestinationInventoryMovements)
            {
                await LoadDestinationInventoryMovementsAsync(locationList, logEntries);
                isSuccess = ValidateDestinationInventoryMovementsLoaded(locationList, isSuccess);
            }

            if (options.IsGetDisposeProducts)
            {
                await LoadDisposeProductsAsync(locationList, logEntries);
                isSuccess = ValidateDisposeProductsLoaded(locationList, isSuccess);
            }

            AddFinalLogEntry(logEntries, isSuccess, methodCall, "locations");
            return _serviceResultFactory.CreateServiceResults(locationList, logEntries, isSuccess);
        }

        // NOTE: private method for navigation properties (single param)
        private async Task<ServiceResult<LocationCityModel>> TryLoadLocationCityAsync(uint locationCityId)
            => await _baseHelperReturnTEntityService.TryLoadEntityAsync(locationCityId, (id) => _baseCityDAO.GetSingleDataAsync(id.ToString()),
                () => new LocationCityModel(), nameof(TryLoadLocationCityAsync));

        private async Task<ServiceResult<LocationDistrictModel>> TryLoadLocationDistrictAsync(uint locationDistrictId)
            => await _baseHelperReturnTEntityService.TryLoadEntityAsync(locationDistrictId, (id) => _baseDistrictDAO.GetSingleDataAsync(id.ToString()),
                () => new LocationDistrictModel(), nameof(TryLoadLocationDistrictAsync));

        private async Task<ServiceResult<LocationWardModel>> TryLoadLocationWardAsync(uint locationWardId)
            => await _baseHelperReturnTEntityService.TryLoadEntityAsync(locationWardId, (id) => _baseWardDAO.GetSingleDataAsync(id.ToString()),
                () => new LocationWardModel(), nameof(TryLoadLocationWardAsync));
        private async Task<ServiceResult<LocationTypeModel>> TryLoadLocationTypeAsync(uint locationTypeId)
            => await _baseHelperReturnTEntityService.TryLoadEntityAsync(locationTypeId, (id) => _baseTypeDAO.GetSingleDataAsync(id.ToString()),
                () => new LocationTypeModel(), nameof(TryLoadLocationTypeAsync));

        private async Task<ServiceResult<InventoryModel>> TryLoadInventoryAsync(uint locationId)
            => await _baseHelperReturnTEntityService.TryLoadEntityAsync(locationId, _inventoryDAO.GetByLocationIdAsync, () => new InventoryModel(), nameof(TryLoadInventoryAsync));

        private async Task<ServiceResults<InventoryMovementModel>> TryLoadSourceInventoryMovementsAsync(uint locationId)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(locationId, (id) => _inventoryMovementDAO.GetBySourceLocationIdAsync(id),
                nameof(TryLoadSourceInventoryMovementsAsync));

        private async Task<ServiceResults<InventoryMovementModel>> TryLoadDestinationInventoryMovementsAsync(uint locationId)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(locationId, (id) => _inventoryMovementDAO.GetByDestinationLocationIdAsync(id),
                nameof(TryLoadDestinationInventoryMovementsAsync));

        private async Task<ServiceResults<DisposeProductModel>> TryLoadDisposeProductsAsync(uint locationId)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(locationId, (id) => _disposeProductDAO.GetByLocationIdAsync(id),
                nameof(TryLoadDisposeProductsAsync));

        // NOTE: private method for navigation properties (IEnumerable param)
        private async Task<IDictionary<uint, ServiceResult<LocationCityModel>>> TryLoadLocationCitiesAsync(IEnumerable<uint> locationCityIds)
            => await _baseHelperReturnTEntityService.TryLoadEntitiesAsync(locationCityIds, (ids) => _baseCityDAO.GetByInputsAsync(ids.Select(id => id.ToString())),
                () => new LocationCityModel(), (entity) => entity.LocationCityId, nameof(TryLoadLocationCitiesAsync));

        private async Task<IDictionary<uint, ServiceResult<LocationDistrictModel>>> TryLoadLocationDistrictsAsync(IEnumerable<uint> locationDistrictIds)
            => await _baseHelperReturnTEntityService.TryLoadEntitiesAsync(locationDistrictIds, (ids) => _baseDistrictDAO.GetByInputsAsync(ids.Select(id => id.ToString())),
                () => new LocationDistrictModel(), (entity) => entity.LocationDistrictId, nameof(TryLoadLocationDistrictsAsync));

        private async Task<IDictionary<uint, ServiceResult<LocationWardModel>>> TryLoadLocationWardsAsync(IEnumerable<uint> locationWardIds)
            => await _baseHelperReturnTEntityService.TryLoadEntitiesAsync(locationWardIds, (ids) => _baseWardDAO.GetByInputsAsync(ids.Select(id => id.ToString())),
                () => new LocationWardModel(), (entity) => entity.LocationWardId, nameof(TryLoadLocationWardsAsync));

        private async Task<IDictionary<uint, ServiceResult<LocationTypeModel>>> TryLoadLocationTypesAsync(IEnumerable<uint> locationTypeIds)
            => await _baseHelperReturnTEntityService.TryLoadEntitiesAsync(locationTypeIds, (ids) => _baseTypeDAO.GetByInputsAsync(ids.Select(id => id.ToString())),
                () => new LocationTypeModel(), (entity) => entity.LocationTypeId, nameof(TryLoadLocationTypesAsync));

        private async Task<IDictionary<uint, ServiceResult<InventoryModel>>> TryLoadInventoriesAsync(IEnumerable<uint> locationIds)
            => await _baseHelperReturnTEntityService.TryLoadEntitiesAsync(locationIds, (ids) => _inventoryDAO.GetByLocationIdsAsync(ids),
                () => new InventoryModel(), (entity) => entity.LocationId, nameof(TryLoadInventoriesAsync));

        private async Task<IDictionary<uint, ServiceResults<InventoryMovementModel>>> TryLoadSourceInventoryMovementsAsync(IEnumerable<uint> locationIds)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(locationIds, (ids) => _inventoryMovementDAO.GetBySourceLocationIdsAsync(ids),
                (entity) => entity.SourceLocationId, nameof(TryLoadSourceInventoryMovementsAsync));

        private async Task<IDictionary<uint, ServiceResults<InventoryMovementModel>>> TryLoadDestinationInventoryMovementsAsync(IEnumerable<uint> locationIds)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(locationIds, (ids) => _inventoryMovementDAO.GetByDestinationLocationIdsAsync(ids),
                (entity) => entity.DestinationLocationId, nameof(TryLoadDestinationInventoryMovementsAsync));

        private async Task<IDictionary<uint, ServiceResults<DisposeProductModel>>> TryLoadDisposeProductsAsync(IEnumerable<uint> locationIds)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(locationIds, (ids) => _disposeProductDAO.GetByLocationIdsAsync(ids),
                (entity) => entity.LocationId, nameof(TryLoadDisposeProductsAsync));

        // NOTE: Helper methods
        private async Task LoadLocationCityAsync(LocationModel location, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntityAsync(city => location.LocationCity = city,
                () => location.LocationCityId, TryLoadLocationCityAsync, logEntries, nameof(LoadLocationCityAsync));

        private async Task LoadLocationDistrictAsync(LocationModel location, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntityAsync(district => location.LocationDistrict = district,
                () => location.LocationDistrictId, TryLoadLocationDistrictAsync, logEntries, nameof(LoadLocationDistrictAsync));

        private async Task LoadLocationWardAsync(LocationModel location, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntityAsync(ward => location.LocationWard = ward,
                () => location.LocationWardId, TryLoadLocationWardAsync, logEntries, nameof(LoadLocationWardAsync));

        private async Task LoadLocationTypeAsync(LocationModel location, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntityAsync(type => location.LocationType = type,
                () => location.LocationTypeId, TryLoadLocationTypeAsync, logEntries, nameof(LoadLocationTypeAsync));

        private async Task LoadInventoryAsync(LocationModel location, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntityAsync(inventory => location.Inventory = inventory,
                () => location.LocationId, TryLoadInventoryAsync, logEntries, nameof(LoadInventoryAsync));

        private async Task LoadSourceInventoryMovementsAsync(LocationModel location, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(movements => location.SourceInventoryMovements = [.. movements],
                () => location.LocationId, TryLoadSourceInventoryMovementsAsync, logEntries, nameof(LoadSourceInventoryMovementsAsync));

        private async Task LoadDestinationInventoryMovementsAsync(LocationModel location, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(movements => location.DestinationInventoryMovements = [.. movements],
                () => location.LocationId, TryLoadDestinationInventoryMovementsAsync, logEntries, nameof(LoadDestinationInventoryMovementsAsync));

        private async Task LoadDisposeProductsAsync(LocationModel location, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(disposeProducts => location.DisposeProducts = [.. disposeProducts],
                () => location.LocationId, TryLoadDisposeProductsAsync, logEntries, nameof(LoadDisposeProductsAsync));

        private async Task LoadLocationCitiesAsync(IEnumerable<LocationModel> locations, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntitiesAsync(locations, (location, city) => location.LocationCity = city,
                location => location.LocationCityId, TryLoadLocationCitiesAsync, logEntries, nameof(LoadLocationCitiesAsync));

        private async Task LoadLocationDistrictsAsync(IEnumerable<LocationModel> locations, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntitiesAsync(locations, (location, district) => location.LocationDistrict = district,
                location => location.LocationDistrictId, TryLoadLocationDistrictsAsync, logEntries, nameof(LoadLocationDistrictsAsync));

        private async Task LoadLocationWardsAsync(IEnumerable<LocationModel> locations, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntitiesAsync(locations, (location, ward) => location.LocationWard = ward,
                location => location.LocationWardId, TryLoadLocationWardsAsync, logEntries, nameof(LoadLocationWardsAsync));

        private async Task LoadLocationTypesAsync(IEnumerable<LocationModel> locations, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntitiesAsync(locations, (location, type) => location.LocationType = type,
                location => location.LocationTypeId, TryLoadLocationTypesAsync, logEntries, nameof(LoadLocationTypesAsync));

        private async Task LoadInventoriesAsync(IEnumerable<LocationModel> locations, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntitiesAsync(locations, (location, inventory) => location.Inventory = inventory,
                location => location.LocationId, TryLoadInventoriesAsync, logEntries, nameof(LoadInventoriesAsync));

        private async Task LoadSourceInventoryMovementsAsync(IEnumerable<LocationModel> locations, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(locations, (location, movements) => location.SourceInventoryMovements = [.. movements],
                location => location.LocationId, TryLoadSourceInventoryMovementsAsync, logEntries, nameof(LoadSourceInventoryMovementsAsync));

        private async Task LoadDestinationInventoryMovementsAsync(IEnumerable<LocationModel> locations, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(locations, (location, movements) => location.DestinationInventoryMovements = [.. movements],
                location => location.LocationId, TryLoadDestinationInventoryMovementsAsync, logEntries, nameof(LoadDestinationInventoryMovementsAsync));

        private async Task LoadDisposeProductsAsync(IEnumerable<LocationModel> locations, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(locations, (location, disposeProducts) => location.DisposeProducts = [.. disposeProducts],
                location => location.LocationId, TryLoadDisposeProductsAsync, logEntries, nameof(LoadDisposeProductsAsync));

        // Validation helper methods for single entity
        private static bool ValidateInventoryLoaded(LocationModel location, bool currentSuccess)
            => currentSuccess && location.Inventory != null && location.Inventory.InventoryId != 0;

        private static bool ValidateLocationCityLoaded(LocationModel location, bool currentSuccess)
            => currentSuccess && location.LocationCity != null && location.LocationCity.LocationCityId != 0;

        private static bool ValidateLocationDistrictLoaded(LocationModel location, bool currentSuccess)
            => currentSuccess && location.LocationDistrict != null && location.LocationDistrict.LocationDistrictId != 0;

        private static bool ValidateLocationWardLoaded(LocationModel location, bool currentSuccess)
            => currentSuccess && location.LocationWard != null && location.LocationWard.LocationWardId != 0;

        private static bool ValidateLocationTypeLoaded(LocationModel location, bool currentSuccess)
            => currentSuccess && location.LocationType != null && location.LocationType.LocationTypeId != 0;

        private static bool ValidateSourceInventoryMovementsLoaded(LocationModel location, bool currentSuccess)
            => currentSuccess && location.SourceInventoryMovements != null && location.SourceInventoryMovements.Count > 0;

        private static bool ValidateDestinationInventoryMovementsLoaded(LocationModel location, bool currentSuccess)
            => currentSuccess && location.DestinationInventoryMovements != null && location.DestinationInventoryMovements.Count > 0;

        private static bool ValidateDisposeProductsLoaded(LocationModel location, bool currentSuccess)
            => currentSuccess && location.DisposeProducts != null && location.DisposeProducts.Count > 0;

        // Validation helper methods for multiple entities
        private static bool ValidateInventoriesLoaded(IEnumerable<LocationModel> locations, bool currentSuccess)
            => currentSuccess && !locations.Any(location => location.Inventory == null || location.Inventory.InventoryId == 0);

        private static bool ValidateLocationCitiesLoaded(IEnumerable<LocationModel> locations, bool currentSuccess)
            => currentSuccess && !locations.Any(location => location.LocationCity == null || location.LocationCity.LocationCityId == 0);

        private static bool ValidateLocationDistrictsLoaded(IEnumerable<LocationModel> locations, bool currentSuccess)
            => currentSuccess && !locations.Any(location => location.LocationDistrict == null || location.LocationDistrict.LocationDistrictId == 0);

        private static bool ValidateLocationWardsLoaded(IEnumerable<LocationModel> locations, bool currentSuccess)
            => currentSuccess && !locations.Any(location => location.LocationWard == null || location.LocationWard.LocationWardId == 0);

        private static bool ValidateLocationTypesLoaded(IEnumerable<LocationModel> locations, bool currentSuccess)
            => currentSuccess && !locations.Any(location => location.LocationType == null || location.LocationType.LocationTypeId == 0);

        private static bool ValidateSourceInventoryMovementsLoaded(IEnumerable<LocationModel> locations, bool currentSuccess)
            => currentSuccess && !locations.Any(location => location.SourceInventoryMovements == null || location.SourceInventoryMovements.Count == 0);

        private static bool ValidateDestinationInventoryMovementsLoaded(IEnumerable<LocationModel> locations, bool currentSuccess)
            => currentSuccess && !locations.Any(location => location.DestinationInventoryMovements == null || location.DestinationInventoryMovements.Count == 0);

        private static bool ValidateDisposeProductsLoaded(IEnumerable<LocationModel> locations, bool currentSuccess)
            => currentSuccess && !locations.Any(location => location.DisposeProducts == null || location.DisposeProducts.Count == 0);

        // Final log entry helper
        private void AddFinalLogEntry(List<JsonLogEntry> logEntries, bool isSuccess, string? methodCall, string entityName)
        {
            if (!isSuccess)
                logEntries.Add(_logger.JsonLogWarning<LocationModel, BaseReturnLocationService>($"One or more navigation properties could not be loaded for {entityName}.", methodCall: methodCall));
            else
                logEntries.Add(_logger.JsonLogInfo<LocationModel, BaseReturnLocationService>($"Successfully retrieved {entityName} with navigation properties.", methodCall: methodCall));
        }
    }
}
