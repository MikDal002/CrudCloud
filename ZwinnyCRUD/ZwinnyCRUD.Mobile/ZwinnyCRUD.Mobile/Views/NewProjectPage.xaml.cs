using Xamarin.Forms;
using ZwinnyCRUD.Common.Models;
using ZwinnyCRUD.Mobile.ViewModels;

namespace ZwinnyCRUD.Mobile.Views
{
    public partial class NewProjectPage : ContentPage
    {
        public Project Item { get; set; }

        public NewProjectPage()
        {
            InitializeComponent();
            BindingContext = new NewProjectViewModel();
        }
    }
}