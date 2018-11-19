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
        private ILogger _loggerService;
        private IOpenFileDialogService _openFileDialogService;
        private IDBService _dbService;

        #endregion

        public NewProjectPageViewModel(IFrameNavigationService navigationService, ILogger loggerService, IOpenFileDialogService openFileDialogService, IDBService dBService)
        {
            _navigationService = navigationService;
            _openFileDialogService = openFileDialogService;
             _loggerService = loggerService;
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

                _dbService.AddProject(ProjectName, Leader, Goal, Description, PlanImgPath);
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
                _loggerService.Write(this, item.Message, item.Level);
            }
            return result;
        }
        #endregion
    }
}
