using ShoesClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesClient.Services
{
   public interface ICartService
    {
        Task<CartLineViewModel> CreateCartLine(CartLineViewModel cartline);
    }
}
