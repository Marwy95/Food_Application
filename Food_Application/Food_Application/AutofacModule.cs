using Autofac;
using Autofac.Core;
using Food_Application.Data;
using Food_Application.Repositories;

namespace Food_Application
{
    public class AutofacModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterGeneric(typeof(RequestParameters<>)).InstancePerLifetimeScope();
            //builder.RegisterType<ControllereParameters>().InstancePerLifetimeScope();
            //builder.RegisterType<UserState>().InstancePerLifetimeScope();
            //builder.RegisterType<RabbitMQPublisherService>().SingleInstance();
            //builder.RegisterType<FakeDataService>().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationDbContext>().InstancePerLifetimeScope();
            //builder.RegisterType<DapperRepository>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            //builder..AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            //builder.RegisterAssemblyTypes(typeof(IExamService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();

        }
    }
}
