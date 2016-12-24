namespace NancyWebApp
{
    using Common;
    using IdentityServer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Nancy.Owin;

    public class Startup
    {
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Debug);
            app.UseDeveloperExceptionPage();

            app.UseIdentityServer();
            //app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            //{
            //    Authority = ServerConfig.BaseAddress,
            //    RequireHttpsMetadata = false,
            //    EnableCaching = false,
            //    ApiName = "api1",
            //});
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
                .AddInMemoryClients(Config.GetClients());
        }
    }
}