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
using ShoesClient.Helper;
using ShoesClient.Infrastructure;
using ShoesClient.ViewModels;

namespace ShoesClient.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly HttpContext _httpContext;
        public OrderService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContext = httpContextAccessor.HttpContext;
            _baseUrl = "http://localhost:5001/api/Orders";
        }
        public async Task<IEnumerable<OrderViewModel>> GetOrderByUser(string UserId)
        {
            var uri = _baseUrl+ "/GetOrderByUser/"+UserId;
            
            await AttachTokenToHttpMessage();

            var response = await _httpClient.GetAsync(uri);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                var order_res = JsonConvert.DeserializeObject< IEnumerable<OrderViewModel>>(apiResponse);

                return order_res;
            }
            else
            {
                return null;
            }

        }
        public async Task<OrderViewModel> CreateOrder(OrderViewModel order)
        {
            var uri = _baseUrl;
            var content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");

            await AttachTokenToHttpMessage();

            var response = await _httpClient.PostAsync(uri, content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                var order_res = JsonConvert.DeserializeObject<OrderViewModel>(apiResponse);

                return order_res;
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<OrderViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        

        private async Task AttachTokenToHttpMessage()
        {
            var auth = _httpContext.Session.GetObjectFromJson<AuthenticateModel>("JWToken");
            if (auth != default)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.access_token);
            }

        }
    }
}
