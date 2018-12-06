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
    public class ManualInventoryUpdatePageViewModel : ViewModelBase
    {
        private IFrameNavigationService _navigationService;
        private ILogger _logService;
        private IDBService _dbService;
        public ManualInventoryUpdatePageViewModel(IFrameNavigationService navigationService, ILogger logService, IDBService dBService)
        {
            _navigationService = navigationService;
            _logService = logService;
            _dbService = dBService;

            ConfigNavigationCommands();

            FoundMolecules = new ObservableCollection<SelectedMolecule>();
            _allmolecules = new List<SelectedMolecule>();
            OutputMessages = new ObservableCollection<OutputMessage>();
            Add = new RelayCommand(AddCommand);
            ExplicitModify = new RelayCommand(ExplicitModifyCommand);
            GetResources = new RelayCommand(GetResourcesCommandAsync);
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

        public RelayCommand GetResources { get; private set; }
        private async void GetResourcesCommandAsync()
        {
            _logService.Write(this, "Reading molecules from database started", "debug");
            _allmolecules =await _dbService.GetMoleculesAsync();
            _logService.Write(this, _allmolecules.Count + " molecule(s) readed", "debug");
            foreach (var item in _allmolecules)
            {
                FoundMolecules.Add(item);
            }
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

        #region Commnds - Logic
        public RelayCommand Add { get; set; }
        private void AddCommand()
        {
            double tmp;
            if(Double.TryParse(ModifyAmount, out tmp))
            {
                if (Selected.mAvailable.HasValue)
                {
                    double newValue= Selected.mAvailable.Value + tmp;

                    if (newValue > 0)
                    {
                        Selected.mAvailable += tmp;
                        RaisePropertyChanged(nameof(Selected));
                        _logService.Write(this, "Update inventory with: " + Selected.CAS + " " + Selected.Location + " " + newValue + " " + null, "debug");
                        _dbService.ModifyMoleculeAvailable(Selected.CAS, Selected.Location, newValue, null);
                        OutputMessages.Add(new OutputMessage { Message = "Inventory updated!", Level = "debug" });
                        _logService.Write(this, "Inventory updated!", "debug");
                    }
                    else
                    {
                        OutputMessages.Add(new OutputMessage { Message = "Amount cannot be negative!", Level = "error" });
                        _logService.Write(this, "Amount cannot be negative!", "error");
                    }

                    
                }
                else
                {
                    double newValue = Selected.VAvailable.Value + tmp;

                    if (newValue > 0)
                    {
                        Selected.VAvailable += tmp;
                        RaisePropertyChanged(nameof(Selected));
                        _logService.Write(this, "Update inventory with: " + Selected.CAS + " " + Selected.Location + " " + null + " " + newValue, "debug");
                        _dbService.ModifyMoleculeAvailable(Selected.CAS, Selected.Location, null, newValue);
                        OutputMessages.Add(new OutputMessage { Message = "Inventory updated!", Level = "" });
                        _logService.Write(this, "Inventory updated!", "debug");
                    }
                    else
                    {
                        OutputMessages.Add(new OutputMessage { Message = "Amount cannot be negative!", Level = "error" });
                        _logService.Write(this, "Amount cannot be negative!", "error");
                    }

                }
            }
            else
            {
                OutputMessages.Add(new OutputMessage { Message = "Cannot convert given value to number", Level = "error" });
                _logService.Write(this, "Cannot convert given value to number", "error");
            }

        }

        public RelayCommand ExplicitModify { get; set; }
        private void ExplicitModifyCommand()
        {
            double tmp;
            if (Double.TryParse(ModifyAmount, out tmp))
            {
                if (tmp < 0)
                {
                    OutputMessages.Add(new OutputMessage { Message = "Amount cannot be negative!", Level = "error" });
                    _logService.Write(this, "Amount cannot be negative!", "error");
                }
                else
                {
                    if (Selected.mAvailable.HasValue)
                    {
                        Selected.mAvailable = tmp;
                        RaisePropertyChanged(nameof(Selected));
                        _logService.Write(this, "Update inventory with: " + Selected.CAS + " " + Selected.Location + " " + tmp + " " + null, "debug");

                        _dbService.ModifyMoleculeAvailable(Selected.CAS, Selected.Location, tmp, null);
                        OutputMessages.Add(new OutputMessage { Message = "Inventory updated!", Level = "" });
                        _logService.Write(this, "Inventory updated!", "debug");
                    }
                    else
                    {
                        Selected.VAvailable = tmp;
                        RaisePropertyChanged(nameof(Selected));
                        _logService.Write(this, "Update inventory with: " + Selected.CAS + " " + Selected.Location + " " + null + " " + tmp, "debug");

                        _dbService.ModifyMoleculeAvailable(Selected.CAS, Selected.Location, null, tmp);
                        OutputMessages.Add(new OutputMessage { Message = "Inventory updated!", Level = "" });
                        _logService.Write(this, "Inventory updated!", "debug");
                    }
                }
            }
            else
            {
                OutputMessages.Add(new OutputMessage { Message = "Cannot convert given value to number", Level = "error" });
                _logService.Write(this, "Cannot convert given value to number", "error");
            }
        }
        #endregion

        #region Bindings
        public ObservableCollection<SelectedMolecule> FoundMolecules { get; set; }
        private List<SelectedMolecule> _allmolecules;
        private SelectedMolecule _selected;
        public SelectedMolecule Selected { get { return _selected; } set { Set(ref _selected, value); } }
        private string _searchedtext;
        public string SearchedText { get { return _searchedtext; } set { Set(ref _searchedtext, value); RefreshList(); } }

        

        private string _modifyamount;
        public string ModifyAmount { get { return _modifyamount; } set { Set(ref _modifyamount, value); } }

        public void RefreshList()
        {
            FoundMolecules.Clear();
            List<SelectedMolecule> tmp = new List<SelectedMolecule>();
            foreach (var item in _allmolecules)
            {
                if (item.Name.ToUpper().Contains(_searchedtext.ToUpper()))
                {
                    tmp.Add(item);
                }
                if (item.CAS.ToUpper().Contains(_searchedtext.ToUpper()))
                {
                    tmp.Add(item);
                }

            }
            tmp = tmp.Distinct().ToList();

            foreach (var item in tmp)
            {
                FoundMolecules.Add(item);
            }
        }

        public ObservableCollection<OutputMessage> OutputMessages { get; set; }
        #endregion
    }
}
