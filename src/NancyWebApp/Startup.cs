using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nancy.Owin;
using NancyWebApp.Common;
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

            app.UseIdentityServer();
            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                ApiName = "api1",
                ApiSecret = "secret",
                Authority = ServerConfig.BaseAddress,
                RequireHttpsMetadata = false
            });

            app.UseOwin(x => x.UseNancy());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryUsers(Config.GetUsers());
        }
    }
}