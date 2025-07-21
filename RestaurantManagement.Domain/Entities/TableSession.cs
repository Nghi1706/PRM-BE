namespace RestaurantManagement.Domain.Entities;

public class TableSession
{
    public Guid Id { get; set; }
    public Guid TableId { get; set; }
    public Guid OrderId { get; set; }
    public string QRToken { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? CreatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedByUser { get; set; }
}