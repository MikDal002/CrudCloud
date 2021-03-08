using System;
using System.Collections.Generic;
using ZwinnyCRUD.Mobile.ViewModels;
using ZwinnyCRUD.Mobile.Views;
using Xamarin.Forms;

namespace ZwinnyCRUD.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
