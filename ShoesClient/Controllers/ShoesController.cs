using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoesClient.Models;
using ShoesClient.Services;
using ShoesClient.ViewModels;

namespace ShoesClient.Controllers
{
    public class ShoesController : BaseController
    {
        private readonly IShoesService _service;
        public ShoesController(IShoesService service, ICategoryService cservice) : base(cservice)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            var shoes = await _service.GetShoes(id);
            if (shoes == null)
            {
                return View("404");
            }
            DetailViewModel detail = new DetailViewModel();
            detail.shoes = shoes;
            return View(detail);
        }
        public async Task<IActionResult> List()
        {

            var shoes = await _service.GetAll();
            return View(shoes);
        }
        public async Task<IActionResult> ShoesByCategory(int categoryId)
        {

            var shoes = await _service.GetShoesByCategory(categoryId);
            return View(shoes);
        }
    }
}