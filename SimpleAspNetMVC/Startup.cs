using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleAspNetMVC.Data.interfaces;
using SimpleAspNetMVC.Data.Models;


namespace SimpleAspNetMVC
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=LibraryDB;Trusted_Connection=True;MultipleActiveResultSets=true";
            services.AddDbContext<LibraryContext>
                (options => options.UseSqlServer(connectionString));
            services.AddTransient<ILibraryIntern, LibraryIntern>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvcWithDefaultRoute();
            DBInit.Seed(app);
        }
    }
}
