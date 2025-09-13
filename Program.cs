using BookingManagerMVC.Services;

namespace BookingManagerMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDistributedMemoryCache(); // lagrar session i minnet
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // hur länge sessionen är giltig
                options.Cookie.HttpOnly = true; // skyddar mot JS-access
                options.Cookie.IsEssential = true; // behövs även om användaren inte accepterar cookies
            });
            builder.Services.AddScoped<MenuService>();
            builder.Services.AddScoped<AuthService>();

            builder.Services.AddHttpClient("BookingApi", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
            });


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
