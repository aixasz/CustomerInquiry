using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CustomerInquiry.Models;
using CustomerInquiry.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Swagger;

namespace CustomerInquiry
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
            services.AddDbContext<CustomerInquiryContext>(options =>
            {
                options.UseSqlServer("Data Source=.;Initial Catalog=CustomerInquiry;Integrated Security=True");
            });

            services.AddMvc()
                .AddJsonOptions(options => { options.SerializerSettings.Converters.Add(new StringEnumConverter()); })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<ICustomerInquiryService, CustomerInquiryService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Customer Inquiry API",
                        Version = "v1",
                        Contact = new Contact
                        {
                            Name = "Nattapong Nunpan",
                            Email = string.Empty,
                            Url = "https://github.com/aixasz"
                        },
                        License = new License
                        {
                            Name = "WTFPL",
                            Url = "http://www.wtfpl.net/"
                        }
                    }
                );

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer Inquiry API");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
