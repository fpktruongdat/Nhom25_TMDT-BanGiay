using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoesClient.Helper;
using ShoesClient.Models;
using ShoesClient.Services;
using ShoesClient.ViewModels;
using Stripe;

namespace ShoesClient.Controllers
{
    public class CheckoutController : BaseController
    {
        public CheckoutController(ICategoryService cservice) : base(cservice)
        {
            StripeConfiguration.ApiKey = "sk_test_9FSpuxl0RVW2spTfhMofE71v00jNNJmUDs";
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Charge()
        {
            var userInfo = HttpContext.Session.GetObjectFromJson<UserLoginModel>("UserInfo");
            if (userInfo == default)
            {
                return View("404");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Charge(ChargeViewModel model)
        {
            var userInfo = HttpContext.Session.GetObjectFromJson<UserLoginModel>("UserInfo");
            var cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart");

            if (userInfo == default || cart==default)
            {
                return View("404");
            }
            var token_options = new TokenCreateOptions
            {
                Card = new CreditCardOptions
                {
                    Number = model.Number,
                    ExpYear = model.ExpYear,
                    ExpMonth = model.ExpMonth,
                    Cvc = model.Cvc
                }
            };

            var token_service = new TokenService();
            Token stripeToken = token_service.Create(token_options);
            


            

            var charge_options = new ChargeCreateOptions
            {
                Amount = Decimal.ToInt64(cart.ComputeTotalValue())*100,
                Currency = "usd",
                Source = stripeToken.Id,
                Description = "Checkout by "+userInfo.name,
            };
            var charge_service = new ChargeService();
            charge_service.Create(charge_options);

            return RedirectToAction("Create","Order");
        }
    }
}