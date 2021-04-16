using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZwinnyCRUD.Mobile.Services;
using ZwinnyCRUD.Mobile.ViewModels;

namespace ZwinnyCRUD.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogoutPage : ContentPage
    {
        private AuthenticationService _service;

        public LogoutPage()
        {
            InitializeComponent();
            BindingContext = new LogoutViewModel();
            _service = DependencyService.Get<AuthenticationService>();
        }

        protected override void OnAppearing()
        {
            _service.LogoutUser();
        }
    }
}