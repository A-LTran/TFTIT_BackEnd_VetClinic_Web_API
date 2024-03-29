

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TFTIC_BackEnd_VetClinic_Web_API.Tools.ExceptionHandler;

namespace TFTIC_BackEnd_VetClinic_Web_API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "TFTIC - BackEnd - VetClinic",
                    Version = "v1"
                });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "documentation.xml");
                c.IncludeXmlComments(filePath);
            });

            // BLL
            builder.Services.AddScoped<IUserRepository_BLL, UserService_BLL>();
            builder.Services.AddScoped<IAnimalRepository_BLL, AnimalService_BLL>();
            builder.Services.AddScoped<IAppointmentRepository_BLL, AppointmentService_BLL>();

            // DAL
            builder.Services.AddScoped<IUserRepository_DAL, UserService_DAL>(_ => new UserService_DAL(builder.Configuration.GetConnectionString("TFTIC_VetClinic")!));
            builder.Services.AddScoped<IAnimalRepository_DAL, AnimalService_DAL>(_ => new AnimalService_DAL(builder.Configuration.GetConnectionString("TFTIC_VetClinic")!));
            builder.Services.AddScoped<IAppointmentRepository_DAL, AppointmentService_DAL>(_ => new AppointmentService_DAL(builder.Configuration.GetConnectionString("TFTIC_VetClinic")!));

            // Exception handling
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            // standardized responses as per RFC 7807 specification
            builder.Services.AddProblemDetails();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
               options =>
               {
                   options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenManager.key)),
                       ValidateLifetime = true,
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
               }
            );

            builder.Services.AddAuthorizationBuilder()
                .AddPolicy("adminPolicy", p => p.RequireRole("administrator"))
                .AddPolicy("veterinaryPolicy", p => p.RequireRole("veterinary"))
                .AddPolicy("adminAndVetPolicy", p => p.RequireRole("administrator", "veterinary"))
                .AddPolicy("connected", p => p.RequireAuthenticatedUser());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            app.UseHttpsRedirection();

            // User Exception Handler
            app.UseExceptionHandler();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
