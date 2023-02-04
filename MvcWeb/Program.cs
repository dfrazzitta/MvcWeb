using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using MvcWeb.Data;
using Prometheus;
using System.Net;

namespace MvcWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHealthChecks();
            builder.Services.AddSingleton<MetricCollector>();

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            // Add services to the container.
            builder.Services.AddRazorPages();
            // Add services to the container.
            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                logging.RequestHeaders.Add("sec-ch-ua");
                logging.ResponseHeaders.Add("MyResponseHeader");
                logging.MediaTypeOptions.AddText("application/javascript");
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;

            });
            // Add services to the container.
            /*
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            */

            builder.Services.AddControllersWithViews();


            builder.WebHost.ConfigureKestrel(options =>
            {
                var port = 443;
                var pfxFilePath = @"/app/Simple.pfx";
                // The password you specified when exporting the PFX file using OpenSSL.
                // This would normally be stored in configuration or an environment variable;
                // I've hard-coded it here just to make it easier to see what's going on.
                var pfxPassword = "";

                options.Listen(IPAddress.Any, port, listenOptions =>
                {
                    // Enable support for HTTP1 and HTTP2 (required if you want to host gRPC endpoints)
                    listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                    // Configure Kestrel to use a certificate from a local .PFX file for hosting HTTPS
                    listenOptions.UseHttps(pfxFilePath, pfxPassword);
                });


                options.Listen(IPAddress.Any, 5000, listenOptions =>
                {
                    // Enable support for HTTP1 and HTTP2 (required if you want to host gRPC endpoints)
                    listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                    // Configure Kestrel to use a certificate from a local .PFX file for hosting HTTPS

                    //listenOptions.UseHttps(pfxFilePath, pfxPassword);
                });

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
            app.UseRequestMiddleware();
            app.UseGaugeMiddleware();
            app.UseMetricServer(5000, "/metrics");
            app.UseHttpLogging();

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