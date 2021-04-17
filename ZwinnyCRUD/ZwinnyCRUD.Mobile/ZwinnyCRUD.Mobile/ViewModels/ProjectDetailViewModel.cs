using System;
using System.Diagnostics;
using Xamarin.Forms;
using ZwinnyCRUD.Mobile.Views;

namespace ZwinnyCRUD.Mobile.ViewModels
{
    [QueryProperty(nameof(ProjectId), nameof(ProjectId))]
    public class ProjectDetailViewModel : BaseViewModel
    {
        private string _itemId;
        private string _title;
        private string _description;
        public int Id { get; set; }
        public Command GoToFilesCommand { get; }

        public ProjectDetailViewModel()
        {
            GoToFilesCommand = new Command(OnGoToFiles);
        }

        public string Text
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string ProjectId
        {
            get => _itemId;
            set
            {
                _itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string projId)
        {
            var intId = Int32.Parse(projId);
            try
            {
                var item = await DataStore.GetProjectAsync(intId);
                Id = item.Id;
                Text = item.Title;
                Description = item.Description;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Failed to load Project because: " + exception.Message);
            }
        }

        private async void OnGoToFiles(object obj)
        {
            await Shell.Current.GoToAsync(nameof(ProjectFilesPage));
        }
    }
}
