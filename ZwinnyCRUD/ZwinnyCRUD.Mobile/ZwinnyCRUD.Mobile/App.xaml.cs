using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZwinnyCRUD.Common.Models;
using ZwinnyCRUD.Mobile.Services;
using ZwinnyCRUD.Mobile.Views;

namespace ZwinnyCRUD.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            var datacontext = DependencyService.Get<IDataStore<Project>>();
             await datacontext.GetItemsAsync(true);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
