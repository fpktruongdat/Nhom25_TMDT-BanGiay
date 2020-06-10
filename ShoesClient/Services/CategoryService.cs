using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoesClient.Infrastructure;
using ShoesClient.Models;

namespace ShoesClient.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpClient _httpClient;
        private readonly string _baseUrl;
        public CategoryService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = "http://localhost:5001/api/Categories";
        }
        public async Task<IEnumerable<Category>> GetAll()
        {
            var uri = _baseUrl;

            return await _httpClient.GetListAsync<Category>(uri);
        }
    }
}
