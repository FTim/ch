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
    public class NewProjectPageViewModel :ViewModelBase
    {
        public ObservableCollection<OutputMessage> OutputMessages { get; set; }
        #region Services
        private IFrameNavigationService _navigationService;
        private ILogger _logService;
        private IOpenFileDialogService _openFileDialogService;
        private IDBService _dbService;

        #endregion

        public NewProjectPageViewModel(IFrameNavigationService navigationService, ILogger loggerService, IOpenFileDialogService openFileDialogService, IDBService dBService)
        {
            _navigationService = navigationService;
            _openFileDialogService = openFileDialogService;
             _logService = loggerService;
            _dbService = dBService;
           
            ConfigNavigationCommands();

            SelectProjectPlan = new RelayCommand(SelectProjectPlanCommand);
            AddProject = new RelayCommand(AddProjectCommand);

            OutputMessages = new ObservableCollection<OutputMessage>();

            _ProjectInfo = new Project();
            UsersInit();
        }
        private List<string> users;

        private async void UsersInit()
        {
           users = new List<string>();

            users = await _dbService.GetUsersAsync();
        }

        private void ConfigNavigationCommands()
        {
            CurrentUser = ((NavigationServiceParameter)_navigationService.Parameter).Person;

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
        private string _currentuser;
        public string CurrentUser { get { return _currentuser; } set { Set(ref _currentuser, value); } }
        public RelayCommand Home { get; private set; }
        private void HomeCommand()
        {
            _navigationService.NavigateTo("Home");
        }


        public RelayCommand NewProject { get; private set; }
        private void NewProjectCommand()
        {
            _logService.Write(this, "Navigate to: New Project page");
            _navigationService.NavigateTo("NewProject", new NavigationServiceParameter { Person = CurrentUser });
        }

        public RelayCommand BrowseAllProjects { get; private set; }
        private void BrowseAllProjectsCommand()
        {
            _logService.Write(this, "Navigate to: Browse All Projects page");
            _navigationService.NavigateTo("BrowseProjects", new NavigationServiceParameter { Person = CurrentUser, Mode = "all" });
        }

        public RelayCommand BrowseMyProjects { get; private set; }
        private void BrowseMyProjectsCommand()
        {
            _logService.Write(this, "Navigate to: Browse My Projects page");
            _navigationService.NavigateTo("BrowseProjects", new NavigationServiceParameter { Person = CurrentUser, Mode = "my" });
        }

        public RelayCommand NewReaction { get; private set; }
        private void NewReactionCommand()
        {
            _logService.Write(this, "Navigate to: New Reaction page");
            _navigationService.NavigateTo("NewReaction", new NavigationServiceParameter { Person = CurrentUser });
        }

        public RelayCommand BrowseAllReactions { get; private set; }
        private void BrowseAllReactionsCommand()
        {
            _logService.Write(this, "Navigate to: Browse All Reactions page");
            _navigationService.NavigateTo("BrowseReactions", new NavigationServiceParameter { Person = CurrentUser, Mode = "all" });
            //_navigationService.NavigateTo("BrowseReactions", "all");
        }

        public RelayCommand BrowseMyReactions { get; private set; }
        private void BrowseMyReactionsCommand()
        {
            _logService.Write(this, "Navigate to: Browse My Reactions page");
            _navigationService.NavigateTo("BrowseReactions", new NavigationServiceParameter { Person = CurrentUser, Mode = "my" });
            //_navigationService.NavigateTo("BrowseReactions", "my");
        }

        public RelayCommand AddNewMolecule { get; private set; }
        private void AddNewMoleculeCommand()
        {
            _logService.Write(this, "Navigate to: Add New Molecule page");
            _navigationService.NavigateTo("AddNewMolecule", new NavigationServiceParameter { Person = CurrentUser });
        }

        public RelayCommand ManualInventoryUpdate { get; private set; }
        private void ManualInventoryUpdateCommand()
        {
            _logService.Write(this, "Navigate to: Manual Inventory Update page");
            _navigationService.NavigateTo("ManualInventoryUpdate", new NavigationServiceParameter { Person = CurrentUser });
        }

        public RelayCommand ExportExcel { get; private set; }
        private void ExportExcelCommand()
        {
            _logService.Write(this, "Navigate to: Export Excel page");
            _navigationService.NavigateTo("ExportExcel", new NavigationServiceParameter { Person = CurrentUser });
        }
        #endregion


        #region Bindings - Info
        private string _projectname;
        private string _leader;
        private string _goal;
        private string _description;
        private string _planimgpath;


        public string ProjectName { get { return _projectname; } set { Set(ref _projectname, value); _ProjectInfo.Name = _projectname; } }
        public string Leader { get { return _leader; } set { Set(ref _leader, value); _ProjectInfo.Leader = _leader; } }
        public string Goal { get { return _goal; } set { Set(ref _goal, value); _ProjectInfo.Goal = _goal; } }
        public string Description { get { return _description; } set { Set(ref _description, value); _ProjectInfo.Description = _description; } }
        public string PlanImgPath { get { return _planimgpath; } set { Set(ref _planimgpath, value); _ProjectInfo.CurrentPlan = _planimgpath; } }

        #endregion


        public RelayCommand SelectProjectPlan { get; private set; }
        private void SelectProjectPlanCommand()
        {
            var resu = _openFileDialogService.ShowOpenFileDialog();
            PlanImgPath = resu;
        }
        public RelayCommand AddProject { get; private set; }
        private void AddProjectCommand()
        {
            
            if (Validate())
            {

                _dbService.AddProject(_ProjectInfo);
                OutputMessages.Add(new OutputMessage { Message = ProjectName + " added!", Level = "" });
            }
           
        }

        #region Models
        private Project _ProjectInfo;


        private bool Validate()
        {
            OutputMessages.Add(new OutputMessage { Message = "Validate project inputs", Level = "" });
            bool result = true;
            List<OutputMessage> tmp = new List<OutputMessage>();
            tmp = _ProjectInfo.Validate(users);

            foreach (var item in tmp)
            {
                if (item.Level == "error") result = false;
                OutputMessages.Add(item);
                _logService.Write(this, item.Message, item.Level);
            }
            return result;
        }
        #endregion
    }
}
