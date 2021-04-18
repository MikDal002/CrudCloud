using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using ZwinnyCRUD.Common.Models;
using ZwinnyCRUD.Mobile.Views;
using System.Collections.Generic;

namespace ZwinnyCRUD.Mobile.ViewModels
{
    [QueryProperty(nameof(ProjectId), nameof(ProjectId))]
    public class ProjectDetailViewModel : BaseViewModel
    {
        public ProjectDetailViewModel()
        {
            FileTappedOnce = new Command<File>(OnFileTappedOnce);
            FileTappedTwice = new Command<File>(OnFileTappedTwice);
        }

        private string _itemId;
        private string _title;
        private string _description;
        public int Id { get; set; }
        private ObservableCollection<File> _files;
        private File _fileTappedOnce;
        private File _fileTappedTwice;

        public Command<File> FileTappedOnce { get; }
        public Command<File> FileTappedTwice { get; }


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
       
        public ObservableCollection<File> Files { get => _files; set => SetProperty(ref _files, value); }

        public async void LoadItemId(string projId)
        {
            Debug.Assert(FileStore != null);

            var intId = Int32.Parse(projId);
            try
            {
                var item = await DataStore.GetProjectAsync(intId);
                Id = item.Id;
                Text = item.Title;
                Description = item.Description;
                Files = new ObservableCollection<File>(await FileStore.GetFilesAsync(intId));
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Failed to load Project because: " + exception.Message);
            }
        }

        public File FileToDownload
        {
            get => _fileTappedOnce;
            set
            {
                SetProperty(ref _fileTappedOnce, value);
                OnFileTappedOnce(value);
            }
        }

        public File FileToDelete
        {
            get => _fileTappedTwice;
            set
            {
                SetProperty(ref _fileTappedTwice, value);
                OnFileTappedTwice(value);
            }
        }

        async void OnFileTappedOnce(File File)
        {
            if (File == null)
                return;

            var filePath = File.FilePath;
            await FileStore.GetFileAsync(filePath);
            return;
        }

        async void OnFileTappedTwice(File File)
        {
            if (File == null)
                return;

            var intId = File.Id;            
            await FileStore.DeleteFileAsync(intId);
            Files.Remove(File);
            return;
        }
   
        public void OnAppearing()
        {
            IsBusy = true;
            _fileTappedOnce = null;
            _fileTappedTwice = null;
        }
    }
}
