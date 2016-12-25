using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace NancyWebAppClient.Tests
{
    public class BasicClientTests : IDisposable
    {
        public BasicClientTests(ITestOutputHelper helper)
        {
            this.Helper = helper;
        }

        private ITestOutputHelper Helper { get; }

        public void Dispose()
        {
        }

        [Fact]
        public async Task GetUsersTest1()
        {
            //  arrange
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000");

            //  act
            var response = await client.PostAsync("api/users", null);
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var html = await response.Content.ReadAsStringAsync();
                Helper.WriteLine(html);
            }

            Helper.WriteLine(response.StatusCode.ToString());

            //  assert
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetUsersTest2()
        {
            //  arrange
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000");

            //  act
            var response = await client.PostAsync("api/users", new StringContent(""));
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var html = await response.Content.ReadAsStringAsync();
                Helper.WriteLine(html);
            }

            Helper.WriteLine(response.StatusCode.ToString());

            //  assert
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}