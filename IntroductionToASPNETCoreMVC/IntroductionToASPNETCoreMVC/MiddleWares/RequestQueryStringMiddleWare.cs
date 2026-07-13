using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroductionToASPNETCoreMVC.MiddleWares
{
    public class RequestQueryStringMiddleWare
    {
        private RequestDelegate next;

        public RequestQueryStringMiddleWare()
        {
        }

        public RequestQueryStringMiddleWare(RequestDelegate nextDelegate)
        {
            next = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Get
                        && context.Request.Query["iscertified"] == "true")
            {
                await context.Response.WriteAsync("Message from Class-based Middleware \n");
            }

            if (next != null)
            {
                await next(context);
            }


        }
    }
}
