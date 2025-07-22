using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Shared.Enums
{
    public enum TableStatus
    {
        Empty = 1,
        Booked = 2,
        Cleaning = 3
    }

    public enum OrderStatus
    {
        Created = 1,
        Ordered = 2,
        InProgress = 3,
        Paid = 4,
        Done = 5
    }

    public enum DishStatus
    {
        Ordered = 1,
        InKitchen = 2,
        Ready = 3,
        Served = 4,
        OutOfStock = 5,
        Cancelled = 6
    }
}
