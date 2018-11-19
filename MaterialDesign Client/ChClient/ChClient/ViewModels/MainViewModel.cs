using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using ChClient.Services;

namespace ChClient.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private IFrameNavigationService _navigationService;
        
        public MainViewModel(IFrameNavigationService navigationService)
        {
            _navigationService = navigationService;
            
            Loaded = new RelayCommand(LoadedCommand);
        }

        public RelayCommand Loaded { get; private set; }
        private void LoadedCommand()
        {
            _navigationService.NavigateTo("Home");
        }

        
    }
}