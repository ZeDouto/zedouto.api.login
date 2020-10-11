using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Zedouto.Api.Model.Configurations;
using Zedouto.Api.Service.Extensions;

namespace Zedouto.Api.Login
{
    public class Startup
    {
        private const string FIRESTORE_SETTINGS_SECTION = "Firestore";
        private const string SWAGGER_ENDPOINT = "swagger/v1/swagger.json";
        private const string APP_NAME = "ZeDouto.Login";
        private const string APP_VERSION = "V1";

        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var firestoreSettings = Configuration.GetSection(FIRESTORE_SETTINGS_SECTION).Get<FirestoreAppSettings>();

            services.AddSwaggerGen(o =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                o.IncludeXmlComments(xmlPath, true);
            });
            
            services.AddControllers();
            services.AddApplicationDependencies(firestoreSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint(SWAGGER_ENDPOINT, $"{APP_NAME}/{APP_VERSION}");
                o.RoutePrefix = string.Empty;
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
