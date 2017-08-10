using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Nancy.Owin;
using NancyWebAppClient;
using NancyWebAppClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Xunit;

namespace NancyWebApp.Tests
{
    public class IntegrationTests
    {
        [Fact]
        public void Authenticate_should_return_False()
        {
            //  arrange
            NancyWebAppConfig.IdentityServerEnabled = true;
            var server = new TestServer(new WebHostBuilder().UseStartup(typeof(Startup)));
            var handler = server.CreateHandler();
            var client = new BasicClient(handler, "https://server/connect/token", "client", "secret");
            var authorized = false;
            this.Invoking((a) =>
            {
                //  act
                authorized = client.AuthenticateUserAsync("", "", "").RunAsSynchronous();
            })

            //  assert
            .ShouldNotThrow();
            authorized.Should().BeFalse();
        }

        [Fact]
        public void Authenticate_should_return_True()
        {
            //  arrange
            NancyWebAppConfig.IdentityServerEnabled = true;
            var server = new TestServer(new WebHostBuilder().UseStartup(typeof(Startup)));
            var handler = server.CreateHandler();
            var client = new BasicClient(handler, "https://server/connect/token", "client", "secret");
            var authorized = false;
            this.Invoking((a) =>
            {
                //  act
                authorized = client.AuthenticateUserAsync("alfredorevilla", "password", "api1").RunAsSynchronous();
            })

            //  assert
            .ShouldNotThrow();
            authorized.Should().BeTrue();
        }

        [Fact]
        public void GetBooksByUser_should_be_Ok()
        {
            //  arrange
            NancyWebAppConfig.IdentityServerEnabled = false;
            var server = new TestServer(new WebHostBuilder().UseStartup(typeof(Startup)));
            var client = new BasicClient(server.CreateClient(), "client", "secret");
            IEnumerable<Book> result = null;
            this.Invoking((a) =>
            {
                //  act
                result = client.GetBooksByUser("alfredorevilla").RunAsSynchronous();
            })

            //  assert
            .ShouldNotThrow();
            result.Should().NotBeNull();
            result.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void GetBooksByUserShouldThowUnauthorizedSecurityException()
        {
            //  arrange
            NancyWebAppConfig.IdentityServerEnabled = true;
            var server = new TestServer(new WebHostBuilder().UseStartup(typeof(Startup)));
            var client = new BasicClient(server.CreateClient(), "client", "secret");
            IEnumerable<Book> result = null;
            this.Invoking((a) =>
            {
                //  act
                result = client.GetBooksByUser("alfredorevilla").RunAsSynchronous();
            })

            //  assert
            .ShouldThrow<SecurityException>();
        }

        [Fact]
        public void GetTokenExpirationDate()
        {
            NancyWebAppConfig.IdentityServerEnabled = true;
            var server = new TestServer(new WebHostBuilder().UseStartup(typeof(Startup)));
            var handler = server.CreateHandler();
            var client = new BasicClient(handler, "https://server/connect/token", "client", "secret");
            DateTimeOffset expirationDate;
            this.Invoking((a) =>
            {
                //  act
                client.AuthenticateUserAsync("alfredorevilla", "password", "api1").RunAsSynchronous();
                expirationDate = client.GetTokenExpirationDate();
            })

            //  assert
            .ShouldNotThrow();
            expirationDate.Should().BeAfter(DateTimeOffset.UtcNow);
            expirationDate.Should().BeBefore(DateTimeOffset.MaxValue);
        }

        [Fact]
        public void GetTokenUserId()
        {
            NancyWebAppConfig.IdentityServerEnabled = true;
            var server = new TestServer(new WebHostBuilder().UseStartup(typeof(Startup)));
            var handler = server.CreateHandler();
            var client = new BasicClient(handler, "https://server/connect/token", "client", "secret");
            string userId = null;
            this.Invoking((a) =>
            {
                //  act
                client.AuthenticateUserAsync("alfredorevilla", "password", "api1").RunAsSynchronous();
                userId = client.GetTokenUserId();
            })

            //  assert
            .ShouldNotThrow();
            userId.Should().Be("F7D5AC0B-5C00-4253-8B54-046392260658");
        }

        [Fact]
        public void GetUsers_should_be_Ok()
        {
            //  arrange
            NancyWebAppConfig.IdentityServerEnabled = false;
            var server = new TestServer(new WebHostBuilder().UseStartup(typeof(Startup)));
            var client = new BasicClient(server.CreateClient(), "client", "secret");
            IEnumerable<User> result = null;
            this.Invoking((a) =>
            {
                //  act
                result = client.GetUsers().RunAsSynchronous();
            })

            //  assert
            .ShouldNotThrow();
            result.Should().NotBeNull();
            result.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void GetUsers_should_thow_UnauthorizedSecurityException()
        {
            //  arrange
            NancyWebAppConfig.IdentityServerEnabled = true;
            var server = new TestServer(new WebHostBuilder().Configure(app => app.UseOwin().UseNancy()));
            var client = new BasicClient(server.CreateClient(), "client", "secret");
            IEnumerable<User> result = null;
            this.Invoking((a) =>
           {
               //  act
               result = client.GetUsers().RunAsSynchronous();
           })

            //  assert
            .ShouldThrow<SecurityException>();
        }
    }
}