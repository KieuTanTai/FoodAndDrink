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
        Task<IEnumerable<Person>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default);
        Task<Person?> GetByFullNameAsync(string name, CancellationToken cancellationToken = default);

        // Query by Gender
        Task<IEnumerable<Person>> GetByGenderAsync(bool isMale, CancellationToken cancellationToken = default);

        // Query by Status
        Task<IEnumerable<Person>> GetByStatusAsync(bool? status, CancellationToken cancellationToken = default);

        // Query with navigation properties
        Task<Person?> GetByIdWithNavigationAsync(uint personId, CancellationToken cancellationToken = default);
    }
}
