using ShoesClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesClient.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
    }
}
