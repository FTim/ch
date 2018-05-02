using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using InventoryReader;
using WpfApp4.Model;
using DocxGen;

namespace WpfApp4.View
{

    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        
        public ObservableCollection<StartingMaterial> smaterialList { get; set; }
        public ObservableCollection<Reagent> reagentList { get; set; }
        public ObservableCollection<Solvent> solventList { get; set; }
        public ObservableCollection<Product> productList { get; set; }
        public List<MoleculeData> readed;
        public string ReactionImgPath;
        public string ObservationImgPath;
        public string ReportPath;
        public DocxGenerator gen;
        public AddonInfo DataFromExcel;
        
        public MainPage()
        {
           

            readed = new List<MoleculeData>();
            InventoryReading reader = new InventoryReading();
            reader.FilePath = String.Concat(System.IO.Directory.GetCurrentDirectory(), "\\Inventory.xlsx");
            readed = reader.ReadTo();

            smaterialList = new ObservableCollection<StartingMaterial>();
            reagentList = new ObservableCollection<Reagent>();
            solventList = new ObservableCollection<Solvent>();
            productList = new ObservableCollection<Product>();

            ObservationImgPath = null;
            gen = new DocxGenerator();
            DataFromExcel = new AddonInfo();
            DataContext = this;
            InitializeComponent();

            StartingMaterialList.DataContext = this;
            ReagentsList.DataContext = this;
            SolventsList.DataContext = this;
            ProductsList.DataContext = this;


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
                if (!NameCASvalidate(item))
                {
                    var result=MessageBox.Show("Invalid starting material!");
                    return;
                     
                }

                foreach (var excelitem in readed)
                {
                    if(excelitem.Name==item.Name)
                    {
                        DataFromExcel.MW = excelitem.Mvalue;
                        DataFromExcel.dValue = excelitem.dvalue;
                        DataFromExcel.bpValue = excelitem.bpvalue;
                        DataFromExcel.mpValue = excelitem.mpvalue;
                    }
                }

                double? tmp_V;
                if (item.VValue == null||item.VValue=="") tmp_V = null;
                else tmp_V = Double.Parse(item.VValue);

                double? tmp_m;
                if (item.mValue == null || item.mValue == "") tmp_m = null;
                else tmp_m = Double.Parse(item.mValue);

                smMoleculeRow.Add(new MoleculeRow{ Name=item.Name, CAS=item.CAS, MWvalue=DataFromExcel.MW, Vvalue=tmp_V, mvalue=tmp_m, nvalue=new double(), Denvalue=DataFromExcel.dValue, Mpvalue=DataFromExcel.mpValue, Bpvalue=DataFromExcel.bpValue, Ratio=1});
            }
            gen.StartingMaterial = smMoleculeRow.ElementAt(0);
            //List<MoleculeRow> rMoleculeRow = new List<MoleculeRow>();
            foreach (var item in reagentList)
            {
                if (!NameCASvalidate(item))
                {
                    var result = MessageBox.Show("Invalid reagent!");
                    return;

                }

                foreach (var excelitem in readed)
                {
                    if (excelitem.Name == item.Name)
                    {
                        DataFromExcel.MW = excelitem.Mvalue;
                        DataFromExcel.dValue = excelitem.dvalue;
                        DataFromExcel.bpValue = excelitem.bpvalue;
                        DataFromExcel.mpValue = excelitem.mpvalue;
                        DataFromExcel.Vvalue = excelitem.Vvalue;
                    }
                }

                gen.AddReagent(new MoleculeRow { Name = item.Name, CAS = item.CAS, MWvalue = DataFromExcel.MW, Vvalue = DataFromExcel.Vvalue, mvalue = null, nvalue = new double(), Denvalue = DataFromExcel.dValue, Mpvalue = DataFromExcel.mpValue, Bpvalue =DataFromExcel.bpValue, Ratio=Double.Parse(item.Ratio) });
            }
            
            //List<MoleculeRow> sMoleculeRow = new List<MoleculeRow>();
            foreach (var item in solventList)
            {
                if (!NameCASvalidate(item))
                {
                    var result = MessageBox.Show("Invalid solvent!");
                    return;

                }

                foreach (var excelitem in readed)
                {
                    if (excelitem.Name == item.Name)
                    {
                        DataFromExcel.MW = excelitem.Mvalue;
                        DataFromExcel.dValue = excelitem.dvalue;
                        DataFromExcel.bpValue = excelitem.bpvalue;
                        DataFromExcel.mpValue = excelitem.mpvalue;
                        DataFromExcel.Vvalue = excelitem.Vvalue;
                    }
                }

                gen.AddSolvent(new MoleculeRow { Name = item.Name, CAS = item.CAS, MWvalue = DataFromExcel.MW, Vvalue = Double.Parse(item.VValue), mvalue = null, nvalue = new double(), Denvalue = DataFromExcel.dValue, Mpvalue = DataFromExcel.mpValue, Bpvalue = DataFromExcel.bpValue, Ratio = null });
            }
            List<MoleculeRow> pMoleculeRow = new List<MoleculeRow>();
            foreach (var item in productList)
            {
                gen.Product=(new MoleculeRow { Name = "", CAS = "", MWvalue = Double.Parse(item.MWValue), Vvalue = null, mvalue = null, nvalue =Double.Parse(item.nValue), Denvalue = null, Mpvalue = "", Bpvalue = "", Ratio = Double.Parse(item.Ratio) });
            }

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
            NameCASvalidate(new StartingMaterial { Name = txtSMAddName.Text, CAS = txtSMAddCAS.Text, mValue = txtSMAddmValue.Text, VValue = txtSMAddVValue.Text });
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
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg| All Files (*.*)|*.*";


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
            NameCASHint nchWindow = new NameCASHint(readed);
            nchWindow.ShowDialog();
            nchWindow.Owner = this;
        }

        public bool NameCASvalidate(StartingMaterial sm)
        {
            if (readed.Any(item => item.Name == sm.Name)) return true;
            return false;
        }
        public bool NameCASvalidate(Reagent r)
        {
            if (readed.Any(item => item.Name == r.Name)) return true;
            return false;
        }
        public bool NameCASvalidate(Solvent s)
        {
            if (readed.Any(item => item.Name == s.Name)) return true;
            return false;
        }

        private void btnPAdd_Click(object sender, RoutedEventArgs e)
        {
            productList.Add(new Product { MWValue = txtPAddMW.Text, Ratio = txtPAddRatio.Text, nValue = txtPAddnValue.Text });
        }
    }
    }

