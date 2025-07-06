namespace RestaurantManagement.Domain.Entities;

public class Restaurant
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
}
