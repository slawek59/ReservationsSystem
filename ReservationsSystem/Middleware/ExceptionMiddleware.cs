using System.Net;
using System.Text.Json;

namespace ReservationsSystem.API.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception)
			{
				await HandleExceptionAsync(context);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context)
		{
			var statusCode = (int)HttpStatusCode.InternalServerError;
				
			var jsonResponse = JsonSerializer.Serialize(new
			{
				Status = statusCode,
				Message = "An unexpected error occurred.",
			});
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = statusCode;

			await context.Response.WriteAsync(jsonResponse);
		}
	}
}