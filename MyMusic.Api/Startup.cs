using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyMusic.Core;
using MyMusic.Core.Services;
using MyMusic.Data;
using MyMusic.Services;

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
            else if (Configuration["Provider"] == "MySQL")
            {
                services.AddDbContext<MyMusicDbContext>(options =>
                    options.UseMySQL(Configuration.GetConnectionString("MySQL"), x => x.MigrationsAssembly("MyMusic.Data")));
            }else{
                throw new ArgumentException("Not a valid database type");
            }

            services.AddScoped<IUnitOfWork , UnitOfWork>();
            services.AddTransient<IMusicService, MusicService>();
            services.AddTransient<IArtistService, ArtistService>();
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
        }
    }
}
