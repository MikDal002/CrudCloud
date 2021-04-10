using System;
using Xamarin.Forms;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Mobile.ViewModels
{
    public class NewProjectViewModel : BaseViewModel
    {
        private string _title;
        private string _description;

        public NewProjectViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(_title)
                && !String.IsNullOrWhiteSpace(_description);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Project newItem = new Project()
            {
                //Id = Guid.NewGuid().ToString(),
                Title = Title,
                Description = Description
            };

            await DataStore.AddProjectAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
