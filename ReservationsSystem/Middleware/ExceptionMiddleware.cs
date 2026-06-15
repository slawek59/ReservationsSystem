using ReservationsSystem.Application.Exceptions;
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
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception ex)
		{

			var statusCode = ex switch
			{
				NotFoundException => HttpStatusCode.NotFound,
				BadRequestException => HttpStatusCode.BadRequest,
				ValidationException => HttpStatusCode.BadRequest,
				_ => HttpStatusCode.InternalServerError
			};

			var jsonResponse = JsonSerializer.Serialize(new
			{
				Status = statusCode,
				Message = ex.Message,
			});
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int	)statusCode;

			await context.Response.WriteAsync(jsonResponse);
		}
	}
}