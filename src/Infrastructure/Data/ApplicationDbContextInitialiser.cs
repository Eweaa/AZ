using System.Runtime.InteropServices;
using AZ.Domain.Constants;
using AZ.Domain.Entities;
using AZ.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AZ.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        // Default data
        // Seed, if necessary
        if (!_context.TodoLists.Any())
        {
            _context.TodoLists.Add(new TodoList
            {
                Title = "Todo List",
                Items =
                {
                    new TodoItem { Title = "Make a todo list 📃" },
                    new TodoItem { Title = "Check off the first item ✅" },
                    new TodoItem { Title = "Realise you've already done two things on the list! 🤯"},
                    new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
                }
            });

        }

        if (!_context.Sellers.Any())
        {
            var sellers = new List<Seller>()
            {
                new Seller(){Name = "Samsung"},
                new Seller(){Name = "Anker"},

            };
            _context.Sellers.AddRange(sellers);
        }

        if (!_context.Categories.Any())
        {
            var categories = new List<Category>()
            {
                new Category(){Name = "Mobiles"},
                new Category(){Name = "Power Banks & Chargers"},
                new Category(){Name = "Headphones"},

            };
            _context.Categories.AddRange(categories);
        }

        if (!_context.Products.Any())
        {
            var products = new List<Product>()
            {
                new Product(){Name = "Samsung Galaxy S23 Ultra", Rating = 4.8M, ImgPath="https://images.samsung.com/is/image/samsung/p6pim/eg/2302/gallery/eg-galaxy-s23-s918-sm-s918bzecmea-534861980", Description = "256GB, 12GB, 5G Mobile Phone, Dual SIM, Android Smartphone, Green - ‎SM-S918BZGCMEA", Discount = 3, Price = 39989, Quantity = 200, SellerId = 3, CategoryId = 4},
                new Product(){Name = "Samsung Galaxy Z Fold 5", Rating = 4.6M, ImgPath="https://wibi.com.kw/cdn/shop/products/543353-03-scaled_345ae274-817c-40d8-b98c-d6cc59544268_1024x1024.jpg?v=1691412470", Description = "12GB RAM, 256GB ROM, Icy Blue - 1 Year Warranty", Discount = 0, Price = 73770, Quantity = 100, SellerId = 3, CategoryId = 4},
                new Product(){Name = "Anker 621 magnetic battery", Rating=4.2M, ImgPath="https://i.ebayimg.com/images/g/RsYAAOSwgcBjwKRQ/s-l1600.jpg", Description = "5000mah magnetic wireless portable charger with usb-c cable", Discount = 0, Price = 1550, Quantity = 150, SellerId = 4, CategoryId = 5},
                new Product(){Name = "Soundcore by Anker Life Q30", Rating=4.5M, ImgPath="https://btech.com/cdn-cgi/image/quality=50,format=auto/media/catalog/product/cache/e21418cd5b7db379d69dd0c3518b7f56/a/3/a3028011_active_noise_cancelling_headphones_td_01_low_res_.jpg", Description = "Hybrid Active Noise Cancelling Headphones with Multiple Modes, Hi-Res Sound, Custom EQ via App, 40H Playtime, Comfortable Fit, Bluetooth Headphones, Multipoint Connection", Discount = 15, Price = 4660.54, Quantity = 5, SellerId = 4, CategoryId = 6},

            };
            _context.Products.AddRange(products);
        }

        await _context.SaveChangesAsync();
    }
}
