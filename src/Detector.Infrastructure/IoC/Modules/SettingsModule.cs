using Autofac;
using Detector.Infrastructure.Extensions;
using Detector.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace Detector.Infrastructure.IoC.Modules
{
    public class SettingsModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;
        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        protected override void Load(ContainerBuilder builder)
        {
            // var assembly = typeof(SettingsModule)
            //     .GetTypeInfo()
            //     .Assembly;

            builder.RegisterInstance(_configuration.GetSettings<GeneralSettings>())
                .SingleInstance();
        }
    }
}