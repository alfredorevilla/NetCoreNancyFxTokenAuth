using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NancyWebApp;
using Xunit;
using Xunit.Abstractions;

namespace NancyWebAppClient.Tests
{
    public class IntegrationTests
    {
        public IntegrationTests(ITestOutputHelper helper)
        {
            this.Helper = helper;
        }

        private ITestOutputHelper Helper { get; set; }

        [Fact]
        public void ResponseShouldBeUnauthorized()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup(typeof(Startup)));
            var client = server.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.PostAsync("api/users", null);
        }
    }
}