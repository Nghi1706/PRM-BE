namespace RestaurantManagement.Application.DTOs;

public class StatusDto
{
    public int M02Id { get; set; }
    public required string M02Name { get; set; }
    public required string M02ForTable { get; set; }
    public bool M02IsActive { get; set; }
}

public class CreateStatusDto
{
    public required string M02Name { get; set; }
    public required string M02ForTable { get; set; }
    public bool M02IsActive { get; set; } = true;
}

public class UpdateStatusDto
{
    public string? M02Name { get; set; }
    public string? M02ForTable { get; set; }
    public bool? M02IsActive { get; set; }
}

