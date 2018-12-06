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
    public class ExportExcelPageViewModel :ViewModelBase
    {
        private IFrameNavigationService _navigationService;
        private ILogger _logService;
        private IExcelWriterService _excelwriterService;
        private IDBService _dbService;
        private IOpenFileDialogService _openFileDialogService;
        public ExportExcelPageViewModel(IFrameNavigationService navigationService, ILogger logService, IExcelWriterService excelWriterService, IDBService dBService, IOpenFileDialogService openFileDialogService)
        {
            _navigationService = navigationService;
            _logService = logService;
            _excelwriterService = excelWriterService;
            _dbService = dBService;
            _openFileDialogService = openFileDialogService;

            ConfigNavigationCommands();

            ExportExcelFile = new RelayCommand(ExportExcelFileCommand);
            SelectSaveLocation = new RelayCommand(SelectSaveLocationCommand);


            OutputMessages = new ObservableCollection<OutputMessage>();

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

        private string savepath;
        public ObservableCollection<OutputMessage> OutputMessages { get; set; }

        public RelayCommand SelectSaveLocation { get; private set; }
        private void SelectSaveLocationCommand()
        {
            var resu = _openFileDialogService.ShowSaveFileDialog(".xlsx", "Excel Files (*.xlsx)| All files (*.*)");
            if (String.IsNullOrEmpty(resu))
            {

                savepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Inventory.xlsx";
                OutputMessages.Add(new OutputMessage { Message = "Using default save location", Level = "" });
            }
            else
            {
                savepath = resu;
                OutputMessages.Add(new OutputMessage { Message = savepath + " added as Save Location", Level = "" });
            }
            
            
            
        }


        public RelayCommand ExportExcelFile { get; private set; }
        private async void ExportExcelFileCommand()
        {
            SaveProgressVisibility = "visible";
            if (String.IsNullOrEmpty(savepath))
            {

                savepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Inventory.xlsx";
                OutputMessages.Add(new OutputMessage { Message = "Using default save location", Level = "" });
            }
            //savepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\savedmolecules.xlsx";
            var molecules = await _dbService.GetMoleculesAsync();
            _excelwriterService.ExportExcelAsync(savepath, molecules);
            OutputMessages.Add(new OutputMessage { Message = "Excel document saved!", Level = "" });
            SaveProgressVisibility = null;
            
            
        }

        private string _saveprogressvisibility;
        public string SaveProgressVisibility { get { return _saveprogressvisibility; } set { Set(ref _saveprogressvisibility, value); } }







    }
}
