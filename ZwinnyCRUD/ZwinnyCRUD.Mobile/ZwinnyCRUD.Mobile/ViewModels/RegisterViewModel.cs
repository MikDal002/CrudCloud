using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using Xamarin.Forms;
using ZwinnyCRUD.Mobile.Views;

namespace ZwinnyCRUD.Mobile.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private string _mail = string.Empty;
        private string _password = string.Empty;
        private string _confirmPassword = string.Empty;

        public Command RegisterCommand { get; }

        public string Mail
        {
            get => _mail;
            set => SetProperty(ref _mail, value);
        }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(OnRegisterClicked);
        }

        private async void OnRegisterClicked(object obj)
        {
            if (_mail.Length == 0 || _password.Length == 0 || _confirmPassword.Length == 0)
            {
                return;
            }

            if (!_password.Equals(_confirmPassword))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Passwords are not the same", "Okay", "Cancel");
                return;
            }

            var client = new RestClient("https://zwinnycrudtest.azurewebsites.net/");
            var request = new RestRequest("api/v1.0/auth/signup", Method.POST);

            request.AddHeader("Accept", "application/json");

            var body = new
            {
                Mail = _mail,
                Password = _password
            };
            request.AddJsonBody(body);

            IRestResponse response = client.Execute(request);

            HttpStatusCode statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;

            var details = JObject.Parse(response.Content);

            if (numericStatusCode == 201)
            {
                await Application.Current.MainPage.DisplayAlert("Registration success", response.Content, "Okay", "Cancel");
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Registration failed", details["detail"].ToString(), "Okay", "Cancel");
            }
        }
    }
}
