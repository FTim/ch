using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ChClient2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ChClient2.View.MainPage window = new ChClient2.View.MainPage();
            //ViewModel.StartingMaterialViewModel VM = new StartingMaterialViewModel();
            //window.DataContext = VM;
            window.Show();
        }
    }
}
