using Autofac;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;

namespace ChatApp.Api.AutofacModules
{
    public class AppAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("ChatApp.Application"))
                .Where(t => t.Name.EndsWith("AppService"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
