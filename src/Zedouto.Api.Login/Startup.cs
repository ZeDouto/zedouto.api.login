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
using Zedouto.Api.Login.Facade.Extensions;
using Zedouto.Api.Login.Model.Configurations;

namespace Zedouto.Api.Login
{
    public class Startup
    {
        private const string FIRESTORE_SETTINGS_SECTION = "Firestore";
        private const string APPLICATION_SETTINGS_SECTION = "Application";
        private const string JWT_SETTINGS_SECTION = "Jwt";
        private const string SWAGGER_ENDPOINT = "/swagger/v1/swagger.json";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var firestoreSettings = Configuration.GetSection(FIRESTORE_SETTINGS_SECTION).Get<FirestoreAppSettings>();
            var applicationSettings = Configuration.GetSection(APPLICATION_SETTINGS_SECTION).Get<ApplicationSettings>();
            var jwtSettings = Configuration.GetSection(JWT_SETTINGS_SECTION).Get<JwtConfigurationSettings>();
            
            services.AddSingleton(applicationSettings);
            services.AddSingleton(jwtSettings);

            services.AddSwaggerGen(o =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                o.IncludeXmlComments(xmlPath, true);
            });
            
            services.AddControllers();
            services.AddServicesDependency(firestoreSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationSettings applicationSettings)
        {            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint(SWAGGER_ENDPOINT, $"{applicationSettings.Name}/{applicationSettings.Version}");
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
