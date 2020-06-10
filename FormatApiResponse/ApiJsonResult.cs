using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FormatApiResponse
{
    class ApiJsonResult : ActionResult
    {
        private HttpStatusCode _statusCode;
        private object _result;
        public ApiJsonResult(object result, HttpStatusCode code)
        {
            _statusCode = code;
            _result = result;
        }
        public ApiJsonResult(HttpStatusCode code)
        {
            _statusCode = code;
        }
        public override Task ExecuteResultAsync(ActionContext context)
        {
            var httpContext = context.HttpContext;
            var response = httpContext.Response;

            response.ContentType = "application/json; charset=utf-8";
            response.StatusCode = (int)_statusCode;

            string jsonString = JsonConvert.SerializeObject(_result, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            response.WriteAsync(jsonString, Encoding.UTF8);
            
            return Task.CompletedTask;
        }
    }
}
