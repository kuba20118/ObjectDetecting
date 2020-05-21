using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Detector.Infrastructure.IoC;
using Microsoft.Extensions.ML;
using Detector.Infrastructure.Extensions;
using Detector.Infrastructure.ImageFileHelpers;
using Detector.ML.Config;
using Detector.ML;
using Detector.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Detector.Infrastructure.Repositories;
using Detector.Core.Repositories;
using Detector.Infrastructure.Services;
using Detector.Infrastructure.Mappers;
using Detector.Infrastructure.Mongo;

namespace Detector.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        private readonly string _onnxModelFilePath;
        private readonly string _mlnetModelFilePath;
        private readonly MLModelSettings _mLModelSetting;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            _onnxModelFilePath = PathExtensionions.GetAbsolutePath(Configuration["MLModel:OnnxModelFilePath"]);
            _mlnetModelFilePath = PathExtensionions.GetAbsolutePath(Configuration["MLModel:MLNETModelFilePath"]);

            var onnxModelConfigurator = new Detector.ML.Config.OnnxModelConfigurator(new TinyYoloModel(_onnxModelFilePath));

            onnxModelConfigurator.SaveMLNetModel(_mlnetModelFilePath);
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddPredictionEnginePool<ImageInputData, TinyYoloPrediction>()
                .FromFile(_mlnetModelFilePath);
            // services.AddEntityFrameworkMySql()
            //     .AddDbContext<DataContext>(); //(x => x.UseMySql("server=localhost;database=detectordb;user=user;password=password"));
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfiles());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            var builder = new ContainerBuilder();

            builder.Populate(services);
            builder.RegisterModule(new ContainerModule(Configuration));
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            MongoConfigurator.Initialize();
            // var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();
            // dataInitializer.SeedAsync();

            app.UseRouting();
            //app.UseCustomExceptionHandler();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            hostApplicationLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }
    }
}
