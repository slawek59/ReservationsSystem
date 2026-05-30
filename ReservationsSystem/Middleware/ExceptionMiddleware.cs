using System.Text.Json;
using System.Text.Json.Serialization;

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
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			var jsonResponse = JsonSerializer.Serialize(new
			{
				Status = 500,
				Message = "An unexpected error occurred.",
			});
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = 500;

			return context.Response.WriteAsync(jsonResponse);
		}
	}
}
///TODO defaultowe wyjątki najpierw zobaczmy jak dzialają tutaj, potem napisze swoje