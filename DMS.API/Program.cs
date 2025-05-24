using DMS.BLL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DMS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //DbContext
            builder.Services.AddDbContext<DmsdatabaseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //DI

            //Service

            //CORS
            builder.Services.AddCors(options =>
            {
                var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
                //AllowLocalhost
                options.AddPolicy("AllowLocalhost",
                    policy =>
                    {
                        policy.SetIsOriginAllowed(origin =>
                        {
                            return new Uri(origin).Host.EndsWith("ngrok-free.app") || allowedOrigins.Contains(origin);
                        })
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
            });

            //JWT
            //Authentication JWT
            var jwtKey = builder.Configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("JWT Key is missing from configuration.");
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.FromMinutes(5),

                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });


            //Authorization
            builder.Services.AddAuthorization(options =>
            {
                // 1. AdminOnly
                options.AddPolicy("Admin", policy =>
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(c => c.Type == "ur" && c.Value == "admin")
                    )
                );
            });


            builder.Services.AddControllers();
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
