using System;
using Xamarin.Forms;
using ZwinnyCRUD.Mobile.Services;
using ZwinnyCRUD.Mobile.Views;

namespace ZwinnyCRUD.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public bool IsUserLogged { get; set; }
        public AuthenticationService AuthenticationService;
        private FlyoutItem _registerMenuEntry;
        private FlyoutItem _loginMenuEntry;
        private FlyoutItem _logoutMenuEntry;

        public AppShell()
        {
            InitializeComponent();
            AuthenticationService = DependencyService.Get<AuthenticationService>();
            
            _registerMenuEntry = new FlyoutItem() { FlyoutDisplayOptions = Xamarin.Forms.FlyoutDisplayOptions.AsMultipleItems };
            _registerMenuEntry.Items.Add(new ShellContent() { Title = "Register", ContentTemplate = new DataTemplate(typeof(RegisterPage)) });

            _loginMenuEntry = new FlyoutItem() { FlyoutDisplayOptions = Xamarin.Forms.FlyoutDisplayOptions.AsMultipleItems };
            _loginMenuEntry.Items.Add(new ShellContent() { Title = "Login", ContentTemplate = new DataTemplate(typeof(LoginPage)) });

            _logoutMenuEntry = new FlyoutItem() { FlyoutDisplayOptions = Xamarin.Forms.FlyoutDisplayOptions.AsMultipleItems };
            _logoutMenuEntry.Items.Add(new ShellContent() { Title = "Logout", ContentTemplate = new DataTemplate(typeof(LogoutPage)) });
            
            Items.Add(_registerMenuEntry);
            Items.Add(_loginMenuEntry);
            
            Routing.RegisterRoute(nameof(ProjectDetailPage), typeof(ProjectDetailPage));
            Routing.RegisterRoute(nameof(NewProjectPage), typeof(NewProjectPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            AuthenticationService.LoggingStatusChanged += AuthenticationService_LoggingStatusChanged;
        }

        private void AuthenticationService_LoggingStatusChanged(object sender, AuthenticationService.UserStatus e)
        {
            IsUserLogged = e == AuthenticationService.UserStatus.Logged;
            if (IsUserLogged)
            {
                Items.Remove(_registerMenuEntry);
                Items.Remove(_loginMenuEntry);
                Items.Add(_logoutMenuEntry);
        }
            else
            {
                Items.Add(_registerMenuEntry);
                Items.Add(_loginMenuEntry);
                Items.Remove(_logoutMenuEntry);
    }
}
    }
}
