using System;

namespace StartupInvestorMatcher.API{

public class HeaderAuthenticationMiddleware
{
   private const string MY_SECRET_VALUE = "1!";
   private readonly RequestDelegate _next;
   public HeaderAuthenticationMiddleware(RequestDelegate next) {
      _next = next;
   }

   public async Task InvokeAsync(HttpContext context) {
      string authHeaderValue = context.Request.Headers["X-My-Request_Header"];

      if (string.IsNullOrWhiteSpace(authHeaderValue)) {
         context.Response.StatusCode = 401;
         await context.Response.WriteAsync("Auth Header value was not provided");
         return;
      }

      if (!string.Equals(authHeaderValue, MY_SECRET_VALUE)) {
         context.Response.StatusCode = 401;
         await context.Response.WriteAsync("Auth Header value was incorrect");
         return;
      }

      await _next(context);
   }
}

public static class HeaderAuthenticationMiddlewareExtensions {
   public static IApplicationBuilder UseHeaderAuthenticationMiddleware(this IApplicationBuilder builder) {
      return builder.UseMiddleware<HeaderAuthenticationMiddleware>();
   }
}}

