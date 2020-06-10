using ShoesClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesClient.Services
{
   public interface IShoesService
    {
        Task<IEnumerable<Shoes>> GetAll();
        Task<IEnumerable<Shoes>> GetHome();
        Task<IEnumerable<Shoes>> GetShoesByCategory(int CategoryId);
        Task CreateShoes(Shoes shoes);
        Task<Shoes> GetShoes(int id);
        Task UpdateShoes(int id, Shoes movie);
        Task DeleteShoes(int id);
    }
}
