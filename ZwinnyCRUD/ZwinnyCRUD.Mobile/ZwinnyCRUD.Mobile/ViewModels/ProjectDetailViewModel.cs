using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using ZwinnyCRUD.Common.Models;
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
        public ObservableCollection<File> Files { get; }
        public Command LoadFilesCommand { get; }
        public Command AddFileCommand { get; }
        public Command<Project> FileTapped { get; }

        public ProjectDetailViewModel()
        {
            Title = "Files";
            Files = new ObservableCollection<File>();
            LoadFilesCommand = new Command(async () => await ExecuteLoadFilesCommand());
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

        async System.Threading.Tasks.Task ExecuteLoadFilesCommand()
        {
            IsBusy = true;

            try
            {
                Files.Clear();
                var files = await FilesDataStore.GetFilesAsync(true);
                foreach (var file in files)
                {
                    Files.Add(file);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
