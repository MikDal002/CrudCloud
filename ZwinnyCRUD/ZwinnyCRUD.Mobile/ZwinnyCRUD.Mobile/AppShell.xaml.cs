using System;
using Xamarin.Forms;
using ZwinnyCRUD.Mobile.Views;

namespace ZwinnyCRUD.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ProjectDetailPage), typeof(ProjectDetailPage));
            Routing.RegisterRoute(nameof(NewProjectPage), typeof(NewProjectPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
