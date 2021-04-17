using Xamarin.Forms;
using ZwinnyCRUD.Mobile.ViewModels;

namespace ZwinnyCRUD.Mobile.Views
{
    public partial class ProjectFilesPage : ContentPage
    {
        ProjectFilesViewModel _viewModel;

        public ProjectFilesPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ProjectFilesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}