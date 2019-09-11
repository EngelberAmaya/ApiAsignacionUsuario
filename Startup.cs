using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using apiUsuarioCrud.Models;
//nuevo
using Microsoft.AspNetCore.Cors;
using apiUsuarioCrud.Data.Repository;

namespace apiUsuarioCrud
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
            //nuevo
            services.AddCors();
            //
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddScoped<BaseRepository>(); //nuevo

            //services.AddScoped<UsuarioRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc().AddJsonOptions(ConfigureJson); // nuevo
            services.AddDbContext<UserContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("BaseContext")));
                       
        }

        private void ConfigureJson(MvcJsonOptions obj)  // agregado ek 22/08/19
        {
            obj.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection(); // nuevo
            app.UseCors(option => option.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .AllowCredentials()
                                        .WithOrigins("http://localhost:4200"));


            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
