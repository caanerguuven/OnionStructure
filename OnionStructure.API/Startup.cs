using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OnionStructure.API.Middleware;
using OnionStructure.Contract.DTOs;
using OnionStructure.Contract.Utils.Abstract;
using OnionStructure.Contract.Utils.Concrete;
using OnionStructure.Repositories.Repository.Abstract;
using OnionStructure.Repositories.Repository.Concrete;
using OnionStructure.Repository;
using OnionStructure.Service.Services.Abstract;
using OnionStructure.Service.Services.Concrete;

namespace OnionStructure.API
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OnionStructure.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });

            services.Configure<AppSettingsDto>(Configuration.GetSection("settings"));
            services.AddScoped<IJwtUtils, JwtUtils>();

            services.AddSingleton<ILogService, NLogService>();

            services.AddDbContext<AppDbContext>(item => item.UseSqlServer(Configuration.GetSection("settings:ConnectionStrings")["DBConnectionString"]));
            
            services.AddScoped(typeof(IAppRepository<,>), typeof(AppRepository<,>));

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IRoleService, RoleService>();


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(_ => true)
                        .AllowCredentials());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnionStructure.API v1"));
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<JwtMiddleware>();

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseStaticFiles();
        }
    }
}
