using System.Reflection;
using Autofac;
using Detector.Infrastructure.Database;

namespace Detector.Infrastructure.IoC.Modules
{
    public class MySqlModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(MySqlModule)
                .GetTypeInfo()
                .Assembly;


            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<ISqlRepository>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // builder.RegisterAssemblyTypes(assembly)
            //     .Where(x => x.IsAssignableTo<IDataContext>())
            //     .AsImplementedInterfaces()
            //     .InstancePerLifetimeScope();
        }
    }
}