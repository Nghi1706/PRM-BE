using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Application.Common;

namespace RestaurantManagement.Application.Services;

public class OrdersService : IOrdersService
{
    private readonly IOrdersRepository _ordersRepository;

    public OrdersService(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task<ServiceResponse<IEnumerable<OrdersDto>>> GetAllAsync()
    {
        try
        {
            var orders = await _ordersRepository.GetAllAsync();
            var orderDtos = orders.Select(order => new OrdersDto
            {
                M09Id = order.M09Id,
                M09TotalAmount = order.M09TotalAmount,
                M09FinalAmount = order.M09FinalAmount,
                M09PaymentType = order.M09PaymentType,
                M09TableId = order.M09TableId,
                M09VoucherId = order.M09VoucherId,
                M09CreatedAt = order.M09CreatedAt,
                M09CreatedBy = order.M09CreatedBy,
                M09UpdatedAt = order.M09UpdatedAt,
                M09UpdatedBy = order.M09UpdatedBy
            }).ToList();

            return ServiceResponse<IEnumerable<OrdersDto>>.Success(orderDtos);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<OrdersDto>>.Error($"Error retrieving orders: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<IEnumerable<OrdersDto>>> GetByTableIdAsync(int tableId)
    {
        try
        {
            var orders = await _ordersRepository.GetByTableIdAsync(tableId);
            var orderDtos = orders.Select(order => new OrdersDto
            {
                M09Id = order.M09Id,
                M09TotalAmount = order.M09TotalAmount,
                M09FinalAmount = order.M09FinalAmount,
                M09PaymentType = order.M09PaymentType,
                M09TableId = order.M09TableId,
                M09VoucherId = order.M09VoucherId,
                M09CreatedAt = order.M09CreatedAt,
                M09CreatedBy = order.M09CreatedBy,
                M09UpdatedAt = order.M09UpdatedAt,
                M09UpdatedBy = order.M09UpdatedBy
            }).ToList();

            return ServiceResponse<IEnumerable<OrdersDto>>.Success(orderDtos);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<OrdersDto>>.Error($"Error retrieving orders by table: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<OrdersDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var order = await _ordersRepository.GetByIdAsync(id);
            if (order == null)
            {
                return ServiceResponse<OrdersDto>.NotFound("Order not found");
            }

            var orderDto = new OrdersDto
            {
                M09Id = order.M09Id,
                M09TotalAmount = order.M09TotalAmount,
                M09FinalAmount = order.M09FinalAmount,
                M09PaymentType = order.M09PaymentType,
                M09TableId = order.M09TableId,
                M09VoucherId = order.M09VoucherId,
                M09CreatedAt = order.M09CreatedAt,
                M09CreatedBy = order.M09CreatedBy,
                M09UpdatedAt = order.M09UpdatedAt,
                M09UpdatedBy = order.M09UpdatedBy
            };

            return ServiceResponse<OrdersDto>.Success(orderDto);
        }
        catch (Exception ex)
        {
            return ServiceResponse<OrdersDto>.Error($"Error retrieving order: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<OrdersDto>> CreateAsync(CreateOrdersDto dto)
    {
        try
        {
            var order = new Orders
            {
                M09TotalAmount = dto.M09TotalAmount,
                M09FinalAmount = dto.M09FinalAmount,
                M09PaymentType = dto.M09PaymentType,
                M09TableId = dto.M09TableId,
                M09VoucherId = dto.M09VoucherId,
                M09CreatedBy = dto.M09CreatedBy,
                M09CreatedAt = DateTime.UtcNow
            };

            await _ordersRepository.AddAsync(order);

            var orderDto = new OrdersDto
            {
                M09Id = order.M09Id,
                M09TotalAmount = order.M09TotalAmount,
                M09FinalAmount = order.M09FinalAmount,
                M09PaymentType = order.M09PaymentType,
                M09TableId = order.M09TableId,
                M09VoucherId = order.M09VoucherId,
                M09CreatedAt = order.M09CreatedAt,
                M09CreatedBy = order.M09CreatedBy
            };

            return ServiceResponse<OrdersDto>.Created(orderDto, "Order created successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<OrdersDto>.Error($"Error creating order: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> UpdateAsync(Guid id, UpdateOrdersDto dto)
    {
        try
        {
            var order = await _ordersRepository.GetByIdAsync(id);
            if (order == null)
            {
                return ServiceResponse<object>.NotFound("Order not found");
            }

            order.M09TotalAmount = dto.M09TotalAmount ?? order.M09TotalAmount;
            order.M09FinalAmount = dto.M09FinalAmount ?? order.M09FinalAmount;
            order.M09PaymentType = dto.M09PaymentType ?? order.M09PaymentType;
            order.M09TableId = dto.M09TableId ?? order.M09TableId;
            order.M09VoucherId = dto.M09VoucherId ?? order.M09VoucherId;
            order.M09UpdatedBy = dto.M09UpdatedBy;
            order.M09UpdatedAt = DateTime.UtcNow;

            await _ordersRepository.UpdateAsync(order);

            return ServiceResponse<object>.Success(null, "Order updated successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error updating order: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> DeleteAsync(Guid id)
    {
        try
        {
            var deleted = await _ordersRepository.DeleteAsync(id);
            if (!deleted)
            {
                return ServiceResponse<object>.NotFound("Order not found");
            }

            return ServiceResponse<object>.Success(null, "Order deleted successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error deleting order: {ex.Message}");
        }
    }
}