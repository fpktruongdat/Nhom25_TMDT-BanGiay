using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ShopShoesShared
{
    public static class ApiResultExtensions
    {
        public static IActionResult ErrorResult(this ControllerBase controller, int errorCode, string errorMessage)
        {
            return JsonResult(new ApiResponse<object>(errorCode, errorMessage), HttpStatusCode.BadRequest);
        }
        public static IActionResult ErrorResult(this ControllerBase controller, int errorCode, string errorMessage,HttpStatusCode statusCode)
        {
            return JsonResult(new ApiResponse<object>(errorCode,errorMessage),statusCode);
        }
        public static IActionResult OkResult(this ControllerBase controller, object result)
        {
            return JsonResult(new ApiResponse<object>(result));
        }
        public static IActionResult OkResult(this ControllerBase controller)
        {
            return JsonResult(new ApiResponse<object>(true));
        }
        public static IActionResult OkResult<T>(this ControllerBase controller, T result)
        {
            return JsonResult(new ApiResponse<T>(result));
        }
        private static IActionResult JsonResult(object result, HttpStatusCode statusCode= HttpStatusCode.OK)
        {
            return new ApiJsonResult(result, statusCode);
        }
    }
}
