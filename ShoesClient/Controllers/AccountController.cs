using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoesClient.Helper;
using ShoesClient.Models;
using ShoesClient.Services;
using ShoesClient.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ShoesClient.Controllers
{
    public class AccountController:BaseController
    {
        public AccountController(ICategoryService cservice) : base(cservice)
        {

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string UserName, string Password)
        {
            var uri = "http://localhost:5003/connect/token";
            
            
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent("client"), "client_id");
                content.Add(new StringContent("secret"), "client_secret");
                content.Add(new StringContent("password"), "grant_type");
                content.Add(new StringContent(UserName), "username");
                content.Add(new StringContent(Password), "password");

                using (var response = await httpClient.PostAsync(uri, content))
                {
                    if(response.StatusCode==HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var auth = JsonConvert.DeserializeObject<AuthenticateModel>(apiResponse);
                        HttpContext.Session.SetObjectAsJson("JWToken", auth);

                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.access_token);

                        var responseString = await httpClient.GetStringAsync("http://localhost:5003/connect/userinfo");
                        var user =  JsonConvert.DeserializeObject<UserLoginModel>(responseString);

                        HttpContext.Session.SetObjectAsJson("UserInfo", user);
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        ViewData["message"] = "Incorrect Username/Password";
                        return View();
                    }
                   
                }
            }
            
            
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserInfo");
            HttpContext.Session.Remove("JWToken");
            return RedirectToAction("Index","Home");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel user)
        {
            using (var httpClient = new HttpClient())
            {
                var uri = "http://localhost:5003/api/account";
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(uri, content))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ViewData["message"] = "Register Success";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewData["message"] = "Register Fail";
                        return View();
                    }

                }
            }
            
        }
    }
}
