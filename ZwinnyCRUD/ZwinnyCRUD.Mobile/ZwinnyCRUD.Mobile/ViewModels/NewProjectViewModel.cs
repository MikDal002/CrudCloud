using Firebase.Storage;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using Xamarin.Forms;
using ZwinnyCRUD.Common.Dtos;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Mobile.ViewModels
{
    public class NewProjectViewModel : BaseViewModel
    {
        private string _title;
        private string _description;

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

        public Command AddProjectsFromFirebase { get; }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public NewProjectViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            AddProjectsFromFirebase = new Command(ExecutLoadFromFirebase);

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private async void ExecutLoadFromFirebase()
        {
            try
            {
                var storageImage = await new FirebaseStorage("zwinnycrud.appspot.com")
                    .Child("UserData").Child("Projects.json").GetDownloadUrlAsync();
                HttpClient client = new HttpClient();
                using (var result = await client.GetAsync(storageImage))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        var content = await result.Content.ReadAsStringAsync();
                        Debug.WriteLine(content);

                        var data = JsonConvert.DeserializeObject<ProjectDto>(content);
                        Title = data.Title;
                        Description = data.Description;

                        await new FirebaseStorage("zwinnycrud.appspot.com")
                            .Child("UserData").Child("Projects.json").DeleteAsync();
                    }
                }
            }
            catch (System.Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", "Niestety nie udało się dodać danych z Firebase. Sprawdz, czy na pewno znajduje się tam plik Projects.json i że ma on poprawną składnie.", "Ok");
            }
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(_title)
                && !String.IsNullOrWhiteSpace(_description);
        }

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
