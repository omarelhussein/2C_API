using _2C_API.Data;
using _2C_API.Data.Entities;
using _2C_API.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<DatabaseContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(DatabaseContext))));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<DatabaseContext>();

        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.Cookie.Name = "UserSession";
            options.IdleTimeout = TimeSpan.FromMinutes(360); // Adjust the session timeout as needed
        });

        builder.Services.AddAuthentication();

        builder.Services.AddCors(policyBuilder =>
            policyBuilder.AddDefaultPolicy(policy =>
                policy.WithOrigins("*")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
            )
        );

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseCors();

        app.UseMiddleware<AuthenticateCookieMiddleware>();

        // Set Kestrel server URL using $PORT environment variable
        var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
        app.Run($"https://0.0.0.0:{port}"); // before having docker build change it to http://0.0.0.0:{port}
    }
}
