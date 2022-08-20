using Microsoft.AspNetCore.Builder;
using Rick_and_Morty.WebApi.Middleware;

namespace Rick_and_Morty.WebApi.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseHttpException(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HttpExceptionMiddleware>();
        }
    }
}
