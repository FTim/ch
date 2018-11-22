using ChClient.Models;
using ChClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.ViewModels
{
    public class BrowseProjectsPageViewModel :ViewModelBase
    {
        private IFrameNavigationService _navigationService;
        private ILogger _logService;
        private IDBService _dbService;
        public BrowseProjectsPageViewModel(IFrameNavigationService navigationService, ILogger logService, IDBService dbService)
        {
            _navigationService = navigationService;
            _logService = logService;
            _dbService = dbService;
            Person = navigationService.Parameter.ToString();
            ConfigNavigationCommands();

            ProjectList = new ObservableCollection<Project>();

            GetProjects = new RelayCommand(GetProjectsCommand);

            ViewProject = new RelayCommand<Project>(ViewProjectCommand);
        }
        private string _person;
        public string Person { get { return _person; } set { Set(ref _person, value); } }
        

        private void ConfigNavigationCommands()
        {
            Home = new RelayCommand(HomeCommand);
            NewReaction = new RelayCommand(NewReactionCommand);
            BrowseAllProjects = new RelayCommand(BrowseAllProjectsCommand);
            BrowseMyProjects = new RelayCommand(BrowseMyProjectsCommand);
            NewProject = new RelayCommand(NewProjectCommand);
            BrowseAllReactions = new RelayCommand(BrowseAllReactionsCommand);
            BrowseMyReactions = new RelayCommand(BrowseMyReactionsCommand);
            AddNewMolecule = new RelayCommand(AddNewMoleculeCommand);
            ManualInventoryUpdate = new RelayCommand(ManualInventoryUpdateCommand);
            ExportExcel = new RelayCommand(ExportExcelCommand);
        }

        #region Commands - Navigation
        public RelayCommand Home { get; private set; }
        private void HomeCommand()
        {
            _navigationService.NavigateTo("Home");
        }

        public RelayCommand NewProject { get; private set; }
        private void NewProjectCommand()
        {
            _navigationService.NavigateTo("NewProject");
        }

        public RelayCommand BrowseAllProjects { get; private set; }
        private void BrowseAllProjectsCommand()
        {
            _navigationService.NavigateTo("BrowseProjects", "all");
        }

        public RelayCommand BrowseMyProjects { get; private set; }
        private void BrowseMyProjectsCommand()
        {
            _navigationService.NavigateTo("BrowseProjects", "my");
        }

        public RelayCommand NewReaction { get; private set; }
        private void NewReactionCommand()
        {
            _navigationService.NavigateTo("NewReaction");
        }

        public RelayCommand BrowseAllReactions { get; private set; }
        private void BrowseAllReactionsCommand()
        {
            _navigationService.NavigateTo("BrowseReactions", "all");
        }

        public RelayCommand BrowseMyReactions { get; private set; }
        private void BrowseMyReactionsCommand()
        {
            _navigationService.NavigateTo("BrowseReactions", "my");
        }

        public RelayCommand AddNewMolecule { get; private set; }
        private void AddNewMoleculeCommand()
        {
            _navigationService.NavigateTo("AddNewMolecule");
        }

        public RelayCommand ManualInventoryUpdate { get; private set; }
        private void ManualInventoryUpdateCommand()
        {
            _navigationService.NavigateTo("ManualInventoryUpdate");
        }

        public RelayCommand ExportExcel { get; private set; }
        private void ExportExcelCommand()
        {
            _navigationService.NavigateTo("ExportExcel");
        }
        #endregion

        public RelayCommand GetProjects { get;  private set; }
        private async void GetProjectsCommand()
        {
            ProjectList.Clear();
            List<Project> tmp = new List<Project>();
            tmp= await _dbService.GetProjects();
            foreach (var item in tmp)
            {
                ProjectList.Add(item);
            }
        }

        public RelayCommand<Project> ViewProject { get; private set; }
        private void ViewProjectCommand(Project viewThis)
        {
            _navigationService.NavigateTo("Project", viewThis);
        }

        public ObservableCollection<Project> ProjectList { get; set; }

    }
}
