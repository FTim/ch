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
        #region Services
        private IFrameNavigationService _navigationService;
        private IOpenFileDialogService _openFileDialogService;
        private ISelectMoleculeDialogService _selectMoleculeDialogService;

        #endregion

        public NewReactionPageViewModel(IFrameNavigationService navigationService, IOpenFileDialogService openFileDialogService, ISelectMoleculeDialogService selectMoleculeDialogService)
        {
            _navigationService = navigationService;
            _openFileDialogService = openFileDialogService;
            _selectMoleculeDialogService = selectMoleculeDialogService;

            _ReactionInfo = new ReactionInfo();

            StartDate = DateTime.Now;
            ClosureDate = DateTime.Now;

            IsSketch = false;
        }

        #region Commands
        private RelayCommand _selectReactionImg;
        private RelayCommand _saveReaction;
        private RelayCommand _addMaterialAsStartingMaterial;
        private RelayCommand _addMaterialAsReagent;

        public RelayCommand SelectReactionImg {
            get {
                if (_selectReactionImg == null)
                {
                    _selectReactionImg = new RelayCommand(() =>
                    {
                        var resu = _openFileDialogService.ShowOpenFileDialog();
                        ReactionImgPath = resu;
                    });
                }

                return _selectReactionImg;
            }
        }
        public RelayCommand SaveReaction {
            get {
                if (_saveReaction == null)
                {
                    _saveReaction = new RelayCommand(() =>
                    {
                        ValidateReactionData();
                    });
                }

                return _saveReaction;
            }
        }
        public RelayCommand AddMaterialAsStartingMaterial {
            get {
                    _addMaterialAsStartingMaterial = new RelayCommand(() =>
                      {
                          SelectedMolecule tmp = _selectMoleculeDialogService.ShowMoleculeSelectWindow();
                          if (tmp != null)
                          {
                              SMName = tmp.Name;
                              SMCAS = tmp.CAS;
                              SMLocation = tmp.Location;
                          }
                          else
                          {
                              SMName = "";
                              SMCAS = "";
                              SMLocation = "";
                          }
                      });
                
                return _addMaterialAsStartingMaterial;
            }
        }
        public RelayCommand AddMaterialAsReagent {
            get {
                _addMaterialAsReagent = new RelayCommand(() =>
                {
                    SelectedMolecule tmp = _selectMoleculeDialogService.ShowMoleculeSelectWindow();
                    if (tmp != null)
                    {
                        RName = tmp.Name;
                        RCAS = tmp.CAS;
                        RLocation = tmp.Location;
                    }
                    else
                    {
                        RName = "";
                        RCAS = "";
                        RLocation = "";
                    }
                });

                return _addMaterialAsReagent;
            }
        }


        public RelayCommand _addStartingMaterial;
        public RelayCommand AddStartingMaterial {
            get {
                if (_addStartingMaterial == null)
                {
                    _addStartingMaterial = new RelayCommand(() =>
                    {
                        
                    });
                }

                return _addStartingMaterial;
            }
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


        public string Code {
            get {
                return _code;
            }
            set {
                Set(ref _code, value);
                _ReactionInfo.Code = _code;
            }
        }
        public string Chemist {
            get {
                return _chemist;
            }
            set {

                Set(ref _chemist, value);
                _ReactionInfo.Chemist = _chemist;
            }
        }
        public string Chiefchemist {
            get {
                return _chiefchemist;
            }
            set {

                Set(ref _chiefchemist, value);
                _ReactionInfo.Chiefchemist = _chiefchemist;
            }
        }
        public string Project {
            get {
                return _project;
            }
            set {
                Set(ref _project, value);
                _ReactionInfo.Project = _project;
            }
        }
        public string Laboratory {
            get {
                return _laboratory;
            }
            set {
                Set(ref _laboratory, value);
                _ReactionInfo.Laboratory = _laboratory;
            }
        }
        public DateTime StartDate {
            get {
                return _startdate;
            }
            set {
                Set(ref _startdate, value);
                _ReactionInfo.StartDate = _startdate;
            }
        }
        public DateTime ClosureDate {
            get {
                return _closuredate;
            }
            set {
                Set(ref _closuredate, value);
                _ReactionInfo.ClosureDate = _closuredate;
            }
        }
        public string PreviousStep {
            get {
                return _previousstep;
            }
            set {
                Set(ref _previousstep, value);
                _ReactionInfo.PreviousStep = _previousstep;
            }
        }
        public string Literature {
            get {
                return _literature;
            }
            set {
                Set(ref _literature, value);
                _ReactionInfo.Literature = _literature;
            }
        }
        public string Procedure {
            get {
                return _procedure;
            }
            set {
                Set(ref _procedure, value);
                _ReactionInfo.Procedure = _procedure;
            }
        }
        public string Yield {
            get {
                return _yield;
            }
            set {
                Set(ref _yield, value);
                _ReactionInfo.Yield = _yield;
            }
        }
        public string Observation {
            get {
                return _observation;
            }
            set {
                Set(ref _observation, value);
            }
        }
        public string ReactionImgPath {
            get {
                return _reactionimgpath;
            }
            set {
                Set(ref _reactionimgpath, value);
                _ReactionInfo.ReactionImgPath = _reactionimgpath;
            }
        }
        public bool IsSketch {
            get {
                return _issketch;
            }
            set {
                Set(ref _issketch, value);
                _ReactionInfo.IsSketch = _issketch;
            }
        }

        #endregion

        #region Bindings - StartingMaterial
        private string _smname;
        private string _smcas;
        private string _smlocation;

        public string SMName { get { return _smname; } set { Set(ref _smname, value); } }
        public string SMCAS { get { return _smcas; } set { Set(ref _smcas, value); } }
        public string SMLocation { get { return _smlocation; } set { Set(ref _smlocation, value); } }

        public ObservableCollection<StartingMaterial> StartingMaterialList;

        #endregion

        #region Bindings - Reagent
        private string _rname;
        private string _rcas;
        private string _rlocation;

        public string RName { get { return _rname; } set { Set(ref _rname, value); } }
        public string RCAS { get { return _rcas; } set { Set(ref _rcas, value); } }
        public string RLocation { get { return _rlocation; } set { Set(ref _rlocation, value); } }

        public ObservableCollection<Reagent> ReagentList;
        #endregion


        public ObservableCollection<Solvent> SolventList;
        public ObservableCollection<Product> ProductList;
        #region Bindings - Errors
        private string _reactioncodeerror;
        private string _closuredateerror;
        private string _closuredateignore;
        private string _reactionimageerror;

        public string ReactionCodeError {
            get {
                return _reactioncodeerror;
            }
            set {
                Set(ref _reactioncodeerror, value);
            }
        }
        public string ClosureDateError {
            get { return _closuredateerror; }
            set { Set(ref _closuredateerror, value); }
        }
        public string ClocureDateIgnore {
            get { return _closuredateignore; }
            set { Set(ref _closuredateignore, value); }
        }
        public string ReactionImageError {
            get { return _reactionimageerror; }
            set { Set(ref _reactionimageerror, value); }
        }
        #endregion

        #region Models
        private ReactionInfo _ReactionInfo;

        private void ValidateReactionData()
        {
            ReactionErrorInfo tmpErrorInfo = _ReactionInfo.ValidateHeader();

            ReactionCodeError = tmpErrorInfo.ReactionCodeError;
            ClosureDateError = tmpErrorInfo.ClosureDateError;
            ClocureDateIgnore = tmpErrorInfo.ClosureDateIgnoreNote;
            ReactionImageError = tmpErrorInfo.ReactionImageError;
        }

        private void ValidateStartingMaterial()
        {

        }
        #endregion

    }
}
