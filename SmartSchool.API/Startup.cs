using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartSchool.API.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SmartSchool.API
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
            services.AddDbContext<SmartContext>(
                    context => context.UseSqlite(Configuration.GetConnectionString("Default"))
                );
            services.AddControllers().AddNewtonsoftJson(
                opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IRepository, Repository>();

            services.AddVersionedApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'VVV";
                opt.SubstituteApiVersionInUrl = true;
            }).AddApiVersioning(opt =>
            {
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ReportApiVersions = true;
            }
            );

            var apiDescricao = services.BuildServiceProvider()
                .GetService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(options =>
            {
                foreach (var descricao in apiDescricao.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(
                    descricao.GroupName,
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "SmartSchool API",
                        Version = descricao.ApiVersion.ToString(),
                        Description = "SmartScoolAPI - WebAPI",
                        //License = new Microsoft.OpenApi.Models.OpenApiLicense
                        //{
                        //    Name = "SmartSchool License",
                        //    Url = new Uri("")
                            
                        //},
                         Contact = new Microsoft.OpenApi.Models.OpenApiContact 
                        {
                            Name = "Everton Leão",
                            Email = "everleao@gmail.com",
                            Url = new Uri("https://github.com/VToum"),

                        }
                    });
                }

                var xmlArquivoDeComentario = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlArquivoCompleto = Path.Combine(AppContext.BaseDirectory, xmlArquivoDeComentario);

                options.IncludeXmlComments(xmlArquivoCompleto);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider apiDescricao)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger()
                .UseSwaggerUI(options =>
                {
                    foreach (var descricao in apiDescricao.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{descricao.GroupName}/swagger.json", 
                            descricao.GroupName.ToLowerInvariant());
                    }
                    options.RoutePrefix = "";
                });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
