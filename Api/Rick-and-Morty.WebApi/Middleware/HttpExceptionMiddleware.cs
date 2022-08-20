using Microsoft.AspNetCore.Http;
using Rick_and_Morty.Application.Exceptions;
using Rick_and_Morty.Application.Responses;
using System.Threading.Tasks;

namespace Rick_and_Morty.WebApi.Middleware
{
    public class HttpExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public HttpExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);
            }
            catch (HttpException httpException)
            {
                context.Response.StatusCode = httpException.StatusCode;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = httpException.StatusCode,
                    Message = httpException.Message
                }.ToString());
            }
        }
    }
}
