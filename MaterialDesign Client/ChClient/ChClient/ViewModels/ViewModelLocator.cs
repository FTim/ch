
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
            //SimpleIoc.Default.Register<CurrentUserMessenger>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<HomePageViewModel>();
            SimpleIoc.Default.Register<ManageUsersPageViewModel>();
            SimpleIoc.Default.Register<DatabaseResetPageViewModel>();

            SimpleIoc.Default.Register<NewProjectPageViewModel>();
            SimpleIoc.Default.Register<BrowseProjectsPageViewModel>();
            SimpleIoc.Default.Register<ProjectPageViewModel>();

            SimpleIoc.Default.Register<NewReactionPageViewModel>();
            SimpleIoc.Default.Register<MaterialSelectWindowViewModel>();
            SimpleIoc.Default.Register<BrowseReactionsPageViewModel>();
            SimpleIoc.Default.Register<ReactionPageViewModel>();

            SimpleIoc.Default.Register<AddNewMoleculePageViewModel>();
            SimpleIoc.Default.Register<ManualInventoryUpdatePageViewModel>();
            SimpleIoc.Default.Register<ExportExcelPageViewModel>();

            SetupNavigation();

            // var openFileDialogService = new OpenFileDialogService();


            SimpleIoc.Default.Register<IOpenFileDialogService, OpenFileDialogService>();
            SimpleIoc.Default.Register<ILogger, Logger>();
            SimpleIoc.Default.Register<ISelectMoleculeDialogService, SelectMoleculeDialogService>();
            SimpleIoc.Default.Register<IExcelReaderService, ExcelReaderService>();
            SimpleIoc.Default.Register<IDBService, DBService>();
            SimpleIoc.Default.Register<IDocxGeneratorService, DocxGeneratorService>();

            

        }

        private static void SetupNavigation()
        {
            var navigationService = new FrameNavigationService();
            navigationService.Configure("Home", new Uri("../Views/HomePage.xaml", UriKind.Relative));
            navigationService.Configure("ManageUsers", new Uri("../Views/ManageUsersPage.xaml", UriKind.Relative));
            navigationService.Configure("DatabaseReset", new Uri("../Views/DatabaseResetPage.xaml", UriKind.Relative));

            navigationService.Configure("NewProject", new Uri("../Views/NewProjectPage.xaml", UriKind.Relative));
            navigationService.Configure("BrowseProjects", new Uri("../Views/BrowseProjectsPage.xaml", UriKind.Relative));
            navigationService.Configure("Project", new Uri("../Views/ProjectPage.xaml", UriKind.Relative));

            navigationService.Configure("NewReaction", new Uri("../Views/NewReactionPage.xaml", UriKind.Relative));
            navigationService.Configure("BrowseReactions", new Uri("../Views/BrowseReactionsPage.xaml", UriKind.Relative));
            navigationService.Configure("Reaction", new Uri("../Views/ReactionPage.xaml", UriKind.Relative));

            navigationService.Configure("AddNewMolecule", new Uri("../Views/AddNewMoleculePage.xaml", UriKind.Relative));
            navigationService.Configure("ManualInventoryUpdate", new Uri("../Views/ManualInventoryUpdatePage.xaml", UriKind.Relative));
            navigationService.Configure("ExportExcel", new Uri("../Views/ExportExcelPage.xaml", UriKind.Relative));

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
        public ManageUsersPageViewModel ManageUsersViewModel{
            get {
                return ServiceLocator.Current.GetInstance<ManageUsersPageViewModel>();
            }
        }
        public DatabaseResetPageViewModel DatabaseResetViewModel {
            get {
                SimpleIoc ioc = ServiceLocator.Current as SimpleIoc;
                return ioc.GetInstanceWithoutCaching<DatabaseResetPageViewModel>(Guid.NewGuid().ToString());
            }
        }
        public NewProjectPageViewModel NewProjectViewModel {
            get {
                SimpleIoc ioc = ServiceLocator.Current as SimpleIoc;
                return ioc.GetInstanceWithoutCaching<NewProjectPageViewModel>(Guid.NewGuid().ToString());
            }
        }
        public BrowseProjectsPageViewModel BrowseProjectsViewModel {
            get {
                SimpleIoc ioc = ServiceLocator.Current as SimpleIoc;
                return ioc.GetInstanceWithoutCaching<BrowseProjectsPageViewModel>(Guid.NewGuid().ToString());
            }
        }
        public ProjectPageViewModel ProjectViewModel {
            get {
                //mindig új példány legyen
                SimpleIoc ioc = ServiceLocator.Current as SimpleIoc;
                return ioc.GetInstanceWithoutCaching<ProjectPageViewModel>(Guid.NewGuid().ToString());
                //return ServiceLocator.Current.GetInstance<NewReactionPageViewModel>();

            }
        }
        public ReactionPageViewModel ReactionViewModel {
            get {
                //mindig új példány legyen
                SimpleIoc ioc = ServiceLocator.Current as SimpleIoc;
                return ioc.GetInstanceWithoutCaching<ReactionPageViewModel>(Guid.NewGuid().ToString());
                //return ServiceLocator.Current.GetInstance<NewReactionPageViewModel>();

            }
        }
        public NewReactionPageViewModel NewReactionViewModel {
            get {
                //mindig új példány legyen
                SimpleIoc ioc = ServiceLocator.Current as SimpleIoc;
                return ioc.GetInstanceWithoutCaching<NewReactionPageViewModel>(Guid.NewGuid().ToString());
                //return ServiceLocator.Current.GetInstance<NewReactionPageViewModel>();

            }
        }
        public MaterialSelectWindowViewModel MaterialSelectWindowViewModel {
            get {
                SimpleIoc ioc = ServiceLocator.Current as SimpleIoc;
                return ioc.GetInstanceWithoutCaching<MaterialSelectWindowViewModel>(Guid.NewGuid().ToString());
                //return ServiceLocator.Current.GetInstance<MaterialSelectWindowViewModel>();
            }
        }
        public BrowseReactionsPageViewModel BrowseReactionsViewModel {
            get {
                SimpleIoc ioc = ServiceLocator.Current as SimpleIoc;
                return ioc.GetInstanceWithoutCaching<BrowseReactionsPageViewModel>(Guid.NewGuid().ToString());
                //return ServiceLocator.Current.GetInstance<BrowseReactionsPageViewModel>();
            }
        }
        public AddNewMoleculePageViewModel AddNewMoleculeViewModel {
            get {
                return ServiceLocator.Current.GetInstance<AddNewMoleculePageViewModel>();
            }
        }
        public ManualInventoryUpdatePageViewModel ManualInventoryUpdateViewModel {
            get {
                return ServiceLocator.Current.GetInstance<ManualInventoryUpdatePageViewModel>();
            }
        }
        public ExportExcelPageViewModel ExportExcelViewModel {
            get {
                return ServiceLocator.Current.GetInstance<ExportExcelPageViewModel>();
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
