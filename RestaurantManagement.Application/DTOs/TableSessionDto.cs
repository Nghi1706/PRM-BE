namespace RestaurantManagement.Application.DTOs;

public class TableSessionDto
{
    public Guid Id { get; set; }
    public Guid TableId { get; set; }
    public Guid OrderId { get; set; }
    public string QRToken { get; set; } = default!;
    public bool IsActive { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedByUser { get; set; }
}

public class CreateTableSessionDto
{
    public Guid TableId { get; set; }
    public Guid OrderId { get; set; }
    public string QRToken { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    public Guid? CreatedByUser { get; set; }
}

public class UpdateTableSessionDto
{
    public bool? IsActive { get; set; }
    public DateTime? EndedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedByUser { get; set; }
}