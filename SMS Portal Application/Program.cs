using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SMSPortal.Application.Auth.Login.Commands;
using SMSPortal.Application.Helper;
using SMSPortal.Application.Interfaces.Repositories.Logs;
using SMSPortal.Application.Interfaces.Repositories.MessageTemplates;
using SMSPortal.Application.Interfaces.Repositories.SentMessages;
using SMSPortal.Application.Interfaces.Seeder.Role;
using SMSPortal.Application.Interfaces.Seeder.User;
using SMSPortal.Application.Interfaces.SendMessage;
using SMSPortal.Application.Services.GenerateJwtToken;
using SMSPortal.Application.Services.Login;
using SMSPortal.Application.Services.Logout;
using SMSPortal.Application.Services.SmsService;
using SMSPortal.Application.Templates.Commands.Delete;
using SMSPortal.Domain.Enitites.User;
using SMSPortal.Infrastructure.Services.Login;
using SMSPortal.Infrastructure.Services.Logout;
using SMSPortal.Persistence.Persistence;
using SMSPortal.Persistence.Repositories.Logs;
using SMSPortal.Persistence.Repositories.MessageTemplates;
using SMSPortal.Persistence.Repositories.SentMessages;
using SMSPortal.Persistence.Seeder.Role;
using SMSPortal.Persistence.Seeder.User;
using SMSPortal.Persistence.Services;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Portal_Application
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAuthentication(options =>
            {
                // Use JWT
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;  // require https
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    // (exp)  ==> expiration
                    ValidateLifetime = true,

                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TravelBookingPortal.API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            builder.Services.AddLogging();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
            // Add services to the container.
            builder.Services.AddDbContext<SMSPortalDBContext>(options =>
                    options.UseSqlServer("Server=db18714.public.databaseasp.net; Database=db18714; User Id=db18714; Password=4q#NZ@9h5r!T; Encrypt=False; MultipleActiveResultSets=True;"));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
     .AddEntityFrameworkStores<SMSPortalDBContext>()
     .AddDefaultTokenProviders();
            builder.Services.AddScoped<IUserSeeder, UserSeeder>();
            builder.Services.AddScoped<IRoleSeeder, RoleSeeder>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly));
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<ILogoutService, LogoutService>();
            builder.Services.AddScoped<IGenerateJwtToken, GenerateJwtToken>();
            builder.Services.AddScoped<ISentMessageRepo, SentMessageRepository>();

            builder.Services.AddScoped<ILogRepo, LogRepository>();
            builder.Services.AddScoped<IMessageTemplateRepo, MessageTemplateRepository>();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IMediator, Mediator>();
            builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));
            builder.Services.AddTransient<ISmsService, SMSService>();

            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAngularApp");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            using (var scope = app.Services.CreateScope())
            {
                var roleSeeder = scope.ServiceProvider.GetRequiredService<IRoleSeeder>();
                var userSeeder = scope.ServiceProvider.GetRequiredService<IUserSeeder>();
                await roleSeeder.SeedRoles();
                await userSeeder.SeedUsers();
            }
            app.Run();
        }
    }
}