//using FakeItEasy;
//using FluentAssertions;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.TestHost;
//using Nancy;
//using Nancy.Owin;
//using NancyWebApp.Services;
//using System.Linq;
//using System.Threading.Tasks;
//using Xunit;

//[assembly: IncludeInNancyAssemblyScanning]

//namespace NancyWebApp.Tests
//{
//    //public class MyOwnNancyBootstrapper : INancyBootstrapper
//    //{
//    //    public static IUserService UserService { get; set; }

//    //    public void Dispose()
//    //    {
//    //        throw new NotImplementedException();
//    //    }

//    //    public INancyEngine GetEngine()
//    //    {
//    //        var fakeEngine = A.Fake<INancyEngine>();
//    //        return fakeEngine;
//    //    }

//    //    public INancyEnvironment GetEnvironment()
//    //    {
//    //        var fakeEnviroment = A.Fake<INancyEnvironment>();
//    //        return fakeEnviroment;
//    //    }

//    //    public void Initialise()
//    //    {
//    //        throw new NotImplementedException();
//    //    }
//    //}

//    public class Class
//    {
//        [Fact]
//        public async Task ResponseCodeShouldBeOk()
//        {
//            //  arrange

//            var service = A.Fake<IUserService>();
//            A.CallTo(() => service.GetUsers()).Returns(Enumerable.Empty<Models.User>());
//            NancyWebAppConfig.UserServicePerRequestInstance = () => service;

//            NancyWebAppConfig.AuthenticationEnabled = false;
//            //NancyWebAppConfig.Url = "http://localhost";
//            TestServer server = new TestServer(new WebHostBuilder()
//                .Configure(app =>
//                {
//                    app.UseOwin(x => x.UseNancy());
//                }).
//                ConfigureServices(services =>
//                {
//                }));
//            //var client = server.CreateClient();
//            var request = server.CreateRequest("/api/some");

//            //  act
//            //var response = await client.GetAsync("nancy/users");
//            var response = request.GetAsync().Result;
//            var html = response.Content.ReadAsStringAsync().Result;

//            //  assert
//            //var html = response.Content.ReadAsStringAsync().Result;
//            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
//        }

//        [Fact]
//        public async Task ResponseCodeShouldBeUnauthorized()
//        {
//            //  arrange
//            NancyWebAppConfig.AuthenticationEnabled = true;
//            TestServer server = new TestServer(new WebHostBuilder().UseStartup(typeof(Startup)));
//            var client = server.CreateClient();

//            //  act
//            var response = await client.GetAsync("nancy/users");

//            //  assert
//            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
//        }
//    }
//}