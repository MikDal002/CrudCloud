using System.ComponentModel;
using Xamarin.Forms;
using ZwinnyCRUD.Mobile.ViewModels;

namespace ZwinnyCRUD.Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}