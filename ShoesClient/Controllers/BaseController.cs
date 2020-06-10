using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShoesClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesClient.Controllers
{
    public abstract class BaseController:Controller
    {
        private readonly ICategoryService _cservice;
        public BaseController(ICategoryService cservice)
        {
            _cservice = cservice;
        }
        public async override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var categories = await _cservice.GetAll();
            ViewBag.categories = categories;
            base.OnActionExecuting(filterContext);
        }
    }
}
