using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ZwinnyCRUD.Mobile.Services
{
    public class AuthenticationService
    {
        public enum UserStatus
        {
            Logged,
            Unlogged
        }
        public async Task<bool> LoginUser(string login, string password)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentException($"'{nameof(login)}' cannot be null or empty", nameof(login));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"'{nameof(password)}' cannot be null or empty", nameof(password));
            }

            var client = new RestClient("https://zwinnycrudtest.azurewebsites.net/");
            var request = new RestRequest("api/v1.0/auth/signin", Method.POST);

            var body = new
            {
                Mail = login,
                Password = password
            };

            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(body);

            IRestResponse response = await client.ExecuteAsync(request);

            LoggingStatusChanged?.Invoke(this, response.IsSuccessful ? UserStatus.Logged : UserStatus.Unlogged);
            return response.IsSuccessful;
        }

        public void LogoutUser()
        {
            LoggingStatusChanged?.Invoke(this, UserStatus.Unlogged);
        }

        public async Task<bool> RegisterUser(string login, string password)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentException($"'{nameof(login)}' cannot be null or empty", nameof(login));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"'{nameof(password)}' cannot be null or empty", nameof(password));
            }

            var client = new RestClient("https://zwinnycrudtest.azurewebsites.net/");
            var request = new RestRequest("api/v1.0/auth/signup", Method.POST);

            var body = new
            {
                Mail = login,
                Password = password
            };

            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(body);

            IRestResponse response = await client.ExecuteAsync(request);

            return response.IsSuccessful;
        }

        public event EventHandler<UserStatus> LoggingStatusChanged;
    }
}
