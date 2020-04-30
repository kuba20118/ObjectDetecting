using Autofac;
using Detector.Infrastructure.Database;
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
            builder.RegisterInstance(_configuration.GetSettings<MLModelSettings>())
                .SingleInstance();
            builder.RegisterInstance(_configuration.GetSettings<MySqlSettings>())
                .SingleInstance();
        }
    }
}