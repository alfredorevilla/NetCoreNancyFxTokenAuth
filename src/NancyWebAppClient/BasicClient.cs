using IdentityModel.Client;
using Microsoft.IdentityModel.Tokens;
using NancyWebAppClient.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;

namespace NancyWebAppClient
{
    public class BasicClient
    {
        private readonly string TokenEndpoint;
        private TokenResponse TokenResponse;

        /// <summary>
        /// Test ctor
        /// </summary>
        /// <param name="httpClientMessageHandler"></param>
        /// <param name="tokenClientMessageHandler"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        public BasicClient(HttpMessageHandler httpClientMessageHandler, string tokenEndPoint, string clientId, string clientSecret)
        {
            this.HttpClient = new HttpClient(httpClientMessageHandler);
            this.TokenClient = new TokenClient(tokenEndPoint, clientId, clientSecret, httpClientMessageHandler);
        }

        public BasicClient(HttpClient httpClient, string clientId, string clientSecret)
        {
            if (httpClient == null || httpClient.BaseAddress == null)
            {
                throw new ArgumentException(nameof(httpClient));
            }
            this.HttpClient = httpClient;
            this.TokenEndpoint = httpClient.BaseAddress + "/connect/token";
            this.TokenClient = new TokenClient(TokenEndpoint, clientId, clientSecret);
        }

        public BasicClient(string authority, string clientId, string clientSecret) : this(new HttpClient() { BaseAddress = new Uri(authority) }, clientId, clientSecret)
        {
        }

        public string BearerToken { get; private set; }

        public SecurityToken Token
        {
            get;
            private set;
        }

        private HttpClient HttpClient { get; set; }

        private JwtSecurityToken TokenAsJwt
        {
            get { return (JwtSecurityToken)this.Token; }
        }

        private TokenClient TokenClient { get; set; }

        public async Task<bool> AuthenticateUserAsync(string userName, string password, string api)
        {
            this.TokenResponse = await this.TokenClient.RequestResourceOwnerPasswordAsync(userName, password, api);
            if (TokenResponse.IsError)
            {
                return false;
            }
            this.BearerToken = TokenResponse.AccessToken;
            this.HttpClient.SetBearerToken(this.BearerToken);
            var handler = new JwtSecurityTokenHandler();
            this.Token = handler.ReadJwtToken(this.TokenResponse.AccessToken);
            return true;
        }

        public async Task<IEnumerable<Book>> GetBooksByUser(string userId)
        {
            return await this.PostReadAsync<dynamic, IEnumerable<Book>>($"api/user/{userId}/books", new { });
        }

        public DateTimeOffset GetTokenExpirationDate()
        {
            var exp = DateTimeHelper.GetFromNumericDate(this.TokenAsJwt.Payload.Exp.Value);
            return exp;
        }

        public string GetTokenUserId()
        {
            return this.TokenAsJwt.Subject;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await this.PostReadAsync<dynamic, IEnumerable<User>>("api/users", new { });
        }

        private async Task<T> GetAsync<T>(string path)
        {
            var response = await this.HttpClient.GetAsync(path);
            return await response.Content.ReadAsAsync<T>();
        }

        private async Task<T1> PostReadAsync<T, T1>(string path, T value)
        {
            this.HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var response = await this.HttpClient.PostAsJsonAsync<T>(path, value);
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.InternalServerError:
                    throw new Exception("Unkown server error");
                case System.Net.HttpStatusCode.OK:
                    return await response.Content.ReadAsAsync<T1>();

                case System.Net.HttpStatusCode.Unauthorized:
                    throw new SecurityException("Unauthorized");
                default:
                    throw new NotSupportedException($"Got status code {response.StatusCode.ToString()}");
            }
        }
    }
}