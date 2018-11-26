
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
            Users = new ObservableCollection<string>();
            ConfirmUserChange = new RelayCommand(ConfirmUserChangeCommand);
            GetUsers = new RelayCommand(GetUsersCommand);
            Home = new RelayCommand(HomeCommand);
            AddNewUser = new RelayCommand(AddNewUserCommand);
            AddNewUserVisibilityChange = new RelayCommand(AddNewUserVisibilityChangeCommand);
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
            users = await _dbService.GetUsersAsync();

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
            //MessengerInstance.Send(CurrentUser);
        }

        public RelayCommand Home { get; private set; }
        private void HomeCommand()
        {

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
                NewUserError =NewUser+ " is already registered!";
            }
            else
            {
                _dbService.AddUser(NewUser);
                NewUserError = NewUser+" added!";
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
    }
}
