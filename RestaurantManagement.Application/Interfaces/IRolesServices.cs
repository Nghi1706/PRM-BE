using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Common;

namespace RestaurantManagement.Application.Interfaces;

public interface IRolesServices
{
    Task<ServiceResponse<IEnumerable<RolesDto>>> GetAllAsync();
    Task<ServiceResponse<RolesDto>> GetByIdAsync(int id);

    // Comment out Create, Update, Delete methods
    /*
    Task<ServiceResponse<RolesDto>> CreateAsync(CreateRolesDto role);
    Task<ServiceResponse<object>> UpdateAsync(int id, UpdateRolesDto role);
    Task<ServiceResponse<object>> DeleteAsync(int id);
    */
}