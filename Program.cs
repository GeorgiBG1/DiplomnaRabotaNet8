using Data;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkillBox.App.Hubs;
using Contracts;
using Services;
namespace DiplomnaRabotaNet8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<SkillBoxDbContext>(options =>
                options.UseSqlServer(connectionString));

            //Services
            builder.Services.AddScoped<DatabaseSeedService>();
            builder.Services.AddTransient<IOfferingService, OfferingService>();
            builder.Services.AddTransient<ICategoryService, CategoryService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IChatService, ChatService>();
            builder.Services.AddTransient<IImageService, ImageService>();
            builder.Services.AddTransient<IAdminService, AdminService>();
            builder.Services.AddTransient<INotificationService, NotificationService>();

            //AutoMapper
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            //Cloudinary
            builder.Services.AddSingleton<ICloudinaryService, CloudinaryService>();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddDefaultIdentity<SkillBoxUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                //Password settings
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6; //128
                options.Password.RequiredUniqueChars = 0;

                options.User.RequireUniqueEmail = true;
            })
              .AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<SkillBoxDbContext>()
              .AddDefaultUI()
              .AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddSignalR();

            builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection =
                config.GetSection("Authentication:Google");
                options.ClientId = googleAuthNSection["ClientId"];
                options.ClientSecret = googleAuthNSection["ClientSecret"];
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
            app.MapHub<ChatHub>("/chatHub");

            app.Run();
        }
    }
}