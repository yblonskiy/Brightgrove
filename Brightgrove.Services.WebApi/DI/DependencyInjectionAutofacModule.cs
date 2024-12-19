namespace Brightgrove.Services.WebApi.DI
{
    /// <summary>
    /// Autofac DI Configuration
    /// </summary>
    public class DependencyInjectionAutofacModule : Autofac.Module
    {
        /// <summary>
        /// Load Override
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();

            builder.RegisterAssemblyTypes(
                typeof(AutofacModuleCoreLibrary).Assembly,
                typeof(AutofacModuleIntegrationServices).Assembly,
                typeof(AutofacModuleServices).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // Use extension to inject AutoMapper with Autofac 
            builder.RegisterAutoMapper(false,
                typeof(AutofacModuleServicesMappings).Assembly);

            builder.RegisterAssemblyModules(
                typeof(AutofacModuleCoreLibrary).Assembly,
                typeof(AutofacModuleIntegrationServices).Assembly,
                typeof(AutofacModuleServices).Assembly);
        }
    }
}