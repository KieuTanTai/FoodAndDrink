using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    /// <summary>
    /// Person repository interface with specific query methods
    /// </summary>
    public interface IPersonRepository : IRepository<Person>,
        IBaseGetByCreatedAndLastUpdatedDate<Person>,
        IBaseExplicitLoadRepository<Person, PersonNavigationOptions>
    {
        // Query by AccountId
        Task<Person?> GetByAccountIdAsync(uint accountId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Person>> GetByAccountIdsAsync(IEnumerable<uint> accountIds, CancellationToken cancellationToken = default);

        // Query by Email
        Task<Person?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<IEnumerable<Person>> GetByEmailsAsync(IEnumerable<string> emails, CancellationToken cancellationToken = default);

        // Query by Phone
        Task<Person?> GetByPhoneAsync(string phone, CancellationToken cancellationToken = default);
        Task<IEnumerable<Person>> GetByPhonesAsync(IEnumerable<string> phones, CancellationToken cancellationToken = default);

        // Query by Name
        Task<IEnumerable<Person>> SearchByNameContainsAsync(string searchTerm, uint fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<Person>> SearchManyByNamesAsync(IEnumerable<string> names, uint fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);
        Task<Person?> GetByFullNameAsync(string name, CancellationToken cancellationToken = default);

        // Query by Gender
        Task<IEnumerable<Person>> GetByGenderAsync(bool isMale, uint fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);

        // Query by Status
        Task<IEnumerable<Person>> GetByStatusAsync(bool? status, uint fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);
    }
}
