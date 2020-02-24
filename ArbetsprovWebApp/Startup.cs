using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Mime;

namespace ArbetsprovWebApp
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
            services.AddControllers(options =>
                options.Filters.Add(new HttpResponseExceptionFilter()));
            services.AddMvc(option => option.EnableEndpointRouting = false); //Enpoint routing disabled
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var result = new BadRequestObjectResult(context.ModelState);
                        
                        result.ContentTypes.Add(MediaTypeNames.Application.Json);
                        result.ContentTypes.Add(MediaTypeNames.Application.Xml);

                        return result;
                    };
                });
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressConsumesConstraintForFormFileParameters = true;
                    options.SuppressInferBindingSourcesForParameters = true;
                    options.SuppressModelStateInvalidFilter = true;
                    options.SuppressMapClientErrors = true;
                    options.ClientErrorMapping[404].Link =
                        "https://httpstatuses.com/404";
                });
        }

        public class HttpResponseException : Exception
        {
            public int Status { get; set; } = 500;

            public object Value { get; set; }
        }
        public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
        {
            public int Order { get; set; } = int.MaxValue - 10;

            public void OnActionExecuting(ActionExecutingContext context) { }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                if (context.Exception is HttpResponseException exception)
                {
                    context.Result = new ObjectResult(exception.Value)
                    {
                        StatusCode = exception.Status,
                    };
                    context.ExceptionHandled = true;
                }
            }
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                app.UseExceptionHandler("/error"); 
                app.UseHsts(); //Enable HTTP Strict Transport Security
            };

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "api/v1/devices/{controller}/{action}/{dateparam?}/{sensortype?}");                
            });
        }
    }
}
