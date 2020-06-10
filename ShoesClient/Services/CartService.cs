using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShoesClient.Helper;
using ShoesClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ShoesClient.Services
{
    public class CartService:ICartService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly HttpContext _httpContext;

        public CartService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContext = httpContextAccessor.HttpContext;
            _baseUrl = "http://localhost:5001/api/CartLines";
        }
       
        public async Task<CartLineViewModel> CreateCartLine(CartLineViewModel cartline)
        {
            var uri = _baseUrl;
            var content = new StringContent(JsonConvert.SerializeObject(cartline), Encoding.UTF8, "application/json");

            await AttachTokenToHttpMessage();

            var response = await _httpClient.PostAsync(uri, content);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                var cartline_res = JsonConvert.DeserializeObject<CartLineViewModel>(apiResponse);

                return cartline_res;
            }
            else
            {
                return null;
            }

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
