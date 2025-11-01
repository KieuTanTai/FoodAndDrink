using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IServices._IBase;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Application.Services._BaseServices
{
    public class BasePasswordMappingServices : IBasePasswordMappingServices
    {
        private readonly IHashPassword _hashPassword;
        public BasePasswordMappingServices(IHashPassword hashPassword)
        {
            _hashPassword = hashPassword;
        }
        public async Task<List<Account>> HelperPasswordMappingAsync(List<Account> entities, List<string> newPasswords, CancellationToken cancellationToken)
        {
            if (entities.Count != newPasswords.Count)
                throw new ArgumentException("The number of accounts does not match the number of new passwords.");
            int count = entities.Count;
            for (int i = 0; i < count; i++)
            {
                if (await _hashPassword.ComparePasswordsAsync(entities[i].Password, newPasswords[i], cancellationToken))
                    continue;
                if (!await _hashPassword.IsPasswordHashedAsync(newPasswords[i], cancellationToken))
                    entities[i].Password = await _hashPassword.HashPasswordAsync(newPasswords[i], cancellationToken);
                else
                    entities[i].Password = newPasswords[i];
            }
            return entities;
        }
    }
}