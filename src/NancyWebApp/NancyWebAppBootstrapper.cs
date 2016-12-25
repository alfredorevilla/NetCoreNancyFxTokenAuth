using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.TinyIoc;
using NancyWebApp.Services;
using NancyWebApp.Services.Implementations;

namespace NancyWebApp
{
    public class NancyWebAppBootstrapper : DefaultNancyBootstrapper
    {
        public override void Configure(INancyEnvironment environment)
        {
            environment.Tracing(true, true);
            base.Configure(environment);
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            container.Register<IUserService, UserService>();
            base.ConfigureRequestContainer(container, context);
        }
    }
}