namespace RestaurantManagement.Application.DTOs;

public class UpdateStatusDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? Code { get; set; }
    public bool? IsActive { get; set; }
    public Guid? UpdatedByUser { get; set; }
}
