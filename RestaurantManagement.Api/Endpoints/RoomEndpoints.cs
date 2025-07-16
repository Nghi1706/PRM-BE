using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;

public static class RoomEndpoints
{
    public static RouteGroupBuilder MapRoomEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromQuery] Guid restaurantId, [FromServices] IRoomService service) =>
        {
            var data = await service.GetAllAsync(restaurantId);
            return Results.Ok(ApiResponse.Success(data));
        });

        group.MapGet("/{id:guid}", async (Guid id, [FromServices] IRoomService service) =>
        {
            var room = await service.GetByIdAsync(id);
            return room != null
                ? Results.Ok(ApiResponse.Success(room))
                : Results.NotFound(ApiResponse.Fail("Không tìm thấy phòng"));
        });

        group.MapPost("/", async (CreateRoomDto dto, [FromServices] IRoomService service) =>
        {
            var created = await service.CreateAsync(dto);
            return Results.Created($"api/rooms/{created.Id}", ApiResponse.Success(created));
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateRoomDto dto, [FromServices] IRoomService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            return updated
                ? Results.Ok(ApiResponse.Success("Updated successfully"))
                : Results.NotFound(ApiResponse.Fail("Room not found"));
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IRoomService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted
                ? Results.Ok(ApiResponse.Success("Deleted successfully"))
                : Results.NotFound(ApiResponse.Fail("Room not found"));
        });

        return group;
    }
}