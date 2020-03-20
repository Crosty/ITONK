using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Frontend.Data;

namespace Frontend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //services.AddDbContext<FrontendContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("FrontendContext")));

            //Enable DIP for a HTTP Client to subsitute calls to DBContex with a HTTP Request
            //services.AddHttpClient();

            services.AddHttpClient("backend", c =>
            {
                var host = Configuration["F20ITONKBACKENDXYZ_SERVICE_HOST"];
                var port = Configuration["F20ITONKBACKENDXYZ_PORT_8080_TCP_PORT"];
                //c.BaseAddress = new Uri("https://localhost:44323/"); //local test
                //Remark below not using https but http
                //c.BaseAddress = new Uri("http://" + host + ":" + port + "/"); //Using environment variables
                //c.BaseAddress = new Uri("http://" + host);
                //c.BaseAddress = new Uri("http://f20itonkbackendxyz:8080/"); //Hard coded K8s Service namee
                //c.BaseAddress = new Uri("http://146.148.126.255:8080/");
                //c.BaseAddress = new Uri("http://10.192.79.33:8081/"); // Gr13 BaseAddress
                c.BaseAddress = new Uri("http://34.76.234.244:8080/");
                c.DefaultRequestHeaders.Add("ContentType", "application/json");

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
