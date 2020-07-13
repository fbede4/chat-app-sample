using Autofac;
using ChatApp.Dal;
using ChatApp.Dal.UoW;
using ChatApp.Domain.UoW;
using System.Linq;

namespace ChatApp.Api.AutofacModules
{
    public class DalAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new UnitOfWork(c.Resolve<ChatDbContext>()))
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(ChatDbContext).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
