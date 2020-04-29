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

namespace Detector.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        private readonly string _onnxModelFilePath;
        private readonly string _mlnetModelFilePath;
        private readonly MLModelSettings _mLModelSetting;

        public Startup(IConfiguration configuration, IWebHostEnvironment env, MLModelSettings mLModelSetting)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            _mLModelSetting = mLModelSetting;

            _onnxModelFilePath = mLModelSetting.OnnxModelFilePath; //PathExtensionions.GetAbsolutePath(Configuration["MLModel:OnnxModelFilePath"]);
            _mlnetModelFilePath = mLModelSetting.MLNETModelFilePath; //PathExtensionions.GetAbsolutePath(Configuration["MLModel:MLNETModelFilePath"]);

            var onnxModelConfigurator = new Detector.ML.Config.OnnxModelConfigurator(new TinyYoloModel(_onnxModelFilePath));

            onnxModelConfigurator.SaveMLNetModel(_mlnetModelFilePath);
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));

            var builder = new ContainerBuilder();
            services.AddPredictionEnginePool<ImageInputData, TinyYoloPrediction>().
                FromFile(_mlnetModelFilePath);
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
