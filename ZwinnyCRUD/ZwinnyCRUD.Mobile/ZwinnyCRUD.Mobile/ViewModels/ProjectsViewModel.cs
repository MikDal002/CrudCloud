using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using ZwinnyCRUD.Common.Models;
using ZwinnyCRUD.Mobile.Views;

namespace ZwinnyCRUD.Mobile.ViewModels
{
    public class ProjectsViewModel : BaseViewModel
    {
        private Project _selectedProject;

        public ObservableCollection<Project> Projects { get; }
        public Command LoadProjectsCommand { get; }
        public Command AddProjectCommand { get; }
        public Command AddImageCommand { get; }
        public Command<Project> ProjectTapped { get; }

        public ProjectsViewModel()
        {
            Title = "Browse";
            Projects = new ObservableCollection<Project>();
            LoadProjectsCommand = new Command(async () => await ExecuteLoadProjectsCommand());

            ProjectTapped = new Command<Project>(OnProjectSelected);

            AddProjectCommand = new Command(OnAddProject);

            AddImageCommand = new Command(OnAddImage);
        }

        async System.Threading.Tasks.Task ExecuteLoadProjectsCommand()
        {
            IsBusy = true;

            try
            {
                Projects.Clear();
                var items = await DataStore.GetProjectsAsync(true);
                foreach (var item in items)
                {
                    Projects.Add(item);
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
            SelectedProject = null;
        }

        public Project SelectedProject
        {
            get => _selectedProject;
            set
            {
                SetProperty(ref _selectedProject, value);
                OnProjectSelected(value);
            }
        }

        private async void OnAddProject(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewProjectPage));
        }

        private async void OnAddImage(object obj)
        {
            await Shell.Current.GoToAsync(nameof(FirebasePage));
        }

        async void OnProjectSelected(Project Project)
        {
            if (Project == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ProjectDetailPage)}?{nameof(ProjectDetailViewModel.ProjectId)}={Project.Id}");
        }
    }
}