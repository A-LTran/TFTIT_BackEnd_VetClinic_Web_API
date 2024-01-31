
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
            builder.Services.AddSwaggerGen();

            // BLL
            builder.Services.AddScoped<IUserRepository_BLL, UserService_BLL>();
            builder.Services.AddScoped<IAnimalRepository_BLL, AnimalService_BLL>();
            builder.Services.AddScoped<IAppointmentRepository_BLL, AppointmentService_BLL>();

            // DAL
            builder.Services.AddScoped<IUserRepository_DAL, UserService_DAL>(_ => new UserService_DAL(builder.Configuration.GetConnectionString("TFTIC_VetClinic")!));
            builder.Services.AddScoped<IAnimalRepository_DAL, AnimalService_DAL>(_ => new AnimalService_DAL(builder.Configuration.GetConnectionString("TFTIC_VetClinic")!));
            builder.Services.AddScoped<IAppointmentRepository_DAL, AppointmentService_DAL>(_ => new AppointmentService_DAL(builder.Configuration.GetConnectionString("TFTIC_VetClinic")!));

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

            builder.Services.AddAuthorization(option =>
            {
                // Pour les points d'entrée accessible aux "admins"
                option.AddPolicy("adminPolicy", p => p.RequireRole("administrator"));
                option.AddPolicy("veterinaryPolicy", p => p.RequireRole("veterinary"));
                // User must be authenticated
                option.AddPolicy("connected", p => p.RequireAuthenticatedUser());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
