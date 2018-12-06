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
    public class AddNewMoleculePageViewModel :ViewModelBase
    {
        #region Services
        private IFrameNavigationService _navigationService;
        private ILogger _logService;
        private IDBService _dbService;
        #endregion
        public AddNewMoleculePageViewModel(IFrameNavigationService navigationService, ILogger logService, IDBService dBService)
        {
            _navigationService = navigationService;
            _logService = logService;
            _dbService = dBService;
           
            ConfigNavigationCommands();
            Config();
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

        #region Bindings + Logic Command
        private void Config()
        {
            AmountType = new ObservableCollection<string>();
            AmountType.Add("m (g)");
            AmountType.Add("V (ml)");

            OutputMessages = new ObservableCollection<OutputMessage>();

            AddMolecule = new RelayCommand(AddMoleculeCommandAsync);

            _Molecule = new SelectedMolecule();
        }
        private string _name;
        private string _cas;
        private string _location;
        private string _mw;
        private string _type;
        private string _amount;
        private string _den;
        private string _mp;
        private string _bp;
        private string _purity;

        private SelectedMolecule _Molecule;

        public string Name { get { return _name; } set { Set(ref _name, value); _Molecule.Name = value; } }
        public string CAS { get { return _cas; } set { Set(ref _cas, value); _Molecule.CAS = value; } }
        public string Location { get { return _location; } set { Set(ref _location, value); _Molecule.Location = value; } }
        public string MW { get { return _mw; } set { Set(ref _mw, value); _Molecule.MWString = value; } }
        public ObservableCollection<string> AmountType { get; set; }
        public string SelectedType { get { return _type; } set { Set(ref _type, value);} }
        public string Amount { get { return _amount; } set { Set(ref _amount, value); _Molecule.Amount = value; } }
        public string Den { get { return _den; } set { Set(ref _den, value); _Molecule.DenString = value; } }
        public string Mp { get { return _mp; } set { Set(ref _mp, value); _Molecule.mpValue = value; } }
        public string Bp { get { return _bp; } set { Set(ref _bp, value); _Molecule.bpValue = value; } }
        public string Purity { get { return _purity; } set { Set(ref _purity, value); _Molecule.Purity = value; } }
        public ObservableCollection<OutputMessage> OutputMessages { get; set; }
        public RelayCommand AddMolecule { get; private set; }
        private async void AddMoleculeCommandAsync()
        {
            if (ValidateMolecule())
            {
                try
                {
                    string tmp = await _dbService.AddMolecule(_Molecule);
                    OutputMessages.Add(new OutputMessage() { Message = tmp, Level = "debug" });
                    _logService.Write(this, tmp, "debug");
                }
                catch (Exception e)
                {
                    OutputMessages.Add(new OutputMessage() { Message = "Molecule cannot save to database, read the log file for more info!", Level = "fatal" });
                    _logService.Write(this, e.InnerException.InnerException.Message, "fatal");
                }
            }
        }
        #endregion

        #region Model
        private bool ValidateMolecule()
        {
            bool result = true;
            OutputMessages.Add(new OutputMessage() { Message = "Validate molecule inputs", Level="" });
            List<OutputMessage> tmp=_Molecule.Validate(_type);

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
