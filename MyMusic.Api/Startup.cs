using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyMusic.Core;
using MyMusic.Core.Models;
using MyMusic.Core.Services;
using MyMusic.Data;
using MyMusic.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace MyMusic.Api
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

            if (Configuration["Provider"] == "SQLite")
            {
                services.AddDbContext<MyMusicDbContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("SQLite"), x => x.MigrationsAssembly("MyMusic.Data")));
            }
            else if (Configuration["Provider"] == "MySql")
            {
                services.AddDbContext<MyMusicDbContext>(options =>
                    options.UseMySQL(Configuration.GetConnectionString("MySql"), x => x.MigrationsAssembly("MyMusic.Data")));
            }else{
                throw new ArgumentException("Not a valid database type");
            }

            services.AddScoped<IUnitOfWork , UnitOfWork>();
            services.AddTransient<IMusicService, MusicService>();
            services.AddTransient<IArtistService, ArtistService>();

            // adding identity to dbContext
            services.AddIdentity<User,Role>(options => 
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
            })
            .AddEntityFrameworkStores<MyMusicDbContext>()
            .AddDefaultTokenProviders();

            // Registering Swagger generator
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo{Title="MY API", Version="v1"});
            });
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // Enabling middleware to serve generated Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c=>{
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json","MY API V1");
            });
        }
    }
}
