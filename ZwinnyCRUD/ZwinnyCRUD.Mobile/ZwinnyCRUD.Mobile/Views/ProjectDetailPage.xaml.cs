using Xamarin.Forms;
using ZwinnyCRUD.Mobile.ViewModels;

namespace ZwinnyCRUD.Mobile.Views
{
    public partial class ProjectDetailPage : ContentPage
    {
        ProjectDetailViewModel _viewModel;

        public ProjectDetailPage()
        {
            InitializeComponent();
            BindingContext = new ProjectDetailViewModel();
            BindingContext = _viewModel = new ProjectDetailViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}