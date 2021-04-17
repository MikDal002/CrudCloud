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
        private string _itemId;
        private string _title;
        private string _description;
        public int Id { get; set; }
        private ObservableCollection<File> _files;
   
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

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
