using Xamarin.Forms;
using ZwinnyCRUD.Mobile.ViewModels;

namespace ZwinnyCRUD.Mobile.Views
{
    public partial class ProjectsPage : ContentPage
    {
        ProjectsViewModel _viewModel;

        public ProjectsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ProjectsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}