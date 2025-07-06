namespace RestaurantManagement.Domain.Entities;

public class StatusCategory
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool? IsActive { get; set; } = true;
    public DateTime? CreatedAt { get; set; }
    public Guid CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedByUser { get; set; }

    public void Update(string? name, string? description, bool? isActive, Guid? updatedBy)
    {
        if (name != null) Name = name;
        if (description != null) Description = description;
        if (isActive.HasValue) IsActive = isActive.Value;
        if (updatedBy.HasValue) UpdatedByUser = updatedBy.Value;
    }

}
