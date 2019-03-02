
using ChClient.Models;
using ChClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.ViewModels
{
    public class ManageUsersPageViewModel :ViewModelBase
    {
        #region Services
        private IFrameNavigationService _navigationService;
        private ILogger _loggerService;
        private IDBService _dbService;
        #endregion
        public ManageUsersPageViewModel(IFrameNavigationService navigationService, ILogger loggerService, IDBService dbService)
        {
            _navigationService = navigationService;
            _loggerService = loggerService;
            _dbService = dbService;
            
            CurrentUser = _navigationService.Parameter.ToString();
            Config();
        }
        private void Config()
        {
            Users = new ObservableCollection<string>();
            ConfirmUserChange = new RelayCommand(ConfirmUserChangeCommand);
            GetUsers = new RelayCommand(GetUsersCommand);
            Home = new RelayCommand(HomeCommand);
            AddNewUser = new RelayCommand(AddNewUserCommand);
            AddNewUserVisibilityChange = new RelayCommand(AddNewUserVisibilityChangeCommand);

            OutputMessages = new ObservableCollection<OutputMessage>();
        }
        private string _currentuser;
        public string CurrentUser { get { return _currentuser; } set { Set(ref _currentuser, value); } }

        private string _selecteduser;
        public string SelectedUser { get { return _selecteduser; } set { Set(ref _selecteduser, value); } }

        public ObservableCollection<string> Users { get; set; }
        public RelayCommand GetUsers { get; private set; }
        private async void GetUsersCommand()
        {
            AddNewUserVisibility = null;
            List<string> users = new List<string>();
            Users.Clear();
            try
            {
                users = await _dbService.GetUsersAsync();
            }
            catch(Exception e)
            {
                _loggerService.Write(this,e.Message);
                _loggerService.Write(this, e.Source);
                _loggerService.Write(this, e.InnerException.Message);
                _loggerService.Write(this, e.InnerException.Source);
            }
            
            _loggerService.Write(this, users.Count + " user(s) loaded", "debug");
            foreach (var item in users)
            {
                Users.Add(item);
            }
        }

        public RelayCommand ConfirmUserChange { get; private set; }
        private void ConfirmUserChangeCommand()
        {
            CurrentUser = SelectedUser;
            var myMessage = new NotificationMessage(SelectedUser);
            Messenger.Default.Send(myMessage);
            OutputMessages.Add(new OutputMessage { Message = "Current user set to: " + CurrentUser, Level = "debug" });
            _loggerService.Write(this, "Current user set to: " + CurrentUser, "debug");
        }

        public RelayCommand Home { get; private set; }
        private void HomeCommand()
        {
            _loggerService.Write(this, "Navigate to: Home page");
            _navigationService.NavigateTo("Home");
        }

        private string _newuser;
        public string NewUser { get { return _newuser; } set { Set(ref _newuser, value); } }

        private string _newusererror;
        public string NewUserError { get { return _newusererror; } set { Set(ref _newusererror, value); } }

        public RelayCommand AddNewUser { get; private set; }
        private void AddNewUserCommand()
        {
            if (Users.Contains(NewUser))
            {
                OutputMessages.Add(new OutputMessage { Message = NewUser+" is already registered!", Level = "error" });
                _loggerService.Write(this, NewUser + " is already registered!", "error");
            }
            else
            {
                _loggerService.Write(this, "Add started", "debug");
                _dbService.AddUser(NewUser);
                _loggerService.Write(this, NewUser+" added!", "debug");
                OutputMessages.Add(new OutputMessage { Message = NewUser+" added!", Level = "" });
                Users.Add(NewUser);
                
            }
        }
        private string _addnewuservisibility;
        public string AddNewUserVisibility { get { return _addnewuservisibility; } set { Set(ref _addnewuservisibility, value); } }

        public RelayCommand AddNewUserVisibilityChange { get; private set; }
        private void AddNewUserVisibilityChangeCommand()
        {
            AddNewUserVisibility = "visible";
            NewUserError = "";
            NewUser = "";
        }

        public ObservableCollection<OutputMessage> OutputMessages { get; set; }
    }
}
