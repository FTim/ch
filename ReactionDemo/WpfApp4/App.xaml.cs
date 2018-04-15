using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using WpfApp4;
using WpfApp4.ViewModel;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            WpfApp4.View.MainPage window = new WpfApp4.View.MainPage();
            ViewModel.StartingMaterialViewModel VM = new StartingMaterialViewModel();
            window.DataContext = VM;
            window.Show();
        }
    }
}
