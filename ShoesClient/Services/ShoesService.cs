using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShoesClient.Infrastructure;
using ShoesClient.Models;

namespace ShoesClient.Services
{
    public class ShoesService : IShoesService
    {
        private readonly IHttpClient _httpClient;
        private readonly string _baseUrl;
        public ShoesService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = "http://localhost:5001/api/Shoes";
        }
        public async Task CreateShoes(Shoes shoes)
        {
            var uri = _baseUrl;

            await _httpClient.PostAsync(uri, shoes);

           
        }

        public async Task DeleteShoes(int id)
        {
            var uri = _baseUrl + $"/{id}";

            await _httpClient.DeleteAsync(uri);
        }

        public async Task<IEnumerable<Shoes>> GetAll()
        {
            var uri = _baseUrl;

            return await _httpClient.GetListAsync<Shoes>(uri);
           

        }
        public async Task<IEnumerable<Shoes>> GetHome()
        {
            var uri = _baseUrl+"/GetHome";

            return await _httpClient.GetListAsync<Shoes>(uri);

        }
        public async Task<IEnumerable<Shoes>> GetShoesByCategory(int CategoryId)
        {
            var uri = _baseUrl + "/GetShoesByCategory/"+CategoryId;

            return await _httpClient.GetListAsync<Shoes>(uri);

        }

        public async Task<Shoes> GetShoes(int id)
        {
            var uri = _baseUrl + $"/{id}";

            return await _httpClient.GetAsync<Shoes>(uri);
        }

        public async Task UpdateShoes(int id, Shoes shoes)
        {
            var uri = _baseUrl + $"/{id}";

            await _httpClient.PutAsync(uri, shoes);
        }
    }
}
