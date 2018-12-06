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
    public class NewReactionPageViewModel : ViewModelBase
    {

        public ObservableCollection<OutputMessage> OutputMessages { get; set; }
        #region Services
        private IFrameNavigationService _navigationService;
        private IOpenFileDialogService _openFileDialogService;
        private ISelectMoleculeDialogService _selectMoleculeDialogService;
        private ILogger _logService;
        private IDBService _dbService;
        private IDocxGeneratorService _docxGeneratorService;

        #endregion

        public NewReactionPageViewModel(IFrameNavigationService navigationService, IOpenFileDialogService openFileDialogService, ISelectMoleculeDialogService selectMoleculeDialogService, ILogger loggerService, IDBService dBService, IDocxGeneratorService docxGeneratorService)
        {
            _navigationService = navigationService;
            _openFileDialogService = openFileDialogService;
            _selectMoleculeDialogService = selectMoleculeDialogService;
            _logService = loggerService;
            _dbService = dBService;
            _docxGeneratorService = docxGeneratorService;

            _ReactionInfo = new ReactionInfo();

            StartDate = DateTime.Now;
            ClosureDate = DateTime.Now;

            IsSketch = false;
            ObservationImgPaths = new ObservableCollection<string>();

            OutputMessages = new ObservableCollection<OutputMessage>();

            StartingMaterialList = new ObservableCollection<StartingMaterial>();
            ReagentList = new ObservableCollection<Reagent>();
            SolventList = new ObservableCollection<Solvent>();
            ProductList = new ObservableCollection<Product>();

            _newsm = new StartingMaterial();
            _newr = new Reagent();
            _news = new Solvent();
            _newp = new Product();

            _selectedsm = new StartingMaterial();
            _selectedr = new Reagent();
            _selecteds = new Solvent();


            users = new List<string>();
            projects = new List<string>();
            reactioncodes = new List<string>();

            ConfigLogicCommands();
            ConfigNavigationCommands();

            Users = new ObservableCollection<string>();
            Projects = new ObservableCollection<string>();
            ReactionCodes = new ObservableCollection<string>();
        }

        private void ConfigLogicCommands()
        {
            SelectReactionImg = new RelayCommand(SelectReactionImgCommand);
            SelectObservationImg = new RelayCommand(SelectObservationImgCommand);
            SelectSaveLocation = new RelayCommand(SelectSaveLocationCommand);
            SaveReaction = new RelayCommand(SaveReactionCommandAsync);
            DeleteObservationImg = new RelayCommand<string>(DeleteObservationImgCommand);

            AddMaterialAsStartingMaterial = new RelayCommand(AddMaterialAsStartingMaterialCommand);
            AddStartingMaterial = new RelayCommand(AddStartingMaterialCommand);
            DeleteStartingMaterial = new RelayCommand<StartingMaterial>(DeleteSM);
            EditMaterialAsStartingMaterial = new RelayCommand(EditMaterialAsStartingMaterialCommand);
            EditStartingMaterial = new RelayCommand<StartingMaterial>(EditSM);
            AddEditedSM = new RelayCommand(AddEditedSMCommand);

            AddMaterialAsReagent = new RelayCommand(AddMaterialAsReagentCommand);
            AddReagent = new RelayCommand(AddReagentCommand);
            DeleteReagent = new RelayCommand<Reagent>(DeleteR);
            EditMaterialAsReagent = new RelayCommand(EditMaterialAsReagentCommand);
            EditReagent = new RelayCommand<Reagent>(EditR);
            AddEditedR = new RelayCommand(AddEditedRCommand);

            AddMaterialAsSolvent = new RelayCommand(AddMaterialAsSolventCommand);
            AddSolvent = new RelayCommand(AddSolventCommand);
            DeleteSolvent = new RelayCommand<Solvent>(DeleteS);
            EditMaterialAsSolvent = new RelayCommand(EditMaterialAsSolventCommand);
            EditSolvent = new RelayCommand<Solvent>(EditS);
            AddEditedS = new RelayCommand(AddEditedSCommand);

            AddProduct = new RelayCommand(AddProductCommand);
            DeleteProduct = new RelayCommand<Product>(DeleteP);
            EditProduct = new RelayCommand<Product>(EditP);
            AddEditedP = new RelayCommand(AddEditedPCommand);

            
        }
        
        #region Commands - Logic
        #region Commands - Logic - Reaction
        public RelayCommand SelectReactionImg { get; private set; }
        private void SelectReactionImgCommand()
        {
            var resu = _openFileDialogService.ShowOpenFileDialog();
            ReactionImgPath = resu;
            OutputMessage tmp = new OutputMessage { Message = ReactionImgPath + " added as Reaction Image", Level = "" };
            OutputMessages.Add(tmp);
            _logService.Write(this, tmp.Message, tmp.Level);
        }
        public RelayCommand SelectObservationImg { get; private set; }
        private void SelectObservationImgCommand()
        {
            var resu = _openFileDialogService.ShowOpenFileDialog();
            ObservationImgPath = resu;
            if (resu != null)
            {
                ObservationImgPaths.Add(resu);
                OutputMessages.Add(new OutputMessage { Message = resu + " added as Observation Image", Level = "" });
            }


            else
            {
                OutputMessages.Add(new OutputMessage { Message = "No image added as Observation Image", Level = "" });
                //_loggerService.Write(this, tmp.Message, tmp.Level);
            }

        }
        public RelayCommand<string> DeleteObservationImg { get; private set; }
        private void DeleteObservationImgCommand(string deleteThis)
        {
            OutputMessages.Add(new OutputMessage { Message = "Observation Image " + deleteThis + " removed!", Level = "" });
            ObservationImgPaths.Remove(deleteThis);
            
        }
        public RelayCommand SelectSaveLocation { get; private set; }
        private void SelectSaveLocationCommand()
        {
            var resu = _openFileDialogService.ShowSaveFileDialog(".docx", "Docx Files (*.docx)| All files (*.*)");
            SaveLocation = resu;
            OutputMessage tmp = new OutputMessage { Message = SaveLocation + " added as Save Location", Level = "" };
            OutputMessages.Add(tmp);
            _logService.Write(this, tmp.Message, tmp.Level);
            
        }

        
        public RelayCommand SaveReaction { get; private set; }
        private async void SaveReactionCommandAsync()
        {
            _ReactionInfo.StartingMaterial = StartingMaterialList.FirstOrDefault();
            _ReactionInfo.Reagents = ReagentList.ToList();
            _ReactionInfo.Solvents = SolventList.ToList();
            _ReactionInfo.Products = ProductList.ToList();
            _ReactionInfo.ObservationImgPaths = ObservationImgPaths.ToList();
            if (ValidateReactionData())
            {
                SaveSuccessful = "";
                UploadVisibility = "visible";
                SaveVisibility = "visible";
                _docxGeneratorService.GenerateSingleReaction(_ReactionInfo);
                OutputMessages.Add(new OutputMessage { Message = "Document saved!", Level = "" });
                SaveSuccessful = "Saved! ";
                
                
                
                await _dbService.AddReaction(_ReactionInfo);
                UploadVisibility = null;
                SaveSuccessful += "Saved to database!";
                OutputMessages.Add(new OutputMessage { Message = "Saved to database!", Level = "" });
                if (DropboxUpload)
                {
                    string dropboxresult;
                    try
                    {
                        dropboxresult = await _docxGeneratorService.UploadToDropboxAsync(_ReactionInfo.SaveLocation, _ReactionInfo.Project, _ReactionInfo.Code + ".docx");
                        OutputMessages.Add(new OutputMessage { Message = dropboxresult, Level = "" });
                    }
                    catch
                    {
                        dropboxresult = "Cannot upload to Dropbox! Check internet connection! ";
                        OutputMessages.Add(new OutputMessage { Message = dropboxresult, Level = "info" });
                    }
                    SaveSuccessful += dropboxresult;
                }
            }
           
        }
        #endregion
        #region Commands - Logic - StartingMaterial
        public RelayCommand AddMaterialAsStartingMaterial { get; private set; }
        private void AddMaterialAsStartingMaterialCommand()
        {
            SelectedMolecule tmp = _selectMoleculeDialogService.ShowMoleculeSelectWindow();
            if (tmp != null)
            {
                SMName = tmp.Name;
                SMCAS = tmp.CAS;
                SMLocation = tmp.Location;
                _newsm = new StartingMaterial(tmp);
                if (tmp.mAvailable.HasValue)
                {
                    SMValueFound = "m value found for this material in database";
                }
                else
                {
                    if(tmp.VAvailable.HasValue)
                    {
                        SMValueFound = "V value found for this material in database";
                    }
                    else
                    {
                        //nincs se m se V db-be ehhez a molekulához
                        SMValueFound = "Nothing found for this material in database";
                    }
                }
            }
            else
            {
                SMName = "";
                SMCAS = "";
                SMLocation = "";
                SMValueFound = "";
            }
        }
        public RelayCommand AddStartingMaterial { get; private set; }
        private void AddStartingMaterialCommand()
        {
            if (StartingMaterialList.Count == 0)
            {
                
                if (ValidateNewStartingMaterial())
                {
                    StartingMaterialList.Add(_newsm);
                    OutputMessages.Add(new OutputMessage { Message = _newsm.Name + " added as Starting Material", Level = "" });
                }
            }
            else
            {
                OutputMessages.Add(new OutputMessage { Message = "Only 1 starting material can be added!", Level = "error" });
                
            }
        }
        public RelayCommand<StartingMaterial> DeleteStartingMaterial { get; private set; }
        private void DeleteSM(StartingMaterial deleteThis)
        {
            OutputMessages.Add(new OutputMessage { Message ="Starting Material "+ deleteThis.Name+" removed!", Level = "info" });
            StartingMaterialList.Remove(deleteThis);
            SMNameEdit = "";
            SMCASEdit = "";
            SMLocationEdit = "";
            SMVvalueEdit = "";
            SMmvalueEdit = "";
        }
        public RelayCommand EditMaterialAsStartingMaterial { get; private set; }
        private void EditMaterialAsStartingMaterialCommand()
        {
            SelectedMolecule tmp = _selectMoleculeDialogService.ShowMoleculeSelectWindow();
            if (tmp != null)
            {
                SMNameEdit = tmp.Name;
                SMCASEdit = tmp.CAS;
                SMLocationEdit = tmp.Location;
                _editedsm = new StartingMaterial(tmp);
                if (tmp.mAvailable.HasValue)
                {
                    SMValueFoundEdit = "m value found for this material in database";
                }
                else
                {
                    if (tmp.VAvailable.HasValue)
                    {
                        SMValueFoundEdit = "V value found for this material in database";
                    }
                    else
                    {
                        //nincs se m se V db-be ehhez a molekulához
                        SMValueFoundEdit = "Nothing found for this material in database";
                    }
                }
            }
        }
        public RelayCommand<StartingMaterial> EditStartingMaterial { get; set; }
        private void EditSM(StartingMaterial editThis)
        {
            _selectedsm = editThis;
            _editedsm = _selectedsm;
            EditSMVisibility = "edit";
            SMNameEdit = editThis.Name;
            SMCASEdit = editThis.CAS;
            SMLocationEdit = editThis.Location;
            SMVvalueEdit = editThis.VValueString;
            SMmvalueEdit = editThis.mValueString;
        }
        public RelayCommand AddEditedSM { get; private set; }
        private void AddEditedSMCommand()
        {
            if (_selectedsm != null)
                if (ValidateEditedStartingMaterial())
                {
                    OutputMessages.Add(new OutputMessage { Message = "Starting Material " + _selectedsm.Name + " with "+_selectedsm.mValueString+" m value and "+_selectedsm.VValueString+" V value edited to " + _editedsm.Name + " with " + _editedsm.mValueString + " m value and " + _editedsm.VValueString + " V value!", Level = "info" });
                    StartingMaterialList.Remove(_selectedsm);
                    StartingMaterialList.Add(_editedsm);
                    _selectedsm = null;
                    EditSMVisibility = null;
                    SMNameEdit = "";
                    SMCASEdit = "";
                    SMLocationEdit = "";
                    SMVvalueEdit = "";
                    SMmvalueEdit = "";
                }
        }
        #endregion
        #region Commands - Logic - Reagent
        public RelayCommand AddMaterialAsReagent { get; private set; }
        private void AddMaterialAsReagentCommand()
        {
            SelectedMolecule tmp = _selectMoleculeDialogService.ShowMoleculeSelectWindow();
            if (tmp != null)
            {
                RName = tmp.Name;
                RCAS = tmp.CAS;
                RLocation = tmp.Location;
                _newr = new Reagent(tmp);
            }
            else
            {
                RName = "";
                RCAS = "";
                RLocation = "";
            }
        }
        public RelayCommand AddReagent { get; private set; }
        private void AddReagentCommand()
        {

            if (ValidateNewReagent())
            {
                OutputMessages.Add(new OutputMessage { Message = _newr.Name + " added as Reagent", Level = "" });
                ReagentList.Add(_newr);
            }

        }
        public RelayCommand<Reagent> DeleteReagent { get; private set; }
        private void DeleteR(Reagent deleteThis)
        {
            OutputMessages.Add(new OutputMessage { Message = "Reagent " + deleteThis.Name + " removed!", Level = "info" });
            ReagentList.Remove(deleteThis);
            RNameEdit = "";
            RCASEdit = "";
            RLocationEdit = "";
            RRatioEdit = "";

        }
        public RelayCommand EditMaterialAsReagent { get; private set; }
        private void EditMaterialAsReagentCommand()
        {
            SelectedMolecule tmp = _selectMoleculeDialogService.ShowMoleculeSelectWindow();
            if (tmp != null)
            {
                RNameEdit = tmp.Name;
                RCASEdit = tmp.CAS;
                RLocationEdit = tmp.Location;
                _editedr = new Reagent(tmp);
            }
        }
        public RelayCommand<Reagent> EditReagent { get; private set; }
        private void EditR(Reagent editThis)
        {
            _selectedr = editThis;
            EditRVisibility = "edit";
            RNameEdit = editThis.Name;
            RCASEdit = editThis.CAS;
            RLocationEdit = editThis.Location;
            RRatioEdit = editThis.RatioString;
        }
        public RelayCommand AddEditedR { get; private set; }
        private void AddEditedRCommand()
        {
            if (_selectedr != null)
                if (ValidateEditedReagent())
                {
                    OutputMessages.Add(new OutputMessage { Message = "Reagent " + _selectedr.Name + " with " + _selectedr.RatioString + " ratio edited to " + _editedr.Name + " with " + _editedr.RatioString + " ratio!", Level = "info" });
                    ReagentList.Remove(_selectedr);
                    ReagentList.Add(_editedr);
                    _selectedr = null;
                    EditRVisibility = null;
                    RNameEdit = "";
                    RCASEdit = "";
                    RLocationEdit = "";
                    RRatioEdit = "";
                }
        }
        #endregion
        #region Commands - Logic - Solvent
        public RelayCommand AddMaterialAsSolvent { get; private set; }
        private void AddMaterialAsSolventCommand()
        {
            SelectedMolecule tmp = _selectMoleculeDialogService.ShowMoleculeSelectWindow();
            if (tmp != null)
            {
                SName = tmp.Name;
                SCAS = tmp.CAS;
                SLocation = tmp.Location;

                _news = new Solvent(tmp);
            }
            else
            {
                SName = "";
                SCAS = "";
                SLocation = "";
            }
        }
        public RelayCommand AddSolvent { get; private set; }
        private void AddSolventCommand()
        {

            if (ValidateNewSolvent())
            {
                OutputMessages.Add(new OutputMessage { Message = _news.Name + " added as Solvent", Level = "" });
                SolventList.Add(_news);
            }

        }
        public RelayCommand<Solvent> DeleteSolvent { get; private set; }
        private void DeleteS(Solvent deleteThis)
        {
            OutputMessages.Add(new OutputMessage { Message = "Solvent " + deleteThis.Name + " removed!", Level = "info" });
            SolventList.Remove(deleteThis);
            SNameEdit = "";
            SCASEdit = "";
            SLocationEdit = "";
            SVvalueEdit = "";

        }
        public RelayCommand EditMaterialAsSolvent { get; private set; }
        private void EditMaterialAsSolventCommand()
        {
            SelectedMolecule tmp = _selectMoleculeDialogService.ShowMoleculeSelectWindow();
            if (tmp != null)
            {
                SNameEdit = tmp.Name;
                SCASEdit = tmp.CAS;
                SLocationEdit = tmp.Location;
                _editeds = new Solvent(tmp);
            }
        }
        public RelayCommand<Solvent> EditSolvent { get; set; }
        private void EditS(Solvent editThis)
        {
            EditSVisibility = "edit";
            _selecteds = editThis;
            SNameEdit = editThis.Name;
            SCASEdit = editThis.CAS;
            SLocationEdit = editThis.Location;
            SVvalueEdit = editThis.VValueString;

        }
        public RelayCommand AddEditedS { get; private set; }
        private void AddEditedSCommand()
        {
            if (_selecteds != null)
                if (ValidateEditedSolvent())
                {
                    OutputMessages.Add(new OutputMessage { Message = "Solvent " + _selecteds.Name + " with " + _selecteds.VValueString + " V value edited to " + _editeds.Name + " with " + _editeds.VValueString + " V value!", Level = "info" });
                    SolventList.Remove(_selecteds);
                    SolventList.Add(_editeds);
                    _selecteds = null;
                    EditSVisibility = null;
                    SNameEdit = "";
                    SCASEdit = "";
                    SLocationEdit = "";
                    SVvalueEdit = "";

                }
        }
        #endregion
        #region Commands - Logic - Product
        public RelayCommand AddProduct { get; private set; }
        private void AddProductCommand()
        {

            if (ValidateNewProduct())
            {
                OutputMessages.Add(new OutputMessage { Message ="Product added with "+ _newp.MWString + " MW value and "+_newp.RatioString+" ratio!", Level = "" });
                ProductList.Add(_newp);

            }
        }
        public RelayCommand<Product> DeleteProduct { get; private set; }
        private void DeleteP(Product deleteThis)
        {
            ProductList.Remove(deleteThis);
            OutputMessages.Add(new OutputMessage { Message = "Product removed with " + deleteThis.MWString + " MW value and " + deleteThis.RatioString + " ratio!", Level = "" });
            PRatioEdit = "";
            PMWvalueEdit = "";
        }

        public RelayCommand<Product> EditProduct { get; set; }
        private void EditP(Product editThis)
        {
            _selectedp = editThis;
            EditPVisibility = "edit";
            PRatioEdit = editThis.MWString;
            PMWvalueEdit = editThis.RatioString;
        }
        public RelayCommand AddEditedP { get; private set; }
        private void AddEditedPCommand()
        {
            if (_selectedp != null)
                if (ValidateEditedProduct())
                {
                    OutputMessages.Add(new OutputMessage { Message = "Product with " + _selectedp.MWString + " MW value and " + _selectedp.RatioString + " ratio edited to product with " + _editedp.MWString + "MW value and " + _editedp.RatioString + " ratio!", Level = "info" });
                    ProductList.Remove(_selectedp);
                    ProductList.Add(_editedp);
                    _selectedp = null;
                    EditPVisibility = null;
                    PRatioEdit = "";
                    PMWvalueEdit = "";
                }
        }
        #endregion

        #endregion

        public ObservableCollection<string> Users { get; set; }
        public ObservableCollection<string> Projects { get; set; }
        public ObservableCollection<string> ReactionCodes { get; set; }
        private string _chemistselected;
        public string ChemistSelected { get { return _chemistselected; } set { Set(ref _chemistselected, value); _ReactionInfo.Chemist = value; } }

        private string _chiefchemistselected;
        public string ChiefchemistSelected { get { return _chiefchemistselected; } set { Set(ref _chiefchemistselected, value); _ReactionInfo.Chiefchemist = value; } }

        private string _projectselected;
        public string ProjectSelected { get { return _projectselected; } set { Set(ref _projectselected, value); _ReactionInfo.Project = value; } }

        private string _previousstepselected;
        public string PreviousstepSelected { get { return _previousstepselected; } set { Set(ref _previousstepselected, value); _ReactionInfo.PreviousStep = value; } }


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

            GetResources = new RelayCommand(GetResourcesCommand);
        }

        #region Commands - Navigation
        private string _currentuser;
        public string CurrentUser { get { return _currentuser; } set { Set(ref _currentuser, value); } }
        public RelayCommand GetResources { get; private set; }
        private async void GetResourcesCommand()
        {
            users = await _dbService.GetUsersAsync();
            projects = await _dbService.GetProjectNamesAsync();
            reactioncodes = await _dbService.GetReactionCodesAsync();
            _selectMoleculeDialogService.ConfigureAsync();
            
            foreach (var item in users)
            {
                Users.Add(item);
            }
            foreach (var item in projects)
            {
                Projects.Add(item);
            }
            //nincs prev.step.
            ReactionCodes.Add("-");
            foreach (var item in reactioncodes)
            {
                ReactionCodes.Add(item);
            }
        }
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
        private string _reactionimgpath;
        private string _procedure;
        private string _yield;
        private string _observation;
        private bool _issketch;
        private string _observationimgpath;

        public string Code { get { return _code; } set { Set(ref _code, value); _ReactionInfo.Code = _code; } }
        public string Chemist { get { return _chemist; } set { Set(ref _chemist, value); _ReactionInfo.Chemist = _chemist; } }
        public string Chiefchemist { get { return _chiefchemist; } set { Set(ref _chiefchemist, value); _ReactionInfo.Chiefchemist = _chiefchemist; } }
        public string Project { get { return _project; } set { Set(ref _project, value); _ReactionInfo.Project = _project; } }
        public string Laboratory { get { return _laboratory; } set { Set(ref _laboratory, value); _ReactionInfo.Laboratory = _laboratory; } }
        public DateTime StartDate { get { return _startdate; } set { Set(ref _startdate, value); _ReactionInfo.StartDate = _startdate; } }
        public DateTime ClosureDate { get { return _closuredate; } set { Set(ref _closuredate, value); _ReactionInfo.ClosureDate = _closuredate; } }
        public string PreviousStep { get { return _previousstep; } set { Set(ref _previousstep, value); _ReactionInfo.PreviousStep = _previousstep; } }
        public string Literature { get { return _literature; } set { Set(ref _literature, value); _ReactionInfo.Literature = _literature; } }
        public string Procedure { get { return _procedure; } set { Set(ref _procedure, value); _ReactionInfo.Procedure = _procedure; } }
        public string Yield { get { return _yield; } set { Set(ref _yield, value); _ReactionInfo.Yield = _yield; } }
        public string Observation { get { return _observation; } set { Set(ref _observation, value); _ReactionInfo.Observation = _observation; } }
        public string ReactionImgPath { get { return _reactionimgpath; } set { Set(ref _reactionimgpath, value); _ReactionInfo.ReactionImgPath = _reactionimgpath; } }
        public bool IsSketch { get { return _issketch; } set { Set(ref _issketch, value); _ReactionInfo.IsSketch = _issketch; } }
        public ObservableCollection<string> ObservationImgPaths { get; set; }
        public string ObservationImgPath { get { return _observationimgpath; } set { Set(ref _observationimgpath, value); } }


        private string _savelocation;
        public string SaveLocation { get { return _savelocation; } set { Set(ref _savelocation, value); _ReactionInfo.SaveLocation = _savelocation; } }
        #endregion

        #region Bindings - StartingMaterial
        private string _smname;
        private string _smcas;
        private string _smlocation;
        private string _smvvalue;
        private string _smmvalue;
        private StartingMaterial _selectedsm;
        private string _smnameedit;
        private string _smcasedit;
        private string _smlocationedit;
        private string _smvvalueedit;
        private string _smmvalueedit;

        public string SMName { get { return _smname; } set { Set(ref _smname, value); } }
        public string SMCAS { get { return _smcas; } set { Set(ref _smcas, value); } }
        public string SMLocation { get { return _smlocation; } set { Set(ref _smlocation, value); } }
        public string SMVvalue { get { return _smvvalue; } set { Set(ref _smvvalue, value); } }
        public string SMmvalue { get { return _smmvalue; } set { Set(ref _smmvalue, value); } }

        private StartingMaterial _newsm;
        private StartingMaterial _editedsm;
        public ObservableCollection<StartingMaterial> StartingMaterialList { get; set; }

        public string SMNameEdit { get { return _smnameedit; } set { Set(ref _smnameedit, value); } }
        public string SMCASEdit { get { return _smcasedit; } set { Set(ref _smcasedit, value); } }
        public string SMLocationEdit { get { return _smlocationedit; } set { Set(ref _smlocationedit, value); } }
        public string SMVvalueEdit { get { return _smvvalueedit; } set { Set(ref _smvvalueedit, value); } }
        public string SMmvalueEdit { get { return _smmvalueedit; } set { Set(ref _smmvalueedit, value); } }

        private string _editsmvisibility;
        public string EditSMVisibility { get { return _editsmvisibility; } set { Set(ref _editsmvisibility, value); } }

        private string _smvaluefound;
        public string SMValueFound { get { return _smvaluefound; } set { Set(ref _smvaluefound, value); } }

        private string _smvaluefoundedit;
        public string SMValueFoundEdit { get { return _smvaluefoundedit; } set { Set(ref _smvaluefoundedit, value); } }
        #endregion

        #region Bindings - Reagent
        private string _rname;
        private string _rcas;
        private string _rlocation;
        private string _rratio;

        private Reagent _selectedr;
        private string _rnameedit;
        private string _rcasedit;
        private string _rlocationedit;
        private string _rratioedit;

        public string RName { get { return _rname; } set { Set(ref _rname, value); } }
        public string RCAS { get { return _rcas; } set { Set(ref _rcas, value); } }
        public string RLocation { get { return _rlocation; } set { Set(ref _rlocation, value); } }
        public string RRatio { get { return _rratio; } set { Set(ref _rratio, value); } }

        private Reagent _newr;
        private Reagent _editedr;
        public ObservableCollection<Reagent> ReagentList { get; set; }

        public string RNameEdit { get { return _rnameedit; } set { Set(ref _rnameedit, value); } }
        public string RCASEdit { get { return _rcasedit; } set { Set(ref _rcasedit, value); } }
        public string RLocationEdit { get { return _rlocationedit; } set { Set(ref _rlocationedit, value); } }
        public string RRatioEdit { get { return _rratioedit; } set { Set(ref _rratioedit, value); } }

        private string _editrvisibility;
        public string EditRVisibility { get { return _editrvisibility; } set { Set(ref _editrvisibility, value); } }
        #endregion

        #region Bindings - Solvent
        private string _sname;
        private string _scas;
        private string _slocation;
        private string _svvalue;

        private Solvent _selecteds;
        private string _snameedit;
        private string _scasedit;
        private string _slocationedit;
        private string _svvalueedit;

        public string SName { get { return _sname; } set { Set(ref _sname, value); } }
        public string SCAS { get { return _scas; } set { Set(ref _scas, value); } }
        public string SLocation { get { return _slocation; } set { Set(ref _slocation, value); } }
        public string SVvalue { get { return _svvalue; } set { Set(ref _svvalue, value); } }

        private Solvent _news;
        private Solvent _editeds;
        public ObservableCollection<Solvent> SolventList { get; set; }

        public string SNameEdit { get { return _snameedit; } set { Set(ref _snameedit, value); } }
        public string SCASEdit { get { return _scasedit; } set { Set(ref _scasedit, value); } }
        public string SLocationEdit { get { return _slocationedit; } set { Set(ref _slocationedit, value); } }
        public string SVvalueEdit { get { return _svvalueedit; } set { Set(ref _svvalueedit, value); } }

        private string _editsvisibility;
        public string EditSVisibility { get { return _editsvisibility; } set { Set(ref _editsvisibility, value); } }
        #endregion

        #region Bindings - Product

        private string _pratio;
        private string _pmwvalue;
        private Product _selectedp;

        private string _pratioedit;
        private string _pmwvalueedit;


        public string PRatio { get { return _pratio; } set { Set(ref _pratio, value); } }
        public string PMWvalue { get { return _pmwvalue; } set { Set(ref _pmwvalue, value); } }

        private Product _newp;
        private Product _editedp;
        public ObservableCollection<Product> ProductList { get; set; }


        public string PRatioEdit { get { return _pratioedit; } set { Set(ref _pratioedit, value); } }
        public string PMWvalueEdit { get { return _pmwvalueedit; } set { Set(ref _pmwvalueedit, value); } }

        private string _editpvisibility;
        public string EditPVisibility { get { return _editpvisibility; } set { Set(ref _editpvisibility, value); } }
        #endregion

        #region Bindings - SaveResult
        private bool _dropboxupload;
        public bool DropboxUpload { get { return _dropboxupload; } set { Set(ref _dropboxupload, value); } }

        private string _savesuccessful;
        
        private string _savevisibility;
        private string _uploadvisibility;

        public string SaveSuccessful { get { return _savesuccessful; } set { Set(ref _savesuccessful, value); } }
       
        public string SaveVisibility { get { return _savevisibility; } set { Set(ref _savevisibility, value); } }
        public string UploadVisibility { get { return _uploadvisibility; } set { Set(ref _uploadvisibility, value); } }
        #endregion

        #region Models
        private ReactionInfo _ReactionInfo;

        private List<string> users;
        private List<string> projects;
        private List<string> reactioncodes;

        private bool ValidateReactionData()
        {
            bool result = true;
            OutputMessages.Add(new OutputMessage { Message = "Validate reaction inputs", Level = "" });
            List<OutputMessage> tmp = new List<OutputMessage>();
            tmp = _ReactionInfo.Validate(users, projects,reactioncodes);
            foreach (var item in tmp)
            {
                if (item.Level == "error") result = false;
                OutputMessages.Add(item);
                _logService.Write(this, item.Message, item.Level);
            }
            if (result)
            {
                try
                {

                    _ReactionInfo.StartingMaterial.CalculateValues();

                    foreach (var item in ProductList)
                    {
                        item.CalculateValues(_ReactionInfo.StartingMaterial);
                    }
                    foreach (var item in ReagentList)
                    {
                        item.CalculateValues(_ReactionInfo.StartingMaterial);
                    }
                }
                catch (Exception e)
                {
                    OutputMessages.Add(new OutputMessage { Message = e.Message, Level = "error" });
                    result = false;
                }

            }
            return result;
        }
        private bool ValidateNewStartingMaterial()
        {
            bool result = true;
            //_newsm = new StartingMaterial();
            _newsm.Name = SMName;
            _newsm.CAS = SMCAS;
            _newsm.Location = SMLocation;
            _newsm.mValueString = SMmvalue;
            _newsm.VValueString = SMVvalue;

            OutputMessages.Add(new OutputMessage { Message = "Validate starting material inputs", Level = "" });
            List<OutputMessage> tmp = new List<OutputMessage>();
            tmp  = _newsm.Validate();

            foreach (var item in tmp)
            {
                if (item.Level == "error") result = false;
                OutputMessages.Add(item);
                _logService.Write(this, item.Message, item.Level);
            }
            return result;
        }
        private bool ValidateEditedStartingMaterial()
        {
            bool result = true;
            //_editedsm = _selectedsm;
            _editedsm.Name = SMNameEdit;
            _editedsm.CAS = SMCASEdit;
            _editedsm.Location = SMLocationEdit;
            _editedsm.mValueString = SMmvalueEdit;
            _editedsm.VValueString = SMVvalueEdit;

            OutputMessages.Add(new OutputMessage { Message = "Validate edited starting material inputs", Level = "" });
            List<OutputMessage> tmp = new List<OutputMessage>();
            tmp = _editedsm.Validate();

            foreach (var item in tmp)
            {
                if (item.Level == "error") result = false;
                OutputMessages.Add(item);
                _logService.Write(this, item.Message, item.Level);
            }
            return result;

            
        }
        
        private bool ValidateNewReagent()
        {
            bool result = true;
            //_newr = new Reagent();
            _newr.Name = RName;
            _newr.CAS = RCAS;
            _newr.Location = RLocation;
            _newr.RatioString = RRatio;


            OutputMessages.Add(new OutputMessage { Message = "Validate reagent inputs", Level = "" });
            List<OutputMessage> tmp = new List<OutputMessage>();
            tmp = _newr.Validate();

            foreach (var item in tmp)
            {
                if (item.Level == "error") result = false;
                OutputMessages.Add(item);
                _logService.Write(this, item.Message, item.Level);
            }
            return result;
        }
        private bool ValidateEditedReagent()
        {
            bool result = true;
            _editedr = new Reagent();
            _editedr.Name = RNameEdit;
            _editedr.CAS = RCASEdit;
            _editedr.Location = RLocationEdit;
            _editedr.RatioString = RRatioEdit;


            OutputMessages.Add(new OutputMessage { Message = "Validate edited reagent inputs", Level = "" });
            List<OutputMessage> tmp = new List<OutputMessage>();
            tmp = _editedr.Validate();

            foreach (var item in tmp)
            {
                if (item.Level == "error") result = false;
                OutputMessages.Add(item);
                _logService.Write(this, item.Message, item.Level);
            }
            return result;
        }
        private bool ValidateNewSolvent()
        {
            bool result = true;
            //_news = new Solvent();
            _news.Name = SName;
            _news.CAS = SCAS;
            _news.Location = SLocation;
            _news.VValueString = SVvalue;


            OutputMessages.Add(new OutputMessage { Message = "Validate solvent inputs", Level = "" });
            List<OutputMessage> tmp = new List<OutputMessage>();
            tmp = _news.Validate();

            foreach (var item in tmp)
            {
                if (item.Level == "error") result = false;
                OutputMessages.Add(item);
                _logService.Write(this, item.Message, item.Level);
            }
            return result;
        }
        private bool ValidateEditedSolvent()
        {
            bool result = true;
            _editeds = new Solvent();
            _editeds.Name = SNameEdit;
            _editeds.CAS = SCASEdit;
            _editeds.Location = SLocationEdit;
            _editeds.VValueString = SVvalueEdit;


            OutputMessages.Add(new OutputMessage { Message = "Validate edited solvent inputs", Level = "" });
            List<OutputMessage> tmp = new List<OutputMessage>();
            tmp = _editeds.Validate();

            foreach (var item in tmp)
            {
                if (item.Level == "error") result = false;
                OutputMessages.Add(item);
                _logService.Write(this, item.Message, item.Level);
            }
            return result;
        }
        private bool ValidateNewProduct()
        {
            bool result = true;
            _newp = new Product();
            
            _newp.MWString = PMWvalue;
            _newp.RatioString = PRatio;

            OutputMessages.Add(new OutputMessage { Message = "Validate product inputs", Level = "" });
            List<OutputMessage> tmp = new List<OutputMessage>();
            tmp = _newp.Validate();

            foreach (var item in tmp)
            {
                if (item.Level == "error") result = false;
                OutputMessages.Add(item);
                _logService.Write(this, item.Message, item.Level);
            }
            return result;
        }
        private bool ValidateEditedProduct()
        {
            bool result = true;
            _editedp = new Product();

            _editedp.MWString = PMWvalueEdit;
            _editedp.RatioString = PRatioEdit;

            OutputMessages.Add(new OutputMessage { Message = "Validate edited product inputs", Level = "" });
            List<OutputMessage> tmp = new List<OutputMessage>();
            tmp = _editedp.Validate();

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
