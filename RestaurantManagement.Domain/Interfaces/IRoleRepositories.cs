using RestaurantManagement.Domain.Entities;
namespace RestaurantManagement.Domain.Interfaces;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAllAsync();
    Task<Role?> GetByIdAsync(Guid id);
    Task AddAsync(Role entity);
    Task UpdateAsync(Role enitity);
    Task<bool> DeleteAsync(Guid id);
}