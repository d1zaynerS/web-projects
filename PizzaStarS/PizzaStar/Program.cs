using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PizzaStar.Data;
using PizzaStar.Models;
using PizzaStar.Interfaces;
using PizzaStar.Hubs; // Подключаем пространство имен с нашим хабом чата

namespace PizzaStar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfigurationRoot _confString = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 5;   // мінімальна довжина
                opts.Password.RequireNonAlphanumeric = false;   //чи потрібні алфавітно-цифрові символи
                opts.Password.RequireLowercase = false; // чи потрібні символи в нижньому регістрі
                opts.Password.RequireUppercase = false; //чи потрібні символи у верхньому регістрі
                opts.Password.RequireDigit = false; //чи потрібні цифри
            }).AddEntityFrameworkStores<ApplicationContext>();

            // Добавляем поддержку SignalR для чата в реальном времени
            builder.Services.AddSignalR();

            // Add services to the container.
            builder.Services.AddScoped<PizzaStar.Interfaces.ICategory, PizzaStar.Repositories.CategoryRepository>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    DbInit.InitializeAsync(userManager, rolesManager).Wait();

                    var applicationContext = services.GetRequiredService<ApplicationContext>();
                    DbInit.InitializeContentAsync(applicationContext).Wait();

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();

            // Маппим (регистрируем) SignalR хаб для чата
            app.MapHub<ChatHub>("/chatHub");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}