using IdentityModel.Client;
using Microsoft.IdentityModel.Tokens;
using NancyWebAppClient.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;

namespace NancyWebAppClient
{
    public class BasicClient
    {
        private readonly string token_endpoint;
        private TokenResponse TokenResponse;

        public BasicClient(string authority, string clientId, string clientSecret)
        {
            Uri uri;
            if (!Uri.TryCreate(authority, UriKind.Absolute, out uri))
            {
                throw new ArgumentException(nameof(authority));
            }
            this.Authority = uri.ToString();
            token_endpoint = this.Authority + "/connect/token";
            this.TokenClient = new TokenClient(token_endpoint, clientId, clientSecret);
            this.HttpClient = new HttpClient();
            this.HttpClient.BaseAddress = uri;
        }

        public string Authority { get; }

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
            this.TokenResponse = await this.TokenClient.RequestResourceOwnerPasswordAsync(userName, password);
            if (TokenResponse == null)
            {
                throw new NullReferenceException($"TokeResponse was not excepted to be null");
            }
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

        public async Task<IEnumerable<Book>> GetBookByUser(string userId)
        {
            return await this.PostAsync<dynamic, IEnumerable<Book>>($"user/{userId}/books", null);
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
            return await this.PostAsync<dynamic, IEnumerable<User>>("api/users", null);
        }

        private async Task<T> GetAsync<T>(string path)
        {
            var response = await this.HttpClient.GetAsync(path);
            return await response.Content.ReadAsAsync<T>();
        }

        private async Task<T1> PostAsync<T, T1>(string path, T value)
        {
            var response = await this.HttpClient.PostAsJsonAsync<T>(path, value);
            return await response.Content.ReadAsAsync<T1>();
        }
    }
}