﻿using ChClient.Models;
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
    public class BrowseReactionsPageViewModel :ViewModelBase
    {
        #region Services
        private IFrameNavigationService _navigationService;
        private ILogger _logService;
        private IDBService _dbService;
        #endregion
        public BrowseReactionsPageViewModel(IFrameNavigationService navigationService, ILogger logService, IDBService dBService)
        {
            _navigationService = navigationService;
            _logService = logService;
            _dbService = dBService;
            NavigationServiceParameter tmp = (NavigationServiceParameter)navigationService.Parameter;
            Person = tmp.Person;
            Mode = tmp.Mode;
            ConfigNavigationCommands();

            Config();
        }



        #region Commands - Navigation
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


        #region Person&Mode bindings + Logic Commands
        private void Config()
        {
            ReactionList = new ObservableCollection<ReactionInfo>();
            GetReactions = new RelayCommand(GetReactionsCommand);
            ViewReaction = new RelayCommand<ReactionInfo>(ViewReactionCommand);
            DeleteReaction = new RelayCommand<ReactionInfo>(DeleteReactionCommand);

            OutputMessages = new ObservableCollection<OutputMessage>();
        }
        public ObservableCollection<OutputMessage> OutputMessages { get; set; }

        private string _person;
        public string Person { get { return _person; } set { Set(ref _person, value); } }

        private string _mode;
        public string Mode { get { return _mode; } set { Set(ref _mode, value); } }
        public RelayCommand GetReactions { get; private set; }
        private async void GetReactionsCommand()
        {
            ReactionList.Clear();
            List<ReactionInfo> tmp = new List<ReactionInfo>();
            if (Mode == "my")
            {
                OutputMessages.Add(new OutputMessage { Message = "Loading " + CurrentUser + "'s reaction(s)...", Level = "" });
                _logService.Write(this, "Loading " + CurrentUser + "'s reaction(s)...", "debug");
                tmp = await _dbService.GetReactions(Person);
            }
            else
            {
                OutputMessages.Add(new OutputMessage { Message = "Loading all reaction(s)...", Level = "" });
                _logService.Write(this, "Loading all reaction(s)...", "debug");
                tmp = await _dbService.GetReactions();
            }
            OutputMessages.Add(new OutputMessage { Message = tmp.Count + " reaction(s) loaded!", Level = "" });
            _logService.Write(this, tmp.Count + " reaction(s) loaded", "debug");
            foreach (var item in tmp)
            {
                ReactionList.Add(item);
            }
        }

        public RelayCommand<ReactionInfo> ViewReaction { get; private set; }
        private void ViewReactionCommand(ReactionInfo viewThis)
        {
            _logService.Write(this, "Navigate to: Reaction page");
            _navigationService.NavigateTo("Reaction", new NavigationServiceParameter() { Person = CurrentUser, ObjParam = viewThis });
        }

        public ObservableCollection<ReactionInfo> ReactionList { get; set; }

        public RelayCommand<ReactionInfo> DeleteReaction { get; set; }
        private async void DeleteReactionCommand(ReactionInfo deleteThis)
        {
            try
            {
                OutputMessages.Add(new OutputMessage { Message = "Deleting reaction...", Level = "" });
                _logService.Write(this, "Deleting reaction...", "debug");
                await _dbService.DeleteReaction(deleteThis.ReactionID);
                ReactionList.Remove(deleteThis);
                OutputMessages.Add(new OutputMessage { Message = "Reaction deleted!", Level = "" });
                _logService.Write(this, "Reaction(s) deleted!", "debug");
            }
            catch (Exception e)
            {
                OutputMessages.Add(new OutputMessage { Message = e.Message, Level = "fatal" });
                _logService.Write(this, e.Message, "fatal");
            }



        }
        #endregion
    }
}
