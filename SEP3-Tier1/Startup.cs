using SEP3_Tier1.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SEP3_Tier1.Data.BookSale;
using SEP3_Tier1.Data.Purchase;
using SEP3_Tier1.Data.Users;
using Blazored.Modal;

namespace SEP3_Tier1
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            
            services.AddSingleton<ISaleService, SaleService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddSingleton<IPurchaseService, PurchaseService>();
            
            services.AddBlazoredModal();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", a =>
                    a.RequireAuthenticatedUser().RequireClaim("Role", "Admin"));
            });
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Customer", a =>
                    a.RequireAuthenticatedUser().RequireClaim("Role", "Customer"));
            });
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Moderator", a =>
                    a.RequireAuthenticatedUser().RequireClaim("Role", "Moderator"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}