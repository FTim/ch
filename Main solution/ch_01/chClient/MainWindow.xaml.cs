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

namespace chClient
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

        private void NewProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not implemented yet!");
        }

        private void AllProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not implemented yet!");
        }

        private void MyProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not implemented yet!");
        }

        private void InventoryCheckBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not implemented yet!");
        }

        private void ManualInventoryUpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not implemented yet!");
        }

        private void InventoryToExcelExportBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not implemented yet!");
        }

        private void NewReactionBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not implemented yet!");
        }

        private void AllReactionBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not implemented yet!");
        }

        private void MyReactionBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not implemented yet!");
        }

        private void TestNewReactionBtn_Click(object sender, RoutedEventArgs e)
        {
            ReactionDemo rd = new ReactionDemo();
            rd.Show();
            //rd.Visibility = Visibility.Visible;
            Focusable = false;
            rd.Owner = this;
            rd.Topmost = true;
        }
    }
}
