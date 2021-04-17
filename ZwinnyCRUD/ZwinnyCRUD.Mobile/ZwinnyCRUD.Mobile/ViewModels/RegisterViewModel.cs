using Acr.UserDialogs;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;
using Xamarin.Forms;
using ZwinnyCRUD.Mobile.Services;
using ZwinnyCRUD.Mobile.Views;

namespace ZwinnyCRUD.Mobile.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private string _mail = string.Empty;
        private string _password = string.Empty;
        private string _confirmPassword = string.Empty;

        private AuthenticationService _service;

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
            _service = DependencyService.Get<AuthenticationService>();
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

            bool RegisterSuccess = false;
            try
            {
                UserDialogs.Instance.ShowLoading("Czekaj...");
                RegisterSuccess = await _service.RegisterUser(_mail, _password);
            }
            catch (Exception exce)
            {
                await Application.Current.MainPage.DisplayAlert("Register failed", exce.Message, "Okay");
            }
            finally
            {
                if (!RegisterSuccess)
                    await Application.Current.MainPage.DisplayAlert("Register failed", "You must provide correct data", "Okay");
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Registration success", "You can now log in", "Okay");
                    await Shell.Current.GoToAsync("LoginPage");
                }
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}
