//using FakeItEasy;
//using FluentAssertions;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Nancy.Owin;
//using NancyWebApp.Models;
//using NancyWebApp.Services;
//using NancyWebAppClient;
//using System;
//using System.Linq;
//using System.Net.Http;
//using System.Threading;
//using System.Threading.Tasks;
//using Xunit;

//namespace NancyWebApp.Tests
//{
//    public class NancyFxTests : IDisposable
//    {
//        private CancellationTokenSource cancellationSource;
//        private IWebHost host = null;

//        public NancyFxTests()
//        {
//            cancellationSource = new CancellationTokenSource();
//            var cancellationToken = cancellationSource.Token;

//            new Thread(new ThreadStart(() =>
//            {
//                host = new WebHostBuilder()
//                    .UseContentRoot(@"C:\Users\arevilla\Source\Repos\NetCoreNancyFxTokenAuth\src\NancyWebApp")
//                    .UseUrls(NancyWebAppConfig.Url)
//                    .UseKestrel()
//                    .Configure(app => app.UseOwin().UseNancy())
//                    .Build();
//                host.Run(cancellationToken);
//            })).Start();

//            Thread.Sleep(1000);
//        }

//        public void Dispose()
//        {
//            cancellationSource.Cancel();
//        }

//        //[Fact]
//        public async Task IntegrationTest1()
//        {
//            var fake = A.Fake<IUserService>();
//            A.CallTo(() => fake.GetUsers()).Returns(Enumerable.Empty<User>());
//            A.CallTo(() => fake.GetUserFavoriteBooks("")).WithAnyArguments().Returns(Enumerable.Empty<Book>());

//            Assert.NotNull(host);

//            var client = new BasicClient(NancyWebAppConfig.Url, "client", "secret");
//            var users = await client.GetUsers();
//            users.Count().Should().BeLessOrEqualTo(0);
//        }

//        [Fact]
//        public async Task IntegrationTest2()
//        {
//            HttpClient client = new HttpClient();
//            client.BaseAddress = new Uri("http://localhost:5000/api/users");
//            var response = await client.PostAsync("http://localhost:5000/api/users", new StringContent(""));
//            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
//            {
//                var html = response.Content.ReadAsStringAsync();
//            }
//        }
//    }
//}