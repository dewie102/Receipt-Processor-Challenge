using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceiptProcessorChallenge_CSharp.Models;

namespace ReceiptProcessorChallenge_CSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddDbContext<PointContext>(opt => opt.UseInMemoryDatabase("Points"));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMvc().ConfigureApiBehaviorOptions(options =>
            {
                // Setup default response for invalid model creation
                // This works here because the only model the API is dealing with is receipts
                options.InvalidModelStateResponseFactory = context =>
                {
                    return new ObjectResult("The receipt is invalid") { StatusCode = 400};
                };
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
