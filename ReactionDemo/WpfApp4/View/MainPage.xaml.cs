using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using InventoryReader;
using WpfApp4.Model;
using DocxGen;
using System.Windows.Media.Imaging;

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

            //datetimeStart.DisplayDate = DateTime.Now;
            datetimeStart.SelectedDate = DateTime.Now;
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

                foreach (var excelitem in readed)
                {
                    if (excelitem.Name == item.Name)
                    {
                        DataFromExcel.MW = excelitem.Mvalue;
                        DataFromExcel.dValue = excelitem.dvalue;
                        DataFromExcel.bpValue = excelitem.bpvalue;
                        DataFromExcel.mpValue = excelitem.mpvalue;
                    }
                }

                double? tmp_V;
                if (item.VValue == null || item.VValue == "") tmp_V = null;
                else tmp_V = Double.Parse(item.VValue);

                double? tmp_m;
                if (item.mValue == null || item.mValue == "") tmp_m = null;
                else tmp_m = Double.Parse(item.mValue);

                gen.StartingMaterial = new MoleculeRow { Name = item.Name, CAS = item.CAS, MWvalue = DataFromExcel.MW, Vvalue = tmp_V, mvalue = tmp_m, nvalue = new double(), Denvalue = DataFromExcel.dValue, Mpvalue = DataFromExcel.mpValue, Bpvalue = DataFromExcel.bpValue, Ratio = 1 };
            }


            foreach (var item in reagentList)
            {
               

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

                gen.AddReagent(new MoleculeRow { Name = item.Name, CAS = item.CAS, MWvalue = DataFromExcel.MW, Vvalue = DataFromExcel.Vvalue, mvalue = null, nvalue = new double(), Denvalue = DataFromExcel.dValue, Mpvalue = DataFromExcel.mpValue, Bpvalue = DataFromExcel.bpValue, Ratio = Double.Parse(item.Ratio) });
            }


            foreach (var item in solventList)
            {
                

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
                gen.Product = (new MoleculeRow { Name = "Product", CAS = "", MWvalue = Double.Parse(item.MWValue), Vvalue = null, mvalue = null, nvalue = Double.Parse(item.nValue), Denvalue = null, Mpvalue = "", Bpvalue = "", Ratio = Double.Parse(item.Ratio) });
            }

            gen.ProcedureText = txtProcedure.Text;
            gen.Yield = txtYield.Text;
            gen.ObservationText = txtObservation.Text;
            List<string> obsimg = new List<string>();
            obsimg.Add(ObservationImgPath);
            if (ObservationImgPath != null) gen.ObservationImg = obsimg;

            if (Boolean.Parse(SketchCB.IsChecked.ToString())) gen.GenerateReport(false);
            gen.GenerateReport(true);
            MessageBox.Show("Done!");
        }



        private void btnSAdd_Click(object sender, RoutedEventArgs e)
        {
            
            SNameProblem.Content = "";
            SVProblem.Content = "";
            double tmpdoublecheck;


            if (!NameValidate(txtSAddName.Text))
            {

                SNameProblem.Content = "Invalid name";
                return;
            }
            if (txtSAddVValue.Text == "")
            {
                
                SVProblem.Content = "V value required";

                return;
            }
            else
            {
                if (!Double.TryParse(txtSAddVValue.Text, out tmpdoublecheck))
                {
                    SVProblem.Content = "Invalid V value";
                    return;
                }
            }
            MessageBox.Show("Added");
            SNameProblem.Content = "";
            SVProblem.Content = "";
            solventList.Add(new Solvent { Name = txtSAddName.Text, CAS = txtSAddCAS.Text, VValue = txtSAddVValue.Text });

        }

        private void btnSMAdd_Click(object sender, RoutedEventArgs e)
        {
            SMNameProblem.Content = "";
            SMmProblem.Content = "";
            SMVProblem.Content = "";
            
            double tmpdoublecheck;

            if (smaterialList.Count == 1)
            {
                MessageBox.Show("Already have a Starting Material! Cannot add new one.");
                return;
            }
            else
            {
                if (!NameValidate(txtSMAddName.Text))
                {
                    
                    SMNameProblem.Content = "Invalid name";
                    return;
                }
                if (txtSMAddmValue.Text == "" && txtSMAddVValue.Text == "")
                {
                    MessageBox.Show("Both m and V value missing! One of them required!");
                    return;
                }
                if (txtSMAddmValue.Text != "" && txtSMAddVValue.Text != "")
                {
                    MessageBox.Show("Both m and V value added! One of them required, what to do? :D");
                    return;
                }
                if (txtSMAddVValue.Text == "")
                {
                    if (!Double.TryParse(txtSMAddmValue.Text, out tmpdoublecheck))
                    {
                        
                        SMmProblem.Content = "Invalid m value";
                        return;
                    }
                }
                else
                {
                    if (!Double.TryParse(txtSMAddVValue.Text, out tmpdoublecheck))
                    {
                        
                        SMVProblem.Content = "Invalid V value";
                        return;
                    }
                }


            }
            
            MessageBox.Show("Added");
            SMNameProblem.Content = "";
            SMmProblem.Content = "";
            SMVProblem.Content = "";

            smaterialList.Add(new StartingMaterial { Name = txtSMAddName.Text, CAS = txtSMAddCAS.Text, mValue = txtSMAddmValue.Text, VValue = txtSMAddVValue.Text });
        }


        private void btnRAdd_Click(object sender, RoutedEventArgs e)
        {
            RNameProblem.Content = "";
            RCASProblem.Content = "";
            RRatioProblem.Content = "";

            
            double tmpdoublecheck;


            if (!NameValidate(txtRAddName.Text))
            {

                RNameProblem.Content = "Invalid name";
                return;
            }
            if (txtRAddRatio.Text == "")
            {

                RRatioProblem.Content = "Ratio required";
                
                return;
            }
            else
            {
                if (!Double.TryParse(txtRAddRatio.Text, out tmpdoublecheck))
                {

                    RRatioProblem.Content = "Invalid Ratio value";
                    return;
                }
            }
            
            MessageBox.Show("Added");

            RNameProblem.Content = "";
            RCASProblem.Content = "";
            RRatioProblem.Content = "";

            reagentList.Add(new Reagent { Name = txtRAddName.Text, CAS = txtRAddCAS.Text, Ratio = txtRAddRatio.Text });
        }

        private void btnAddRectionImg_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|All Files (*.*)|*.*";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                ReactionImgPath = dlg.FileName;

                MessageBox.Show("Reaction image selected: " + ReactionImgPath);
                ReactionIcon.Source = new BitmapImage(new Uri(ReactionImgPath));
                ReactionIcon.Visibility = Visibility.Visible;
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
                ObservationIcon.Source = new BitmapImage(new Uri(ObservationImgPath));
                ObservationIcon.Visibility = Visibility.Visible;
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
                MessageBox.Show("Report will be saved to: " + ReportPath);
                SaveLocationLabel.Content = ReportPath;
            }
            else MessageBox.Show("No saving location given");
        }


        private void btnHint_Click(object sender, RoutedEventArgs e)
        {
            NameCASHint nchWindow = new NameCASHint(readed);
            nchWindow.ShowDialog();
            nchWindow.Owner = this;
        }

        private bool NameValidate(string Name)
        {
            if (readed.Any(item => item.Name == Name)) return true;
            return false;
        }

        

        private void btnPAdd_Click(object sender, RoutedEventArgs e)
        {
            PMWProblem.Content = "";
            PRatioProblem.Content = "";
            PnProblem.Content = "";
            
            double tmpdoublecheck;
            
            if (txtPAddMW.Text == "")
            {

                PMWProblem.Content = "MW value required";
                return;
            }
            else
            {
                if (!Double.TryParse(txtPAddMW.Text, out tmpdoublecheck))
                {

                    PMWProblem.Content = "Invalid MW value";
                    return;
                }
            }

            if (txtPAddRatio.Text == "")
            {

                PRatioProblem.Content = "Ratio required";
                return;
            }
            else
            {
                if (!Double.TryParse(txtPAddRatio.Text, out tmpdoublecheck))
                {

                    PRatioProblem.Content = "Invalid Ratio";
                    return;
                }
            }

            if (txtPAddnValue.Text == "")
            {

                PnProblem.Content = "n value required";
                return;
            }
            else
            {
                if (!Double.TryParse(txtPAddnValue.Text, out tmpdoublecheck))
                {

                    PnProblem.Content = "Invalid n value";
                    return;
                }
            }
            MessageBox.Show("Added");
            

           


            productList.Add(new Product { MWValue = txtPAddMW.Text, Ratio = txtPAddRatio.Text, nValue = txtPAddnValue.Text });
        }

        private void btnInit_Click(object sender, RoutedEventArgs e)
        {
            InitFields();
        }

        private void InitFields()
        {
            ReportPath = "";

            if (Boolean.Parse(ClrHeaderCB.IsChecked.ToString()))
                {
                txtReactionCode.Text = "";
                txtChemist.Text = "";
                txtChiefchemist.Text = "";
                txtProject.Text = "";
                txtLaboratory.Text = "";
                datetimeStart.DisplayDate = DateTime.Now;
                datetimeClosure.DisplayDate = DateTime.Now;
                txtPreviousStep.Text = "";
                txtLiterature.Text = "";

                ReactionImgPath = "";
                List<MoleculeRow> smMoleculeRow = new List<MoleculeRow>();

                ReactionIcon.Visibility = Visibility.Hidden;
            }
            if (Boolean.Parse(ClrSMCB.IsChecked.ToString()))
            {
                smaterialList.Clear();
                if (Boolean.Parse(ClrSMInputCB.IsChecked.ToString()))
                {
                    txtSMAddName.Text = "";
                    txtSMAddCAS.Text = "";
                    txtSMAddmValue.Text = "";
                    txtSMVValue.Text = "";
                }
            }

            if (Boolean.Parse(ClrRCB.IsChecked.ToString()))
            {
                reagentList.Clear();
                if (Boolean.Parse(ClrRInputCB.IsChecked.ToString()))
                {
                    txtRAddName.Text = "";
                    txtRAddCAS.Text = "";
                    txtRAddRatio.Text = "";
                }
            }

            if (Boolean.Parse(ClrSCB.IsChecked.ToString()))
            {
                solventList.Clear();
                if (Boolean.Parse(ClrSInputCB.IsChecked.ToString()))
                {
                    txtSAddName.Text = "";
                    txtSAddCAS.Text = "";
                    txtSAddVValue.Text = "";
                }
            }

            if (Boolean.Parse(ClrPCB.IsChecked.ToString()))
            {
                productList.Clear();
                if (Boolean.Parse(ClrPInputCB.IsChecked.ToString()))
                {
                    txtPAddMW.Text = "";
                    txtPAddnValue.Text = "";
                    txtPAddRatio.Text = "";
                }
            }

            if (Boolean.Parse(ClrProcCB.IsChecked.ToString()))
            {
                txtProcedure.Text = "";
                txtYield.Text = "";
                txtObservation.Text = "";
                //List<string> obsimg = new List<string>();
                ObservationImgPath = null;
                ObservationIcon.Visibility = Visibility.Hidden;
            }



            txtProcedure.Text = "";
            txtYield.Text = "";
            txtObservation.Text = "";
            //List<string> obsimg = new List<string>();
            ObservationImgPath = null;

            SaveLocationLabel.Content = ReportPath;

            MessageBox.Show("Init done");
        }

    }
}

