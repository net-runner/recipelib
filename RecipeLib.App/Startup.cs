
using RecipeLib.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipeLib.Entities;

using FluentValidation.AspNetCore;
using FluentValidation;
using RecipeLib.Models.Validators;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace RecipeLib
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
            //JWT Initial config
            var JWTAS = new JWTAuthSettings();
            Configuration.GetSection("Authentication").Bind(JWTAS);


            services.AddSingleton(JWTAS);
            services.AddControllers().AddFluentValidation();
            services.AddDbContext<AppDbContext>();
            services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = false)
                            .AddDefaultTokenProviders().AddRoles<Role>()
                .AddEntityFrameworkStores<AppDbContext>();

            //Authentication configuration
            // services.AddAuthentication(option =>
            // {

            // }).AddJwtBearer(cfg =>
            // {
            //     cfg.RequireHttpsMetadata = true;
            //     cfg.SaveToken = true;
            //     cfg.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         ValidIssuer = JWTAS.JwtIssuer,
            //         ValidAudience = JWTAS.JwtIssuer,
            //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTAS.JwtKey))
            //     };
            // }).AddCookie();


            var multiSchemePolicy = new AuthorizationPolicyBuilder("Cookie", "Bearer").RequireAuthenticatedUser().Build();

            services.AddAuthorization(o => o.DefaultPolicy = multiSchemePolicy);

            services.AddSingleton<RecipeAppSeeder>();
            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            services.AddMemoryCache();
            services.AddSession();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, RecipeAppSeeder seeder, AppDbContext dbContext)
        {
            dbContext.Database.Migrate();
            await seeder.CreateInitialData();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
                app.UseHttpsRedirection();
            }
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseRouting();




            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });

        }
    }
}
