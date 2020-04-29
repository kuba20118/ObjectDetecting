using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Detector.Core.Repositories;
using Detector.Infrastructure.Repositories;
using Detector.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Detector.Infrastructure.IoC.CommandModules;
using Detector.Infrastructure.IoC.Modules;
using Detector.Infrastructure.IoC;
using Detector.Api.Framework;
using OnnxObjectDetectionWeb.Services;
using OnnxObjectDetectionWeb.Utilities;
using OnnxObjectDetection;
using OnnxObjectDetectionWeb.Infrastructure;
using Microsoft.Extensions.ML;

namespace Detector.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        private readonly string _onnxModelFilePath;
        private readonly string _mlnetModelFilePath;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            _onnxModelFilePath = CommonHelpers.GetAbsolutePath(Configuration["MLModel:OnnxModelFilePath"]);
            _mlnetModelFilePath = CommonHelpers.GetAbsolutePath(Configuration["MLModel:MLNETModelFilePath"]);

            var onnxModelConfigurator = new OnnxModelConfigurator(new TinyYoloModel(_onnxModelFilePath));

            onnxModelConfigurator.SaveMLNetModel(_mlnetModelFilePath);
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<IImageService, ImageService>();
            //services.AddScoped<IImageRepository, ImageRepositoryTemporary>();
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            //services.AddMvc();
            services.AddTransient<IImageFileWriter, ImageFileWriter>();
            services.AddTransient<IObjectDetectionService, ObjectDetectionService>();
            //services.AddScoped<IImageService, ImageService>();

            var builder = new ContainerBuilder();
            //services.AddScoped<ITestService, TestService>();
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
