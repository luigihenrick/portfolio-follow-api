using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using PortfolioFollow.Domain.Interfaces;
using PortfolioFollow.Service.Repositories;
using PortfolioFollow.Service.ExternalServices.FixedIncome;
using PortfolioFollow.Service.ExternalServices.VariableIncome;
using PortfolioFollow.Service.ExternalServices.TreasuryDirect;
using PortfolioFollow.Service.Cache;
using Hangfire;
using MongoDB.Driver;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using System;
using Microsoft.AspNetCore.Http;
using PortfolioFollow.Domain.Classes.Requests;
using PortfolioFollow.Api.Middlewares;
using AutoMapper;
using PortfolioFollow.Api.Common;

namespace PortfolioFollow
{
    public class Startup
    {
        public IHostingEnvironment HostingEnvironment { get; private set; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            this.HostingEnvironment = env;
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterServices(services);
            DatabaseConfiguration();
            HangfireConfiguration(services);

            services.AddMvc()
                .AddJsonOptions(jsonOption => jsonOption.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() })
                .AddJsonOptions(jsonOption => jsonOption.SerializerSettings.NullValueHandling = NullValueHandling.Ignore)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new Info { Title = "PortfolioFollow Api", Version = "v2" });
                c.DescribeAllEnumsAsStrings();
            });
        }

        private void HangfireConfiguration(IServiceCollection services)
        {
            services.AddHangfire(config =>
            {
                var mongoUrlBuilder = new MongoUrlBuilder(Configuration["DatabaseConnection"]);
                var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());

                var storageOptions = new MongoStorageOptions
                {
                    MigrationOptions = new MongoMigrationOptions
                    {
                        MigrationStrategy = new MigrateMongoMigrationStrategy(),
                        BackupStrategy = new CollectionMongoBackupStrategy()
                    }
                };
                config.UseMongoStorage(mongoClient, mongoUrlBuilder.DatabaseName, storageOptions);
            });
        }

        private static void DatabaseConfiguration()
        {
            var pack = new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String)
            };

            ConventionRegistry.Register("EnumStringConvention", pack, t => true);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IAssetPriceRepository, AssetPriceRepository>();

            services.AddTransient<IFixedIncomeService, FixedIncomeService>();
            services.AddTransient<IVariableIncomeService, VariableIncomeService>();
            services.AddTransient<ITreasureDirectService, TreasureDirectService>();

            services.AddTransient<IVariableIncomeCacheService, VariableIncomeCacheService>();
            services.AddTransient<ITreasureDirectCacheService, TreasureDirectCacheService>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            
            services.AddSingleton(mapper);
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
            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "PortfolioFollow Api V2");
            });

            var serviceProvider = app.ApplicationServices;
            var treasureDirectCacheService = serviceProvider.GetService<ITreasureDirectCacheService>();

            app.Run(async (context) => 
            {
                //await context.Response.WriteAsync("Started");
                RecurringJob.AddOrUpdate(() => Console.WriteLine("\n I'm Alive \n"), "*/15 * * * *");
                RecurringJob.AddOrUpdate(() => treasureDirectCacheService.GetAllPricesAsync(new TreasureDirectServiceRequest()), Cron.Daily(08, 00));
            });
        }
    }
}
