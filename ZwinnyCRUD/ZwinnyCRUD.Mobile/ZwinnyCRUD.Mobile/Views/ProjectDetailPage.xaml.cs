using Xamarin.Forms;
using ZwinnyCRUD.Mobile.ViewModels;

namespace ZwinnyCRUD.Mobile.Views
{
    public partial class ProjectDetailPage : ContentPage
    {
        public ProjectDetailPage()
        {
            InitializeComponent();
            BindingContext = new ProjectDetailViewModel();
        }
    }
}