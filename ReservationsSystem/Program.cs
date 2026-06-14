
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ReservationsSystem.API.Middleware;
using ReservationsSystem.Application.Interfaces.Repositories;
using ReservationsSystem.Application.Interfaces.Services;
using ReservationsSystem.Application.Services;
using ReservationsSystem.Application.Validators.User;
using ReservationsSystem.Infra.Files;
using ReservationsSystem.Infra.Persistence;
using ReservationsSystem.Infra.Repositories;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ReservationsSystem
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services
				.AddControllers()
				.AddJsonOptions(options =>
				{
					options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
				}
				);
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddFluentValidationAutoValidation();
			builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDataValidator>();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Reservations System API",
					Version = "v1",
					Description = "API for managing reservations for sports facilities."
				});

				var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath= Path.Combine(AppContext.BaseDirectory, xmlFilename);
				
				options.IncludeXmlComments(xmlPath);
			});

			builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			builder.Services.AddScoped<IFacilitiesService, FacilitiesService>();
			builder.Services.AddScoped<IUsersService, UsersService>();
			builder.Services.AddScoped<IReservationsService, ReservationsService>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();
			builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
			builder.Services.AddScoped<IFileService, FileService>();
			builder.Services.AddScoped<ICsvGenerator, CsvGenerator>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseExceptionMiddleware();

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
