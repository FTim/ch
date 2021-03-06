﻿using ChClient.Models;
using ChClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.ViewModels
{
    public class ProjectPageViewModel :ViewModelBase
    {
        private Project _project;
        private IFrameNavigationService _navigationService;
        private IOpenFileDialogService _openFileDialogService;
        private ILogger _logService;
        private IDBService _dbService;
        public ProjectPageViewModel(IFrameNavigationService navigationService, IOpenFileDialogService openFileDialogService, ILogger loggerService, IDBService dBService)
        {
            _navigationService = navigationService;
            _openFileDialogService = openFileDialogService;
            _logService = loggerService;
            _dbService = dBService;

            OutputMessages = new ObservableCollection<OutputMessage>();
            
            _project = ((NavigationServiceParameter)_navigationService.Parameter).ObjParam as Project;
            OutputMessages.Add(new OutputMessage { Message = _project.Name + " details", Level = "" });
            ProjectName = _project.Name;
            Leader = _project.Leader;
            Goal = _project.Goal;
            Description = _project.Description;
            Plans = new ObservableCollection<byte[]>();
            foreach (var item in _project.ProjectPlanByreArrays)
            {
                Plans.Add(item);
            }
            Reactions = new ObservableCollection<ReactionInfo>();
            Modify = null;
            ConfigNavigationCommands();

            Modify = new RelayCommand(ModifyCommand);
            AddPlan = new RelayCommand(AddPlanCommand);
            GetResources = new RelayCommand(GetResourcesCommand);

            ViewReaction = new RelayCommand<ReactionInfo>(ViewReactionCommand);
            DeleteReaction = new RelayCommand<ReactionInfo>(DeleteReactionCommand);

            SaveProject = new RelayCommand(SaveProjectCommand);

        }

        

        public RelayCommand GetResources { get; private set; }
        private async void GetResourcesCommand()
        {
            _logService.Write(this, "Loading project with ID " + _project.ProjectID, "debug");
            var tmplist = await _dbService.GetReactions(_project.ProjectID);
            _logService.Write(this, "Loaded " + _project.ProjectID, "debug");
            foreach (var item in tmplist)
            {
                Reactions.Add(item);
            }
            OutputMessages.Add(new OutputMessage { Message = Reactions.Count + " reactions loaded for this project", Level = "debug" });
            _logService.Write(this, Reactions.Count + " reactions loaded for this project", "debug");
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
            _logService.Write(this, "Navigate to: Home page");
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


        public string ProjectName { get { return _projectname; } set { Set(ref _projectname, value); } }
        public string Leader { get { return _leader; } set { Set(ref _leader, value);  } }
        public string Goal { get { return _goal; } set { Set(ref _goal, value);  } }
        public string Description { get { return _description; } set { Set(ref _description, value); } }

        public ObservableCollection<byte[]> Plans { get; set; }

        public ObservableCollection<ReactionInfo> Reactions { get; set; }
        #endregion

        private string _modifyavailable;
        public string ModifyAvailable { get { return _modifyavailable; } set { Set(ref _modifyavailable, value); } }

        public RelayCommand Modify { get; set; }
        private void ModifyCommand()
        {
            ModifyAvailable = "available";
        }
        private string _newprojectplan;
        public string NewProjectPlan { get { return _newprojectplan; } set { Set(ref _newprojectplan, value); } }

        public RelayCommand AddPlan { get; private set; }
        private void AddPlanCommand()
        {
            var resu = _openFileDialogService.ShowOpenFileDialog();
            
            if (resu != null)
            {
                NewProjectPlan=resu;
                OutputMessages.Add(new OutputMessage { Message = resu + " added as New Project Plan", Level = "debug" });
                _logService.Write(this, resu + " added as New Project Plan", "debug");

            }


            else
            {
                OutputMessages.Add(new OutputMessage { Message = "No image added as New Project Plan", Level = "debug" });
                _logService.Write(this, "No image added as New Project Plan", "debug");
            }

        }

        public RelayCommand<ReactionInfo> ViewReaction { get; set; }
        private void ViewReactionCommand(ReactionInfo viewThis)
        {
            _logService.Write(this, "Navigate to: Project page");
            _navigationService.NavigateTo("Reaction", new NavigationServiceParameter() { Person = CurrentUser, ObjParam = viewThis });
        }
        public RelayCommand<ReactionInfo> DeleteReaction { get; set; }
        private async void DeleteReactionCommand(ReactionInfo deleteThis)
        {
            try
            {
                OutputMessages.Add(new OutputMessage { Message = "Deleting reaction(s)...", Level = "debug" });
                _logService.Write(this, "Deleting reaction(s)...", "debug");
                await _dbService.DeleteReaction(deleteThis.ReactionID);
                Reactions.Remove(deleteThis);
                OutputMessages.Add(new OutputMessage { Message = "Reaction(s) deleted!", Level = "debug" });
                _logService.Write(this, "Deleting reaction(s)...", "debug");
            }
            catch (Exception e)
            {
                OutputMessages.Add(new OutputMessage { Message = e.Message, Level = "fatal" });
                _logService.Write(this, e.Message, "fatal");
            }
            
        }

        public RelayCommand SaveProject { get; private set; }
        private void SaveProjectCommand()
        {
            
            _dbService.UpdateProject(_project.ProjectID, ProjectName, Leader, Goal, Description, NewProjectPlan);
            _logService.Write(this, "Project modified", "debug");
            OutputMessages.Add(new OutputMessage { Message = "Project modified!", Level = "" });
        }

        public ObservableCollection<OutputMessage> OutputMessages { get; set; }
        
    }
}
