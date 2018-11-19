using ChClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private IFrameNavigationService _navigationService;
        private ILogger _logService;
        private IDBService _dbService;
        
        public HomePageViewModel(IFrameNavigationService navigationService, ILogger logService, IDBService dBService)
        {
            
            _navigationService = navigationService;
            _logService = logService;

            _dbService = dBService;

            Messenger.Default.Register<NotificationMessage>(this, OtherUserSelected);
            ConfigNavigationCommands();
            CurrentUser = "-";
        }

        private void OtherUserSelected(NotificationMessage notificationMessage)
        {
            CurrentUser = notificationMessage.Notification;
        }

       

        private void ConfigNavigationCommands()
        {
            ChangeUser = new RelayCommand(ChangeUserCommand);
            DatabaseReset = new RelayCommand(DatabaseResetCommand);
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
        public RelayCommand ChangeUser { get; private set; }
        private void ChangeUserCommand()
        {
            _navigationService.NavigateTo("ManageUsers", CurrentUser);
        }

        public RelayCommand DatabaseReset { get; private set; }
        private void DatabaseResetCommand()
        {
            _navigationService.NavigateTo("DatabaseReset");
        }
        public RelayCommand NewProject { get; private set; }
        private void NewProjectCommand()
        {
            _logService.Write(this, "Navigate to: New Project page");
            _navigationService.NavigateTo("NewProject");
        }

        public RelayCommand BrowseAllProjects { get; private set; }
        private void BrowseAllProjectsCommand()
        {
            _logService.Write(this, "Navigate to: Browse All Projects page");
            _navigationService.NavigateTo("BrowseProjects", "all");
        }

        public RelayCommand BrowseMyProjects { get; private set; }
        private void BrowseMyProjectsCommand()
        {
            _logService.Write(this, "Navigate to: Browse My Projects page");
            _navigationService.NavigateTo("BrowseProjects", "my");
        }

        public RelayCommand NewReaction { get; private set; }
        private void NewReactionCommand()
        {
            _logService.Write(this, "Navigate to: New Reaction page");
            _navigationService.NavigateTo("NewReaction");
        }

        public RelayCommand BrowseAllReactions { get; private set; }
        private void BrowseAllReactionsCommand()
        {
            _logService.Write(this, "Navigate to: Browse All Reactions page");
            _navigationService.NavigateTo("BrowseReactions", "all");
        }

        public RelayCommand BrowseMyReactions { get; private set; }
        private void BrowseMyReactionsCommand()
        {
            _logService.Write(this, "Navigate to: Browse My Reactions page");
            _navigationService.NavigateTo("BrowseReactions", "my");
        }

        public RelayCommand AddNewMolecule { get; private set; }
        private void AddNewMoleculeCommand()
        {
            _logService.Write(this, "Navigate to: Add New Molecule page");
            _navigationService.NavigateTo("AddNewMolecule");
        }

        public RelayCommand ManualInventoryUpdate { get; private set; }
        private void ManualInventoryUpdateCommand()
        {
            _logService.Write(this, "Navigate to: Manual Inventory Update page");
            _navigationService.NavigateTo("ManualInventoryUpdate");
        }

        public RelayCommand ExportExcel { get; private set; }
        private void ExportExcelCommand()
        {
            _logService.Write(this, "Navigate to: Export Excel page");
            _navigationService.NavigateTo("ExportExcel");
        }
        #endregion

        #region Bindings
        private string _currentuser;
        public string CurrentUser { get { return _currentuser; } set { Set(ref _currentuser, value); } }
        #endregion



    }


}

