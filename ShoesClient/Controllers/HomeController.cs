using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoesClient.Data;
using ShoesClient.Models;
using ShoesClient.Services;

namespace ShoesClient.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShoesService _service;


        public HomeController(ILogger<HomeController> logger,IShoesService service,ICategoryService cservice):base(cservice)
        {

            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {

            var shoes = await _service.GetHome();
            return View(shoes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
