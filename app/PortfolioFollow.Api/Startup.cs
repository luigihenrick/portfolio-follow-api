using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using PortfolioFollow.Domain.Interfaces;
using PortfolioFollow.Service.Business;
using PortfolioFollow.Service.Commons;
using PortfolioFollow.Service.Repositories;
using PortfolioFollow.Service.ExternalServices.FixedIncome;
using PortfolioFollow.Service.ExternalServices.VariableIncome;
using Newtonsoft.Json;
using PortfolioFollow.Service.ExternalServices.TreasuryDirect;

namespace PortfolioFollow
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(jsonOption => jsonOption.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() })
                .AddJsonOptions(jsonOption => jsonOption.SerializerSettings.NullValueHandling = NullValueHandling.Ignore)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<Configurations>(Configuration.GetSection("GlobalVariables"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new Info { Title = "PortfolioFollow Api", Version = "v2" });
                c.DescribeAllEnumsAsStrings();
            });

            services.AddScoped<IAssetPriceBusiness, AssetPriceBusiness>();
            services.AddScoped<IAssetPriceRepository, AssetPriceRepository>();
            services.AddTransient<IFixedIncomeService, FixedIncomeService>();
            services.AddTransient<IVariableIncomeService, VariableIncomeService>();
            services.AddTransient<ITreasureDirectService, TreasureDirectService>();

            var pack = new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String)
            };

            ConventionRegistry.Register("EnumStringConvention", pack, t => true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "PortfolioFollow Api V2");
            });
        }
    }
}
