using RestaurantManagement.Domain.Entities;
namespace RestaurantManagement.Domain.Interfaces;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(Guid id);
    Task AddAsync(Restaurant entity);
    Task UpdateAsync(Restaurant enitity);
    Task<bool> DeleteAsync(Guid id);
}