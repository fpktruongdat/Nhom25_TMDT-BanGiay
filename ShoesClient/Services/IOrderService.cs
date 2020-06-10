using ShoesClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesClient.Services
{
   public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> GetAll();
        Task<OrderViewModel> CreateOrder(OrderViewModel order);
        Task<IEnumerable<OrderViewModel>> GetOrderByUser(string UserId);
    }
}
