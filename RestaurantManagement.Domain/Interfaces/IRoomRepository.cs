using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;
public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAllAsync(Guid restaurantId);
    Task<Room?> GetByIdAsync(Guid id);
    Task<Room> AddAsync(Room entity);
    Task<bool> UpdateAsync(Room entity);
    Task<bool> DeleteAsync(Guid id);
}