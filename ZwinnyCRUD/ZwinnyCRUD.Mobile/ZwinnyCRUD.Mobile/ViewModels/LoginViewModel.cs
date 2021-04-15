using RestSharp;
using System.Net;
using Xamarin.Forms;
using ZwinnyCRUD.Mobile.Views;

namespace ZwinnyCRUD.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _mail;
        private string _password;

        public Command LoginCommand { get; }

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

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            var client = new RestClient("https://zwinnycrudtest.azurewebsites.net/");
            var request = new RestRequest("api/v1.0/auth/signin", Method.POST);

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

            if (numericStatusCode == 200)
            {
                await Application.Current.MainPage.DisplayAlert("Login success", response.Content, "Okay", "Cancel");
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login failed", response.Content, "Okay", "Cancel");
            }
        }
    }
}
