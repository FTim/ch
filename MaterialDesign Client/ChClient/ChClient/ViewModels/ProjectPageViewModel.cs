using ChClient.Models;
using ChClient.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ILogger _loggerService;
        private IDBService _dbService;
        public ProjectPageViewModel(IFrameNavigationService navigationService, IOpenFileDialogService openFileDialogService, ILogger loggerService, IDBService dBService)
        {
            _navigationService = navigationService;
            _openFileDialogService = openFileDialogService;
            _loggerService = loggerService;
            _dbService = dBService;

            _project = (Project)_navigationService.Parameter;

            ProjectName = _project.Name;
            Leader = _project.Leader;
            Goal = _project.Goal;
            Description = _project.Description;
            Plans = new ObservableCollection<byte[]>();
            foreach (var item in _project.ProjectPlanByreArrays)
            {
                Plans.Add(item);
            }
            //Plans = _project.ProjectPlanByreArrays;
    }

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
        public string PlanImgPath { get { return _planimgpath; } set { Set(ref _planimgpath, value);  } }

        public ObservableCollection<byte[]> Plans { get; set; }
        #endregion

    }
}
