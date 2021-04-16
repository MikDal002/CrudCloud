using Acr.UserDialogs;
using RestSharp;
using System;
using System.Net;
using Xamarin.Forms;
using ZwinnyCRUD.Mobile.Services;
using ZwinnyCRUD.Mobile.Views;

namespace ZwinnyCRUD.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _mail;
        private string _password;

        

        public Command LoginCommand { get; }

        private AuthenticationService _service;

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
            _service = DependencyService.Get<AuthenticationService>();
            _service.LoggingStatusChanged += Service_LoggingStatusChanged;
        }

        private async void OnLoginClicked(object obj)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Czekaj...");
                await _service.LoginUser(_mail, _password);
            }
            catch (Exception exce)
            {
                await Application.Current.MainPage.DisplayAlert("Login failed", exce.Message, "Okay");
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }

        }

        private async void Service_LoggingStatusChanged(object sender, AuthenticationService.UserStatus e)
        {

            if (e == AuthenticationService.UserStatus.Logged)
            {
                await Application.Current.MainPage.DisplayAlert("Login success", "", "Okay");
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login failed", "Incorrect login or password.", "Okay");
            }
        }
    }
}
