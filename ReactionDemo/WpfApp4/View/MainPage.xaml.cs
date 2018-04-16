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
using DocxGen;

using WpfApp4.Model;

namespace WpfApp4.View
{
    
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        public int asd;
        public ObservableCollection<StartingMaterial> smaterialList { get; set; }
        public ObservableCollection<Reagent> reagentList { get; set; }
        public ObservableCollection<Solvent> solventList { get; set; }
        public string ReactionImgPath;
        public string ObservationImgPath;
        public string ReportPath;
        public DocxGenerator gen;
        
        public MainPage()
        {
            asd = 42;
            smaterialList = new ObservableCollection<StartingMaterial>();
            reagentList = new ObservableCollection<Reagent>();
            solventList = new ObservableCollection<Solvent>();
            ObservationImgPath = null;
            gen = new DocxGenerator();
            DataContext = this;
            InitializeComponent();

            StartingMaterialList.DataContext = this;
            ReagentsList.DataContext = this;
            SolventsList.DataContext = this;
            
            
        }

        private void btnSaveDocx_Click(object sender, RoutedEventArgs e)
        {
            gen.FilePath = ReportPath;
           
            gen.ReactionCode = txtReactionCode.Text;
            gen.Chemist = txtChemist.Text;
            gen.Chiefchemist = txtChiefchemist.Text;
            gen.ProjectName = txtProject.Text;
            gen.Laboratory = txtLaboratory.Text;
            gen.StartDate = datetimeStart.DisplayDate;
            gen.ClosureDate = datetimeClosure.DisplayDate;
            gen.PreviousStep = txtPreviousStep.Text;
            gen.Literature = txtLiterature.Text;

            gen.ReactionImgPath = ReactionImgPath;
            List<MoleculeRow> smMoleculeRow = new List<MoleculeRow>();
            foreach (var item in smaterialList)
            {
                double? tmp_V;
                if (item.VValue == null||item.VValue=="") tmp_V = null;
                else tmp_V = Double.Parse(item.VValue);

                double? tmp_m;
                if (item.mValue == null || item.mValue == "") tmp_m = null;
                else tmp_m = Double.Parse(item.mValue);

                smMoleculeRow.Add(new MoleculeRow{ Name=item.Name, CAS=item.CAS, MWvalue=42, Vvalue=tmp_V, mvalue=tmp_m, nvalue=43, Denvalue=44, Mpvalue="45", Bpvalue="46", Ratio=47});
            }
            gen.StartingMaterial = smMoleculeRow.ElementAt(0);
            List<MoleculeRow> rMoleculeRow = new List<MoleculeRow>();
            foreach (var item in reagentList)
            {
                

                gen.AddReagent(new MoleculeRow { Name = item.Name, CAS = item.CAS, MWvalue = 42, Vvalue = 43, mvalue = 44, nvalue = 43, Denvalue = 44, Mpvalue = "45", Bpvalue ="46", Ratio=Double.Parse(item.Ratio) });
            }
            
            List<MoleculeRow> sMoleculeRow = new List<MoleculeRow>();
            foreach (var item in solventList)
            {


               gen.AddSolvent(new MoleculeRow { Name = item.Name, CAS = item.CAS, MWvalue = 42, Vvalue = Double.Parse(item.VValue), mvalue = 44, nvalue = 43, Denvalue = 44, Mpvalue = "45", Bpvalue = "46", Ratio = 47 });
            }
            gen.Product = new MoleculeRow { Name="product", CAS="", MWvalue=42, mvalue=43, Vvalue=44, Bpvalue="45", Denvalue=46, Mpvalue="47", nvalue=48, Ratio=2};

            gen.ProcedureText = txtProcedure.Text;
            gen.Yield = txtYield.Text;
            gen.ObservationText = txtObservation.Text;
            List<string> obsimg = new List<string>();
            obsimg.Add(ObservationImgPath);
            if(ObservationImgPath!=null)gen.ObservationImg =obsimg;

            gen.GenerateReport();
            MessageBox.Show("Done!");          
        }

        

        private void btnSAdd_Click(object sender, RoutedEventArgs e)
        {
            solventList.Add(new Solvent { Name = txtSAddName.Text, CAS = txtSAddCAS.Text, VValue = txtSAddVValue.Text });
            
        }

        private void btnSMAdd_Click(object sender, RoutedEventArgs e)
        {
            smaterialList.Add(new StartingMaterial { Name = txtSMAddName.Text, CAS = txtSMAddCAS.Text, mValue=txtSMAddmValue.Text, VValue = txtSMAddVValue.Text });
        }

        private void btnRAdd_Click(object sender, RoutedEventArgs e)
        {
            reagentList.Add(new Reagent { Name = txtRAddName.Text, CAS = txtRAddCAS.Text, Ratio = txtRAddRatio.Text });
        }

        private void btnAddRectionImg_Click(object sender, RoutedEventArgs e)
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
                ReactionImgPath = dlg.FileName;
                MessageBox.Show("Reaction image selected: "+ReactionImgPath);
            }
            else MessageBox.Show("No image selected");
        }

        private void btnAddObservationImg_Click(object sender, RoutedEventArgs e)
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
                ObservationImgPath = dlg.FileName;
                MessageBox.Show("Observation image selected: " + ObservationImgPath);
            }
            else MessageBox.Show("No image selected");
        }

        private void btnSelectLocation_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
           // Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();



            // Set filter for file extension and default file extension 
            //dlg.DefaultExt = ".jpg";
           // dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";

            sfd.DefaultExt = ".docx";
            sfd.Filter = "Docx Files (*.docx)| All files (*.*)";


            // Display OpenFileDialog by calling ShowDialog method 
            //Nullable<bool> result = dlg.ShowDialog();

            Nullable<bool> result2 = sfd.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result2 == true)
            {
                // Open document 
                ReportPath = sfd.FileName;
                MessageBox.Show("Report will be saved to: "+ReportPath);
            }
            else MessageBox.Show("No saving location given");
        }

        
        private void btnHint_Click(object sender, RoutedEventArgs e)
        {
            NameCASHint nchWindow = new NameCASHint();
            nchWindow.ShowDialog();
            nchWindow.Owner = this;
        }
    }
    }

