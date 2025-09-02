using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Application.Common;

namespace RestaurantManagement.Application.Services;

public class OrderDetailsService : IOrderDetailsService
{
    private readonly IOrderDetailsRepository _orderDetailsRepository;

    public OrderDetailsService(IOrderDetailsRepository orderDetailsRepository)
    {
        _orderDetailsRepository = orderDetailsRepository;
    }

    public async Task<ServiceResponse<IEnumerable<OrderDetailsDto>>> GetAllAsync()
    {
        try
        {
            var orderDetails = await _orderDetailsRepository.GetAllAsync();
            var orderDetailsDtos = orderDetails.Select(od => new OrderDetailsDto
            {
                M10Id = od.M10Id,
                M10Quantity = od.M10Quantity,
                M10Price = od.M10Price,
                M10TotalAmount = od.M10TotalAmount,
                M10PaymentStatus = od.M10PaymentStatus,
                M10StatusId = od.M10StatusId,
                M10OrderId = od.M10OrderId,
                M10DishId = od.M10DishId,
                M10CreatedAt = od.M10CreatedAt,
                M10CreatedBy = od.M10CreatedBy,
                M10UpdatedAt = od.M10UpdatedAt,
                M10UpdatedBy = od.M10UpdatedBy
            }).ToList();

            return ServiceResponse<IEnumerable<OrderDetailsDto>>.Success(orderDetailsDtos);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<OrderDetailsDto>>.Error($"Error retrieving order details: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<IEnumerable<OrderDetailsDto>>> GetByOrderIdAsync(Guid orderId)
    {
        try
        {
            var orderDetails = await _orderDetailsRepository.GetByOrderIdAsync(orderId);
            var orderDetailsDtos = orderDetails.Select(od => new OrderDetailsDto
            {
                M10Id = od.M10Id,
                M10Quantity = od.M10Quantity,
                M10Price = od.M10Price,
                M10TotalAmount = od.M10TotalAmount,
                M10PaymentStatus = od.M10PaymentStatus,
                M10StatusId = od.M10StatusId,
                M10OrderId = od.M10OrderId,
                M10DishId = od.M10DishId,
                M10CreatedAt = od.M10CreatedAt,
                M10CreatedBy = od.M10CreatedBy,
                M10UpdatedAt = od.M10UpdatedAt,
                M10UpdatedBy = od.M10UpdatedBy
            }).ToList();

            return ServiceResponse<IEnumerable<OrderDetailsDto>>.Success(orderDetailsDtos);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<OrderDetailsDto>>.Error($"Error retrieving order details by order ID: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<OrderDetailsDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var orderDetail = await _orderDetailsRepository.GetByIdAsync(id);
            if (orderDetail == null)
            {
                return ServiceResponse<OrderDetailsDto>.NotFound("Order detail not found");
            }

            var orderDetailDto = new OrderDetailsDto
            {
                M10Id = orderDetail.M10Id,
                M10Quantity = orderDetail.M10Quantity,
                M10Price = orderDetail.M10Price,
                M10TotalAmount = orderDetail.M10TotalAmount,
                M10PaymentStatus = orderDetail.M10PaymentStatus,
                M10StatusId = orderDetail.M10StatusId,
                M10OrderId = orderDetail.M10OrderId,
                M10DishId = orderDetail.M10DishId,
                M10CreatedAt = orderDetail.M10CreatedAt,
                M10CreatedBy = orderDetail.M10CreatedBy,
                M10UpdatedAt = orderDetail.M10UpdatedAt,
                M10UpdatedBy = orderDetail.M10UpdatedBy
            };

            return ServiceResponse<OrderDetailsDto>.Success(orderDetailDto);
        }
        catch (Exception ex)
        {
            return ServiceResponse<OrderDetailsDto>.Error($"Error retrieving order detail: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<OrderDetailsDto>> CreateAsync(CreateOrderDetailsDto dto)
    {
        try
        {
            var orderDetail = new OrderDetails
            {
                M10Quantity = dto.M10Quantity,
                M10Price = dto.M10Price,
                M10TotalAmount = dto.M10TotalAmount,
                M10PaymentStatus = dto.M10PaymentStatus,
                M10StatusId = dto.M10StatusId,
                M10OrderId = dto.M10OrderId,
                M10DishId = dto.M10DishId,
                M10CreatedBy = dto.M10CreatedBy,
                M10CreatedAt = DateTime.UtcNow
            };

            await _orderDetailsRepository.AddAsync(orderDetail);

            var orderDetailDto = new OrderDetailsDto
            {
                M10Id = orderDetail.M10Id,
                M10Quantity = orderDetail.M10Quantity,
                M10Price = orderDetail.M10Price,
                M10TotalAmount = orderDetail.M10TotalAmount,
                M10PaymentStatus = orderDetail.M10PaymentStatus,
                M10StatusId = orderDetail.M10StatusId,
                M10OrderId = orderDetail.M10OrderId,
                M10DishId = orderDetail.M10DishId,
                M10CreatedAt = orderDetail.M10CreatedAt,
                M10CreatedBy = orderDetail.M10CreatedBy
            };

            return ServiceResponse<OrderDetailsDto>.Created(orderDetailDto, "Order detail created successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<OrderDetailsDto>.Error($"Error creating order detail: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> UpdateAsync(Guid id, UpdateOrderDetailsDto dto)
    {
        try
        {
            var orderDetail = await _orderDetailsRepository.GetByIdAsync(id);
            if (orderDetail == null)
            {
                return ServiceResponse<object>.NotFound("Order detail not found");
            }

            orderDetail.M10Quantity = dto.M10Quantity ?? orderDetail.M10Quantity;
            orderDetail.M10Price = dto.M10Price ?? orderDetail.M10Price;
            orderDetail.M10TotalAmount = dto.M10TotalAmount ?? orderDetail.M10TotalAmount;
            orderDetail.M10PaymentStatus = dto.M10PaymentStatus ?? orderDetail.M10PaymentStatus;
            orderDetail.M10StatusId = dto.M10StatusId ?? orderDetail.M10StatusId;
            orderDetail.M10OrderId = dto.M10OrderId ?? orderDetail.M10OrderId;
            orderDetail.M10DishId = dto.M10DishId ?? orderDetail.M10DishId;
            orderDetail.M10UpdatedBy = dto.M10UpdatedBy;
            orderDetail.M10UpdatedAt = DateTime.UtcNow;

            var updated = await _orderDetailsRepository.UpdateAsync(orderDetail);
            if (!updated)
            {
                return ServiceResponse<object>.Error("Failed to update order detail");
            }

            return ServiceResponse<object>.Success(null, "Order detail updated successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error updating order detail: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> DeleteAsync(Guid id)
    {
        try
        {
            var deleted = await _orderDetailsRepository.DeleteAsync(id);
            if (!deleted)
            {
                return ServiceResponse<object>.NotFound("Order detail not found");
            }

            return ServiceResponse<object>.Success(null, "Order detail deleted successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error deleting order detail: {ex.Message}");
        }
    }
}