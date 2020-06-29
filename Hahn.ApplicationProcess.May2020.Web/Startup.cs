using System;
using System.IO;
using System.Reflection;
using Hahn.ApplicationProcess.May2020.Data;
using Hahn.ApplicationProcess.May2020.Domain;
using Hahn.ApplicationProcess.May2020.Domain.Common.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicationProcess.May2020.Web
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
            services.AddDbContext<ApplicationContext>(context => context.UseInMemoryDatabase("InMemory"));
            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
            services.AddControllers();
            services.AddMvc().AddNewtonsoftJson();
            
            services.AddDomain();
            services.AddData(Configuration);
            services.AddSwaggerExamplesFromAssemblyOf<Applicant>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.ExampleFilters();
                
                c.OperationFilter<AddHeaderOperationFilter>("correlationId", "Correlation Id for the request", false); 
                c.OperationFilter<AddResponseHeadersFilter>(); 
                
                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory,fileName);
                // c.IncludeXmlComments(filePath); 
                
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>(); 
                
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            services.AddSwaggerExamplesFromAssemblyOf<Applicant>();
            
            services.AddCors(options =>
            {
                options.AddPolicy("hahnPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("hahnPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn API V1");
            });
            
        }
    }
}