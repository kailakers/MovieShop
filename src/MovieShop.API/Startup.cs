using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.Core.Services;
using MovieShop.Infrastructure.Data;
using MovieShop.Infrastructure.Data.Repositories;

namespace MovieShop.API
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

            services.AddDbContext<MovieShopDbContext>(options =>
                                                          options.UseSqlServer(Configuration.GetConnectionString("MovieShopDbConnection")));
            services.AddAutoMapper(typeof(Startup), typeof(MovieShop.Core.MappingProfiles.MoviesMappingProfile));
            ConfigureRepositoriesDependencyInjection(services);
            ConfigureServicesDependencyInjection(services);
        }

        private void ConfigureRepositoriesDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
        }

        private void ConfigureServicesDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IMovieService, MovieService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                        {
                            builder.WithOrigins(Configuration.GetValue<string>("clientSPAUrl")).AllowAnyHeader().AllowAnyMethod();
                        });
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
