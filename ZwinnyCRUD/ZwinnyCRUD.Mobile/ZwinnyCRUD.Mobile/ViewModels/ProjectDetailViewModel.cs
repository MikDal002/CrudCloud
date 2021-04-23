using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using ZwinnyCRUD.Common.Models;
using ZwinnyCRUD.Mobile.Views;
using System.Collections.Generic;
using Xamarin.Essentials;
using Plugin.DownloadManager;
using System.IO;

namespace ZwinnyCRUD.Mobile.ViewModels
{
    [QueryProperty(nameof(ProjectId), nameof(ProjectId))]
    public class ProjectDetailViewModel : BaseViewModel
    {
        public ProjectDetailViewModel()
        {
            FileTappedOnce = new Command<ZwinnyCRUD.Common.Models.File>(OnFileTappedOnce);
            FileTappedTwice = new Command<ZwinnyCRUD.Common.Models.File>(OnFileTappedTwice);
        }

        private string _itemId;
        private string _title;
        private string _description;
        public int Id { get; set; }
        private ObservableCollection<ZwinnyCRUD.Common.Models.File> _files;
        private ZwinnyCRUD.Common.Models.File _fileTappedOnce;
        private ZwinnyCRUD.Common.Models.File _fileTappedTwice;

        public Command<ZwinnyCRUD.Common.Models.File> FileTappedOnce { get; }
        public Command<ZwinnyCRUD.Common.Models.File> FileTappedTwice { get; }
        private const string BaseUrl = "https://zwinnycrudtest.azurewebsites.net/";

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

        public ZwinnyCRUD.Common.Models.File FileToDownload
        {
            get => _fileTappedOnce;
            set
            {
                SetProperty(ref _fileTappedOnce, value);
                OnFileTappedOnce(value);
            }
        }

        public ZwinnyCRUD.Common.Models.File FileToDelete
        {
            get => _fileTappedTwice;
            set
            {
                SetProperty(ref _fileTappedTwice, value);
                OnFileTappedTwice(value);
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        public ObservableCollection<ZwinnyCRUD.Common.Models.File> Files { get => _files; set => SetProperty(ref _files, value); }

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
                Files = new ObservableCollection<ZwinnyCRUD.Common.Models.File>(await FileStore.GetFilesAsync(intId));
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Failed to load Project because: " + exception.Message);
            }
        }              

        public void OnFileTappedOnce(ZwinnyCRUD.Common.Models.File File)
        {
            if (File == null)
                return;

            string url = $"{BaseUrl}api/v1.0/File?FilePath={File.FilePath}";
            var downloadManager = CrossDownloadManager.Current;
            var file = downloadManager.CreateDownloadFile(url);
            downloadManager.Start(file);
        }

        async void OnFileTappedTwice(ZwinnyCRUD.Common.Models.File File)
        {
            if (File == null)
                return;

            var intId = File.Id;            
            await FileStore.DeleteFileAsync(intId);
            Files.Remove(File);
            return;
        }          
    }
}
