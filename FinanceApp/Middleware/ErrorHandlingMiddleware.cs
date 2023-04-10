﻿using FinanceApp.Exceptions;

namespace FinanceApp.Middleware
{
    public class ErrorHandlingMiddleware:IMiddleware
    {
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(ILogger logger)
        {
            _logger= logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException badRequestException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestException.Message);

            }

         





        }
    }
}
