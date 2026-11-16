using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PizzaStar.Models;

namespace PizzaStar.Data
{
    public class DbInit
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (await roleManager.FindByNameAsync("Editor") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Editor"));
            }
            if (await roleManager.FindByNameAsync("Client") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Client"));
            }


            string adminEmail = "admin@gmail.com", adminPassword = "192837";
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    PhoneNumber = "380970601478",
                    Year = 1990,
                    City = "Дніпро",
                    Address = "Титова 12, кв 33."
                };
                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }


                User alex = new User
                {
                    Email = "alex@gmail.com",
                    UserName = "alex@gmail.com",
                    PhoneNumber = "38096546798",
                    Year = 2001,
                    City = "Дніпро",
                    Address = "Карла Маркса 121, кв 32."
                };
                result = await userManager.CreateAsync(alex, "qwerty");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(alex, "Editor");
                }


                User tom = new User
                {
                    Email = "tom@gmail.com",
                    UserName = "tom@gmail.com",
                    PhoneNumber = "380665459874",
                    Year = 1995,
                    City = "Дніпро",
                    Address = "Тополя 3, будинок 44 кв 7."
                };
                result = await userManager.CreateAsync(tom, "1234567AS");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(tom, "Client");
                }


                User marry = new User
                {
                    Email = "marry@in.ua",
                    UserName = "marry@in.ua",
                    PhoneNumber = "380964578796",
                    Year = 1981,
                    City = "Київ",
                    Address = "Шевеченко, будинок 7 кв 14."
                };
                result = await userManager.CreateAsync(marry, "2S91lds");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(marry, "Client");
                }
            }
        }
        public static async Task InitializeContentAsync(ApplicationContext context)
        {
            if (!await context.Categories.AnyAsync())
            {
                await context.Categories.AddRangeAsync
                    (
                        new Category[]{
                    new Category{ Name = "Піца", Description = "Найсмачніша піца в місті для справжніх гурманів.", DateOfPublication = DateTime.Now },
                    new Category{ Name = "Салати", Description = "Салати з рибою, м'ясом, овочами - великий вибір меню на будь-який смак.", DateOfPublication = DateTime.Now },
                    new Category{ Name = "Напої", Description = "Напої є одним із найважливіших елементів культури харчування.", DateOfPublication = DateTime.Now }
                        }
                    );
                await context.SaveChangesAsync();
            }
        }

    }

}
