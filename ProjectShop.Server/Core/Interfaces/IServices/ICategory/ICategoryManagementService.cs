using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;

namespace ProjectShop.Server.Core.Interfaces.IServices.ICategory
{
    public interface ICategoryManagementService<T> : IBaseService<T>, IBaseRelativeService<T> where T : class
    {

    }
}
