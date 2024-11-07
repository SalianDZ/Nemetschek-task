using Hierarchy.Data;
using Hierarchy.Data.Repositories.Interfaces;
using Hierarchy.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Hierarchy.Services.Data.Interfaces;
using Hierarchy.Services.Data;

namespace Hierarchy.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<HierarchyDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
			builder.Services.AddScoped<IPositionRepository, PositionRepository>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
			builder.Services.AddScoped<IPositionService, PositionService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
