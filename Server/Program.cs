using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Server.Database;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddUserSecrets<Program>();

            // Add services to the container.
            builder.Services.AddCors(o => 
            o.AddDefaultPolicy(p => 
                p.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowed(_ => true)));

            builder.Services.AddSignalR();

            builder.Services.AddDbContext<ServerDb>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionStrings:DatabaseConnection")));

            builder.Services.AddAuthentication().AddJwtBearer();

            // builds services
            var app = builder.Build();

            // Configure Pipeline
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet("/health", () => Results.Ok( new { Ok = true }));

            app.MapHub<ChatHub>("/Hubs/chat");

            app.Run();
        }
    }
}
