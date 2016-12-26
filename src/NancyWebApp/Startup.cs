using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nancy.Owin;
using NancyWebApp.IdentityServer;

namespace NancyWebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Debug);
            app.UseDeveloperExceptionPage();

            if (NancyWebAppConfig.IdentityServerEnabled)
            {
                app.UseIdentityServer();
                app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
                {
                    ApiName = "api1",
                    ApiSecret = "secret",
                    Authority = NancyWebAppConfig.Url,
                    RequireHttpsMetadata = false
                });
            }

            app.UseOwin(x => x.UseNancy());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (NancyWebAppConfig.IdentityServerEnabled)
            {
                services
                .AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                .AddInMemoryUsers(IdentityServerConfig.GetUsers());
            }
        }
    }
}