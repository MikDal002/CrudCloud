using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using ZwinnyCRUD.Common.Models;
using ZwinnyCRUD.Mobile.Views;

namespace ZwinnyCRUD.Mobile.ViewModels
{
    public class ProjectFilesViewModel : BaseViewModel
    {
        public ObservableCollection<File> Files { get; }
        public Command LoadFilesCommand { get; }
        public Command AddFileCommand { get; }
        public Command<Project> FileTapped { get; }

        public ProjectFilesViewModel()
        {
            Title = "Files";
            Files = new ObservableCollection<File>();
            LoadFilesCommand = new Command(async () => await ExecuteLoadFilesCommand());
            AddFileCommand = new Command(OnAddFile);
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

        private async void OnAddFile(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewProjectPage));
        }
    }
}