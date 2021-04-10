using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZwinnyCRUD.Mobile.ViewModels;

namespace ZwinnyCRUD.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
    }
}