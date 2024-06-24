using Forging_calculation.Service;
using Microsoft.EntityFrameworkCore;

namespace Forging_calculation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<CalculationService>();
        builder.Services.AddSingleton<DrawingService>();
        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.MapRazorPages();
        app.UseAuthorization();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            if (endpoints != null)
            {
                endpoints.MapControllerRoute(
                    name: "drawing",
                    pattern: "drawing",
                    defaults: new { controller = "Drawing", action = "Generate" });
            }
        });
        app.Run();
    }
}