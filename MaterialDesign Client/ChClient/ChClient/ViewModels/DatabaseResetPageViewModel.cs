﻿using ChClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.ViewModels
{
    public class DatabaseResetPageViewModel :ViewModelBase
    {
        private IFrameNavigationService _navigationService;
        private ILogger _logService;
        private IDBService _dbService;
        public DatabaseResetPageViewModel(IFrameNavigationService navigationService, ILogger logService, IDBService dBService)
        {
            _navigationService = navigationService;
            _logService = logService;

            _dbService = dBService;
            ResetDb = new RelayCommand(ResetDbCommand);
            Question = new RelayCommand(QuestionCommand);
            Home = new RelayCommand(HomeCommand);
            QuestionEnable = "enable";
        }
        private string _state;
        public string State { get { return _state; } set { Set(ref _state, value); } }
        private string _questionenable;
        public string QuestionEnable { get { return _questionenable; } set { Set(ref _questionenable, value); } }
        #region Bindings - Visibility
        private string _questionvisibility;
        public string QuestionVisibility { get { return _questionvisibility; } set { Set(ref _questionvisibility, value); } }

        private string _inprogressvisibility;
        public string InProgressVisibility { get { return _inprogressvisibility; } set { Set(ref _inprogressvisibility, value); } }

        private string _donevisibility;
        public string DoneVisibility { get { return _donevisibility; } set { Set(ref _donevisibility, value); } }

        public RelayCommand ResetDb { get; set; }

        private async void ResetDbCommand()
        {
            QuestionVisibility = null;
            QuestionEnable = null;
            InProgressVisibility = "visible";
            DoneVisibility = null;
            _dbService.ResetAll();
            _logService.Write(this, "Init database started", "debug");
            State = await _dbService.InitMolecules();
            _logService.Write(this, "Init database finished", "debug");
            InProgressVisibility = null;
            DoneVisibility = "visible";
        }
        #endregion


        
        public RelayCommand Home { get; private set; }
        private void HomeCommand()
        {
            _logService.Write(this, "Navigate to: Home page");
            _navigationService.NavigateTo("Home");
        }
        
        public RelayCommand Question { get; set; }
        private void QuestionCommand()
        {
            QuestionVisibility = "visible";
        }


    }
}
