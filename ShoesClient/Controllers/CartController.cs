using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoesClient.Helper;
using ShoesClient.Models;
using ShoesClient.Services;
using ShoesClient.ViewModels;

namespace ShoesClient.Controllers
{
    public class CartController : BaseController
    {
        private readonly IShoesService _service;

        public CartController(IShoesService service,ICategoryService cservice) : base(cservice)
        {
            _service = service;

        }
        public ViewResult Index(string returnUrl) 
        {
            return View(GetCart());
        }

        public async Task<RedirectToActionResult> AddToCart(int shoesId,float size,int quantity)
        {
            var listShoes = await _service.GetAll();
            Shoes shoes = null;
            foreach (var shoesItem in listShoes)
            {
                if(shoesItem.ShoesId==shoesId)
                {
                    shoes = shoesItem;
                }
            }
            if(shoes!=null)
            {
                Cart cart = GetCart();
                cart.AddItem(shoes, quantity,size);
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }
        public async Task<RedirectToActionResult> RemoveFromCart(float cartLineID)
        {
           
                Cart cart = GetCart(); 
                cart.RemoveLine(cartLineID); 
                SaveCart(cart); 
            
            return RedirectToAction("Index");
        }

        private Cart GetCart() 
        { 
            Cart cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart") ?? new Cart(); 
            return cart; 
        }
        private void SaveCart(Cart cart) 
        { 
            HttpContext.Session.SetObjectAsJson("Cart", cart); 
        }
    }
}