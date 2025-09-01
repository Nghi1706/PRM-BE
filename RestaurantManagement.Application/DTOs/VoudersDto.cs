namespace RestaurantManagement.Application.DTOs;

public class VoucherDto
{
    public Guid M03Id { get; set; }
    public string M03Code { get; set; } = string.Empty;
    public string? M03Description { get; set; }
    public string M03DiscountValue { get; set; } = string.Empty;
    public bool M03IsActive { get; set; }
    public decimal? M03MinOrderValue { get; set; }
    public decimal? M03MaxDiscount { get; set; }
    public DateTime M03FromDate { get; set; }
    public DateTime M03ToDate { get; set; }
    public int M03UsageLimit { get; set; }
    public Guid M03RestaurantId { get; set; }
    public DateTime? M03CreatedAt { get; set; }
    public Guid? M03CreatedBy { get; set; }
    public DateTime? M03UpdatedAt { get; set; }
    public Guid? M03UpdatedBy { get; set; }
}

public class CreateVoucherDto
{
    public required string M03Code { get; set; }
    public string? M03Description { get; set; }
    public required string M03DiscountValue { get; set; }
    public bool M03IsActive { get; set; } = true;
    public decimal? M03MinOrderValue { get; set; }
    public decimal? M03MaxDiscount { get; set; } = 0.1m;
    public required DateTime M03FromDate { get; set; }
    public required DateTime M03ToDate { get; set; }
    public int M03UsageLimit { get; set; } = 10;
    public required Guid M03RestaurantId { get; set; }
    public Guid? M03CreatedBy { get; set; }
}

public class UpdateVoucherDto
{
    public string? M03Code { get; set; }
    public string? M03Description { get; set; }
    public string? M03DiscountValue { get; set; }
    public bool? M03IsActive { get; set; }
    public decimal? M03MinOrderValue { get; set; }
    public decimal? M03MaxDiscount { get; set; }
    public DateTime? M03FromDate { get; set; }
    public DateTime? M03ToDate { get; set; }
    public int? M03UsageLimit { get; set; }
    public Guid? M03RestaurantId { get; set; }
    public Guid? M03UpdatedBy { get; set; }
}