using ChClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.ViewModels
{
    public class HomePageViewModel :ViewModelBase
    {
        private IFrameNavigationService _navigationService;
        
        private RelayCommand _newReactionCommand;
        public RelayCommand NewReactionCommand {
            get {
                return _newReactionCommand
                    ?? (_newReactionCommand = new RelayCommand(
                    () =>
                    {
                        _navigationService.NavigateTo("NewReaction");
                    }));
            }
        }
        public HomePageViewModel(IFrameNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

    }
}
