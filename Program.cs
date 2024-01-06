using DiplomnaRabotaNet8.Data;
using DiplomnaRabotaNet8.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkillBox.App.Data.Seeding;
using SkillBox.App.Extensions;
namespace DiplomnaRabotaNet8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<SkillBoxDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<SkillBoxUserRoleSeeder>();
            builder.Services.AddScoped<SkillBoxAdminUserSeeder>();


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

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<SkillBoxDbContext>();
                context.Database.Migrate();
                // requires using Microsoft.Extensions.Configuration;
                // Set password with the Secret Manager tool.
            }

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

            app.UseDatabaseSeeding();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
