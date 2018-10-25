using ChClient.Services;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<HomePageViewModel>();
            SimpleIoc.Default.Register<NewReactionPageViewModel>();
            SimpleIoc.Default.Register<MaterialSelectWindowViewModel>();
            /*SimpleIoc.Default.Register<HomeViewModel>();*/
            SetupNavigation();

           // var openFileDialogService = new OpenFileDialogService();


            SimpleIoc.Default.Register<IOpenFileDialogService, OpenFileDialogService>();

            

            SimpleIoc.Default.Register<ISelectMoleculeDialogService, SelectMoleculeDialogService>();
            SimpleIoc.Default.Register<IExcelReaderService, ExcelReaderService>();

        }

        private static void SetupNavigation()
        {
            var navigationService = new FrameNavigationService();
            navigationService.Configure("NewReaction", new Uri("../Views/NewReactionPage.xaml", UriKind.Relative));
            navigationService.Configure("Home", new Uri("../Views/HomePage.xaml", UriKind.Relative));
            //navigationService.Configure("MaterialSelect", new Uri("../Views/MaterialSelectWindow.xaml", UriKind.Relative));
            SimpleIoc.Default.Register<IFrameNavigationService>(() => navigationService);
        }
        public MainViewModel Main {
            get {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public HomePageViewModel HomePageViewModel {
            get {
                return ServiceLocator.Current.GetInstance<HomePageViewModel>();
            }
        }
        public NewReactionPageViewModel NewReactionViewModel {
            get {
                
                return ServiceLocator.Current.GetInstance<NewReactionPageViewModel>();
                
            }
        }
        public MaterialSelectWindowViewModel MaterialSelectWindowViewModel {
            get {
                return ServiceLocator.Current.GetInstance<MaterialSelectWindowViewModel>();
            }
        }
        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}
