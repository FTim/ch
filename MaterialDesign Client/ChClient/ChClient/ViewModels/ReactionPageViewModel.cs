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
    public class ReactionPageViewModel :ViewModelBase
    {
        #region Services
        private IFrameNavigationService _navigationService;
        private IOpenFileDialogService _openFileDialogService;
        private ILogger _logService;
        private IDBService _dbService;
        

        #endregion
        public ReactionPageViewModel(IFrameNavigationService navigationService, IOpenFileDialogService openFileDialogService, ILogger loggerService, IDBService dBService)
        {
            _navigationService = navigationService;
            _openFileDialogService = openFileDialogService;
            _logService = loggerService;
            _dbService = dBService;
            

            
           // _reaction = (ReactionInfo)_navigationService.Parameter;
            _reaction = ((NavigationServiceParameter)_navigationService.Parameter).ObjParam as ReactionInfo;
            _reactionId = _reaction.ReactionID;
            ConfigNavigationCommands();
            FinishSketchEnabled = null;
            FinishSketch = new RelayCommand(FinishSketchCommand);
            SketchAvailable = null;


            StartingMaterial = new ObservableCollection<StartingMaterial>();
            Reagents = new ObservableCollection<Reagent>();
            Solvents = new ObservableCollection<Solvent>();
            Products = new ObservableCollection<Product>();
            ObservationImgsByteArray = new ObservableCollection<byte[]>();
            ObservationImgsFilePaths = new ObservableCollection<string>();

            GetResources = new RelayCommand(GetResourcesCommand);
            SelectObservationImg = new RelayCommand(SelectObservationImgCommand);
            DeleteObservationImg = new RelayCommand<string>(DeleteObservationImgCommand);

            SaveReaction = new RelayCommand(SaveReactionCommandAsync);
            OutputMessages = new ObservableCollection<OutputMessage>();
        }

        private int _reactionId;
        private ReactionInfo _reaction;
        public ObservableCollection<OutputMessage> OutputMessages { get; set; }

        private void ConfigureReactionParameter()
        {
            Code = _reaction.Code;
            Chemist = _reaction.Chemist;
            Chiefchemist = _reaction.Chiefchemist;
            Project = _reaction.Project;
            Laboratory = _reaction.Laboratory;
            StartDate = _reaction.StartDate;
            PreviousStep = _reaction.PreviousStep;
            Literature = _reaction.Literature;
            IsSketch = _reaction.IsSketch;

            ReactionImgByteArray = _reaction.ReactionImgByteArray;

            ClosureDate = DateTime.Now;
            if (!_reaction.IsSketch)
            {
                ClosureDate = _reaction.ClosureDate;
                Procedure = _reaction.Procedure;
                Observation = _reaction.Observation;
                Yield = _reaction.Yield;
            }
            else
            {
                //enable modify-> closure date, proc, obs, obs img yield
            }


            
        }
        private string _finishsketchenabled;
        public string FinishSketchEnabled { get { return _finishsketchenabled; } set { Set(ref _finishsketchenabled, value); } }
        private string _sketchavailable;
        public string SketchAvailable { get { return _sketchavailable; } set { Set(ref _sketchavailable, value); } }

        //SketchAvailable
        public RelayCommand FinishSketch { get; set; }
        private void FinishSketchCommand()
        {
            FinishSketchEnabled = "enabled";
            SketchAvailable = "available";

        }

        public ObservableCollection<StartingMaterial> StartingMaterial { get; set; }
        public ObservableCollection<Reagent> Reagents { get; set; }
        public ObservableCollection<Solvent> Solvents { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<byte[]> ObservationImgsByteArray { get; set; }
        public ObservableCollection<string> ObservationImgsFilePaths { get; set; }
        public RelayCommand GetResources { get; private set; }
        private async void GetResourcesCommand()
        {
            _reaction =await  _dbService.GetReactionAsync(_reactionId);
            var tmpsm = await _dbService.GetStartingMaterial(_reactionId);
            var tmpr = await _dbService.GetReagents(_reactionId);
            var tmps = await _dbService.GetSolvents(_reactionId);
            var tmpp = await _dbService.GetProducts(_reactionId);
            var tmpobs = await _dbService.GetObsImgs(_reactionId);
            StartingMaterial.Add(tmpsm);
            foreach (var item in tmpr)
            {
                Reagents.Add(item);
            }
            foreach (var item in tmps)
            {
                Solvents.Add(item);
            }
            foreach (var item in tmpp)
            {
                Products.Add(item);
            }
            foreach (var item in tmpobs)
            {
                ObservationImgsByteArray.Add(item);
            }
            ConfigureReactionParameter();
        }

        public RelayCommand SelectObservationImg { get; set; }
        private void SelectObservationImgCommand()
        {
            var resu = _openFileDialogService.ShowOpenFileDialog();
            ObservationImgsFilePaths.Add(resu);
            OutputMessages.Add(new OutputMessage() { Message = resu + " added as Observation Img", Level = "" });
        }

        public RelayCommand<string> DeleteObservationImg { get; private set; }
        private void DeleteObservationImgCommand(string deleteThis)
        {

            ObservationImgsFilePaths.Remove(deleteThis);

        }

        public RelayCommand SaveReaction { get; private set; }
        private async void SaveReactionCommandAsync()
        {
            OutputMessages.Add(new OutputMessage() { Message = "Saving to database", Level = "" });
            await _dbService.FinishSketchReaction(_reactionId, ClosureDate, Procedure, Observation, Yield, ObservationImgsFilePaths.ToList());
            OutputMessages.Add(new OutputMessage() { Message = "Finished!", Level = "" });
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
        private string _code;
        private string _chemist;
        private string _chiefchemist;
        private string _project;
        private string _laboratory;
        private DateTime _startdate;
        private DateTime _closuredate;
        private string _previousstep;
        private string _literature;
        private byte[] _reactionimgbytearray;
        private string _procedure;
        private string _yield;
        private string _observation;
        private bool _issketch;
        private string _observationimgpath;

        public string Code { get { return _code; } set { Set(ref _code, value);} }
        public string Chemist { get { return _chemist; } set { Set(ref _chemist, value);  } }
        public string Chiefchemist { get { return _chiefchemist; } set { Set(ref _chiefchemist, value);  } }
        public string Project { get { return _project; } set { Set(ref _project, value);  } }
        public string Laboratory { get { return _laboratory; } set { Set(ref _laboratory, value);  } }
        public DateTime StartDate { get { return _startdate; } set { Set(ref _startdate, value);  } }
        public DateTime ClosureDate { get { return _closuredate; } set { Set(ref _closuredate, value);  } }
        public string PreviousStep { get { return _previousstep; } set { Set(ref _previousstep, value);  } }
        public string Literature { get { return _literature; } set { Set(ref _literature, value);  } }
        public string Procedure { get { return _procedure; } set { Set(ref _procedure, value);  } }
        public string Yield { get { return _yield; } set { Set(ref _yield, value);  } }
        public string Observation { get { return _observation; } set { Set(ref _observation, value);  } }
        public byte[] ReactionImgByteArray { get { return _reactionimgbytearray; } set { Set(ref _reactionimgbytearray, value);  } }
        public bool IsSketch { get { return _issketch; } set { Set(ref _issketch, value);  } }
        //public ObservableCollection<string> ObservationImgPaths { get; set; }
        public string ObservationImgPath { get { return _observationimgpath; } set { Set(ref _observationimgpath, value); } }


        private string _savelocation;
        public string SaveLocation { get { return _savelocation; } set { Set(ref _savelocation, value); } }
        #endregion
    }
}
