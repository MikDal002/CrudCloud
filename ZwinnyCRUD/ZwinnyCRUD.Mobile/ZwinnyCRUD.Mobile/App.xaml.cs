using Firebase.Storage;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Xamarin.Forms;
using ZwinnyCRUD.Common.Models;
using ZwinnyCRUD.Mobile.Services;

namespace ZwinnyCRUD.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<RestDataStore>();
            DependencyService.Register<RestFileStore>();
            DependencyService.Register<AuthenticationService>();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            var datacontext = DependencyService.Get<IDataStore<Project>>();
            await datacontext.GetProjectsAsync(true);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
