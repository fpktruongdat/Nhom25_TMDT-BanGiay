using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoesClient.Helper;
using ShoesClient.Models;
using ShoesClient.Services;
using ShoesClient.ViewModels;

namespace ShoesClient.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        public OrderController(IOrderService orderService,ICartService cartService,ICategoryService cservice) : base(cservice)
        {
            _orderService = orderService;
            _cartService = cartService;
            
            
        }
        public async Task<IActionResult> Index()
        {
            var userInfo = HttpContext.Session.GetObjectFromJson<UserLoginModel>("UserInfo");
            if (userInfo == default)
            {
                return View("404");
            }

            var order_res = await _orderService.GetOrderByUser(userInfo.sub);
            
            return View(order_res);
            
        }
        public IActionResult Create()
        {
            var userInfo = HttpContext.Session.GetObjectFromJson<UserLoginModel>("UserInfo");
            var cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart");
            if (cart!=default && userInfo != default )
            {
                return View();
            }
            else
            {
                return View("404");
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderViewModel order)
        {

            var order_res = await _orderService.CreateOrder(order);

            var cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart");
            foreach (var item in cart.Lines)
            {
                CartLineViewModel cartline = new CartLineViewModel();

                cartline.ShoesId = item.Shoes.ShoesId;
                cartline.Quantity = item.Quantity;
                cartline.Size = item.Size;
                cartline.OrderId = order_res.OrderId;


                var cartline_res = await _cartService.CreateCartLine(cartline);
                if (cartline_res == null)
                {
                    return View("404");
                }

            }
            HttpContext.Session.Remove("Cart");
            return Redirect("Index");
        }
    }
}