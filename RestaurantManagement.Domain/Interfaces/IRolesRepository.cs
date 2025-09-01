using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.Interfaces
{
    public interface IRolesRepository
    {
        Task<IEnumerable<Entities.Roles>> GetAllAsync();
        Task<Entities.Roles?> GetByIdAsync(int id);
        Task AddAsync(Entities.Roles entity);
        Task UpdateAsync(Entities.Roles entity);
        Task<bool> DeleteAsync(int id);
    }
}
