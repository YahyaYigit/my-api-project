using Basketball.Entity.Models;
using Basketball.Service.Services.ServiceAttendance;
using Basketball.Service.Services.ServiceAuthentication;
using Basketball.Service.Services.ServiceCategoryGroups;
using Basketball.Service.Services.ServiceDues;
using Basketball.Service.Services.ServiceRole;
using Basketball.Service.Services.ServiceTrainingHours;
using Basketball.Service.Services.ServiceUser;
using Football.DataAcces.Data;
using Football.DataAcces.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace footballAcademy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICategoryGroupsService, CategoryGroupsService>();
            builder.Services.AddScoped<IDuesService, DuesService>();
            builder.Services.AddScoped<ITrainingHoursService, TrainingHoursService>();
            builder.Services.AddScoped<IAttendanceService, AttendanceService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            // Identity configuration
            builder.Services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6; // Minimum length of password
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<SampleDBContext>()
            .AddDefaultTokenProviders();

            // Veritabaný baðlantý dizesi yapýlandýrmasý
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<SampleDBContext>(options =>
                options.UseSqlServer(connectionString));

            // JSON serileþtirme ayarlarý ve döngüsel referans hatasýný önleme
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            // CORS Ayarlarý
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            // Swagger yapýlandýrmasý
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

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
