using Microsoft.EntityFrameworkCore;
using Productos.API.Handlers.ErrorHandling.Middleware;
using Productos.API.Model;
using Productos.API.Repository;
using Productos.API.Repository.Contratos;
using Productos.API.Service;
using Productos.API.Utils;

namespace Productos.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<AppDBContext>(options =>
                    options.UseSqlServer(
                        builder.Configuration.GetConnectionString("DBAranda")));

            builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
            builder.Services.AddScoped<IProductoService, ProductoService>();
            builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<CustomValidationFilterAttributecs>();
            })
                                  .ConfigureApiBehaviorOptions(options =>
                                  {
                                      options.SuppressModelStateInvalidFilter = true;
                                  });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());
            });



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseMiddleware<FilterErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}