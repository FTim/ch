using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using InventoryReader;
using System.IO;

namespace chClient
{
    /// <summary>
    /// Interaction logic for ReactionDemo.xaml
    /// </summary>
    public partial class ReactionDemo : Window
    {
        public List<StartingMaterialInputData> sData { get; set; }
        public List<ReagentInputData> rData { get; set; }
        public List<SolventInputData> soData { get; set; }
        public List<MoleculeData> source = new List<MoleculeData>();
        public InventoryReading r = new InventoryReading();
        public InventoryReading PassReadedMolecules()
        {
            return r;
        }
        public ReactionDemo()
        {
            sData = new List<StartingMaterialInputData>();
            rData = new List<ReagentInputData>();
            soData = new List<SolventInputData>();

            sData.Add(new StartingMaterialInputData { Name = "asd", CAS = "rnd", m = null, V = 100 });
            sData.Add(new StartingMaterialInputData { Name = "dsa", CAS = "42", m = 70, V = null });

            rData.Add(new ReagentInputData { Name = "asd", CAS = "rnd",Ratio=3 });
            rData.Add(new ReagentInputData { Name = "13124314", CAS = "ophu", Ratio = 3.141 });
            rData.Add(new ReagentInputData { Name = "rnd", CAS = "12-123-45", Ratio = 2 });


            soData.Add(new SolventInputData { Name = "asd", CAS = "rnd", V = 100 });
            soData.Add(new SolventInputData { Name = "asd", CAS = "rnd", V = 100 });
            InitializeComponent();
            DataContext = this;
        }

        private void RandomBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("asd");
        }
        private void NameCASHints_Click(object sender, RoutedEventArgs e)
        {
            
            NameCASHint NCHWindow = new NameCASHint(r);
            NCHWindow.Show();
            NCHWindow.Owner = this;
            NCHWindow.Topmost = true;
            
        }

        private void AddReactionImgBtn_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                //MessageBox.Show(filename);
            }
        }

        private void AddStartingMaterialBtn_Click(object sender, RoutedEventArgs e)
        {
            Window form = new AddStartingMaterial(sData);
            form.Owner = this;

            form.ShowDialog();

            
            
        }
        public void AddStartingMaterial(List<StartingMaterialInputData> asd)
        {
            sData.AddRange(asd);
        }

        private void AddReagentBtn_Click(object sender, RoutedEventArgs e)
        {
            Window form = new AddReagent();
            form.Show();
        }

        private void AddSolventBtn_Click(object sender, RoutedEventArgs e)
        {
            Window form = new AddSolvent();
            form.Show();
        }

        private void New_Reaction_Demo_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Topmost = false;
            Owner.Focusable = true;
        }
    }
}
