using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebProject.AppDbContextLayer;
using WebProject.ModelIdentityErrors;
using WebProject.Models.IdentityModels;
using WebProject.Services;
using WebProject.Services.MappingCategorizeServices;
using WebProject.Services.MappingCommodityServices;
using WebProject.Services.MappingImageDataServices;
using WebProject.Services.MappingLinesServices;
using WebProject.Services.MappingProductImage;
using WebProject.Services.MappingProductServices;

namespace WebProject
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
          
            services.AddMvc();
            services.AddSingleton<IProductServcies,ProductServices>();
            services.AddSingleton<ICommodityServices, CommodityServices>();
            services.AddSingleton<IDataImageServices, DataImageServices>();
            services.AddTransient<IProductImageServices, ProductImageServices>();
            services.AddSingleton<ICategorizeServices, CategorizeServices>();
            services.AddSingleton<ILineServices, LineServices>();

            services.AddTransient<AppDbContext>();
            services.AddIdentity<AppUser, IdentityRole>()
              .AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options => {

                //Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 8; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;


                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                //Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
                options.SignIn.RequireConfirmedAccount = true;

            });
            // Thiết lập , nếu IsPersistent = true thì đăng nhập user sẽ duy trì trong 1 ngày ,  IsPersistent = fasle thì đăng nhập user sẽ duy trì mãi mãi , với cookie máy khách 
            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.AccessDeniedPath = "/KhongDuocTruyCap.html";
            });
            services.Configure<SecurityStampValidatorOptions>(options => {
                options.ValidationInterval = TimeSpan.FromSeconds(30);

            });
            services.AddSingleton<IdentityErrorDescriber , AppIdentityErrorDescriber>();
            services.AddSingleton<AppIdentityErrorDescriber>();
            services.AddRazorPages();
            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePages(appBuilder  => {

                appBuilder.Run( async context => {

                    var reponse = context.Response;
                    var code = context.Response.StatusCode;

                    await reponse.WriteAsync($"Trang {code} {(HttpStatusCode)code}");
                    
                });


            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                
                //endpoints.MapRazorPages();
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Manager/Product}/{action=index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "areas",
                    pattern: "ProductManager/{controller}/{action}/{id?}",
                    areaName: "ProductManager",
                    defaults: new
                    {                       
                        controller = "Commodity",
                        action = "index"
                    },

                    constraints: new
                    {
                       
                        id = new GuidRouteConstraint()
                    }

                    );

                endpoints.MapAreaControllerRoute(
                    name: "Docsareas",
                    pattern: "Docs/{controller}/{action}/{id?}",
                    areaName: "Docs",
                    defaults: new
                    {
                        controller = "Csharp",
                        action = "index"
                    },

                    constraints: new
                    {

                        id = new GuidRouteConstraint()
                    }

                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}",
                    defaults: new
                    {
                        controller = "Home",
                        action = "index"
                    },
                    constraints: new
                    {
                        id = new GuidRouteConstraint()
                    }

                );

            });
        }
    }
}
