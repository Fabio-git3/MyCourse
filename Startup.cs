using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MyCourse.Models.Services.Application;
using MyCourse.Models.Services.Infrastucture;

namespace MyCourse
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddTransient<ICourseService,AdoNetCourseService>();
            services.AddTransient<IDataBaseAccessor,SqliteDataBaseAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)//configure gestisce i middleware
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();//primo middleware impostato

                //aggiorniamo un file per notificare browsersync che deve aggiornare la pagina
                lifetime.ApplicationStarted.Register(()=>
                {
                    string filePath=Path.Combine(env.ContentRootPath,"bin/reload.txt");
                    File.WriteAllText(filePath,DateTime.Now.ToString());
                });

            }
            //secondo middleware per la gestione dei file statici
            app.UseStaticFiles();


            //terzo middleware routing 2 modi
            //app.UseMvcWithDefaultRoute(); 
            app.UseMvc(routeBuilder =>{

                routeBuilder.MapRoute("default","{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
