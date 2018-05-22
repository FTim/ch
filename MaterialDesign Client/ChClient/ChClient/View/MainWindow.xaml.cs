
using ChClient.View;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SnackbarMessageQueue msg = new SnackbarMessageQueue();
            msg.Enqueue("Not implemented yet");

            SnackbarNotImplemented.MessageQueue = msg;

        }

        private void NewReaction_Button_Click(object sender, RoutedEventArgs e)
        {
            NewReactionWindow newreact = new NewReactionWindow();
            newreact.ShowDialog();
        }
    }
}
