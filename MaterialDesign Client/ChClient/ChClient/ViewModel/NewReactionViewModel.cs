using ChClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChClient.View;
using InventoryReader;
using System.ComponentModel;
using DocxGen;

namespace ChClient.ViewModel
{
    public class NewReactionViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<StartingMaterial> StartingMaterialList { get; set; }
        public ObservableCollection<Reagent> ReagentList { get; set; }
        public ObservableCollection<Solvent> SolventList { get; set; }
        public ObservableCollection<Product> ProductList { get; set; }
        public Reaction ThisReaction;

        private string _startingmaterialproblem;
        public string StartingMaterialProblem
        {
            get
            {
                return _startingmaterialproblem;
            }
            set
            {
                _startingmaterialproblem = value;
                OnPropertyChanged("StartingMaterialProblem");
            }
        }

        private string _reagentproblem;
        public string ReagentProblem
        {
            get
            {
                return _reagentproblem;
            }
            set
            {
                _reagentproblem = value;
                OnPropertyChanged("ReagentProblem");
            }
        }

        private string _solventproblem;
        public string SolventProblem
        {
            get
            {
                return _solventproblem;
            }
            set
            {
                _solventproblem = value;
                OnPropertyChanged("SolventProblem");
            }
        }

        private string _productproblem;
        public string ProductProblem
        {
            get
            {
                return _productproblem;
            }
            set
            {
                _productproblem = value;
                OnPropertyChanged("ProductProblem");
            }
        }

        public string ReactionImgPath { get; set; }
        public string ObservationImgPath { get; set; }
        public string DocxLocation { get; set; }


        public List<MoleculeData> Readed;
        public DocxGenerator Generator;
        public AddonInfo DataFromExcel;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public NewReactionViewModel()
        {
            StartingMaterialList = new ObservableCollection<StartingMaterial>();
            ReagentList = new ObservableCollection<Reagent>();
            SolventList = new ObservableCollection<Solvent>();
            ProductList = new ObservableCollection<Product>();
            ThisReaction = new Reaction();

            Readed = new List<MoleculeData>();
            InventoryReading reader = new InventoryReading();
            reader.FilePath = String.Concat(System.IO.Directory.GetCurrentDirectory(), "\\Resources\\Inventory.xlsx");
            Readed = reader.ReadTo();
            
            DocxLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            Generator = new DocxGenerator();
            DataFromExcel = new AddonInfo();


        }

        public void AddStartingMaterial(string name, string cas, string m, string v)
        {
            StartingMaterialProblem = "";


            double tmpdoublecheck;

            if (StartingMaterialList.Count == 1)
            {
                StartingMaterialProblem = "Already have a Starting Material! Cannot add new one.";
                return;
            }
            else
            {
                if (!NameValidate(name))
                {

                    StartingMaterialProblem = "Invalid name";
                    return;
                }
                if (m == "" && v == "")
                {
                    StartingMaterialProblem = "Both m and V value missing! One of them required!";
                    return;
                }
                if (m != "" && v != "")
                {
                    StartingMaterialProblem = "Both m and V value added! One of them required, what to do? :D";
                    return;
                }
                if (v == "")
                {
                    if (!Double.TryParse(m, out tmpdoublecheck))
                    {

                        StartingMaterialProblem = "Invalid m value";
                        return;
                    }
                }
                else
                {
                    if (!Double.TryParse(v, out tmpdoublecheck))
                    {

                        StartingMaterialProblem = "Invalid V value";
                        return;
                    }
                }

            }

            StartingMaterialList.Add(new StartingMaterial { Name = name, CAS = cas, mValue = m, VValue = v });
        }

        public void AddReagent(string name, string cas, string ratio)
        {

            ReagentProblem = "";

            double tmpdoublecheck;


            if (!NameValidate(name))
            {

                ReagentProblem = "Invalid name";
                return;
            }
            if (ratio == "")
            {

                ReagentProblem = "Ratio required";

                return;
            }
            else
            {
                if (!Double.TryParse(ratio, out tmpdoublecheck))
                {

                    ReagentProblem = "Invalid Ratio value";
                    return;
                }
            }

            ReagentProblem = "";

            ReagentList.Add(new Reagent { Name = name, CAS = cas, Ratio = ratio });
        }

        public void AddSolvent(string name, string cas, string v)
        {
            SolventProblem = "";
            double tmpdoublecheck;


            if (!NameValidate(name))
            {

                SolventProblem = "Invalid name";
                return;
            }
            if (v == "")
            {

                SolventProblem = "V value required";

                return;
            }
            else
            {
                if (!Double.TryParse(v, out tmpdoublecheck))
                {
                    SolventProblem = "Invalid V value";
                    return;
                }
            }
            SolventProblem = "";
            SolventList.Add(new Solvent { Name = name, CAS = cas, VValue = v });
        }

        public void AddProduct(string mw, string ratio, string n)
        {

            ProductProblem = "";

            double tmpdoublecheck;

            if (mw == "")
            {

                ProductProblem = "MW value required";
                return;
            }
            else
            {
                if (!Double.TryParse(mw, out tmpdoublecheck))
                {

                    ProductProblem = "Invalid MW value";
                    return;
                }
            }

            if (ratio == "")
            {

                ProductProblem = "Ratio required";
                return;
            }
            else
            {
                if (!Double.TryParse(ratio, out tmpdoublecheck))
                {

                    ProductProblem = "Invalid Ratio";
                    return;
                }
            }

            if (n == "")
            {

                ProductProblem = "n value required";
                return;
            }
            else
            {
                if (!Double.TryParse(n, out tmpdoublecheck))
                {

                    ProductProblem = "Invalid n value";
                    return;
                }
            }
            ProductList.Add(new Product { MWValue = mw, Ratio = ratio, nValue = n });
        }

        private bool NameValidate(string Name)
        {
            if (Readed.Any(item => item.Name == Name)) return true;
            return false;
        }

        public string AddReactionImg()
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

                //MessageBox.Show("Reaction image selected: " + ReactionImgPath);
                return ReactionImgPath;

            }
            else return "No image selected";
        }

        public string AddObservationImg()
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
                //MessageBox.Show("Observation image selected: " + ObservationImgPath);

                return ObservationImgPath;
            }
            else return "No image selected";
        }

        public string AddFileLocation()
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
                DocxLocation = sfd.FileName;
                //MessageBox.Show("Report will be saved to: " + ReportPath);
                return "Report will be saved to: " + DocxLocation;
            }
            else return "No saving location given, using default: " + DocxLocation;
        }
        
        public string SaveDocx(bool sketch)
        {
            try { 
            Generator.FilePath = DocxLocation;

            Generator.ReactionCode = ThisReaction.Code;
            Generator.Chemist = ThisReaction.Chemist;
            Generator.Chiefchemist = ThisReaction.Chiefchemist;
            Generator.ProjectName = ThisReaction.Project;
            Generator.Laboratory = ThisReaction.Laboratory;
            Generator.StartDate = DateTime.Parse(ThisReaction.StartDate);
            Generator.ClosureDate = DateTime.Parse(ThisReaction.ClosureDate);
            Generator.PreviousStep = ThisReaction.PreviousStep;
            Generator.Literature = ThisReaction.Literature;

            Generator.ReactionImgPath = ReactionImgPath;
            //List<MoleculeRow> StartingMaterialRow = new List<MoleculeRow>();
            foreach (var item in StartingMaterialList)
            {

                foreach (var excelitem in Readed)
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

                Generator.StartingMaterial = new MoleculeRow { Name = item.Name, CAS = item.CAS, MWvalue = DataFromExcel.MW, Vvalue = tmp_V, mvalue = tmp_m, nvalue = new double(), Denvalue = DataFromExcel.dValue, Mpvalue = DataFromExcel.mpValue, Bpvalue = DataFromExcel.bpValue, Ratio = 1 };
            }


            foreach (var item in ReagentList)
            {


                foreach (var excelitem in Readed)
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

                Generator.AddReagent(new MoleculeRow { Name = item.Name, CAS = item.CAS, MWvalue = DataFromExcel.MW, Vvalue = DataFromExcel.Vvalue, mvalue = null, nvalue = new double(), Denvalue = DataFromExcel.dValue, Mpvalue = DataFromExcel.mpValue, Bpvalue = DataFromExcel.bpValue, Ratio = Double.Parse(item.Ratio) });
            }


            foreach (var item in SolventList)
            {


                foreach (var excelitem in Readed)
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

                Generator.AddSolvent(new MoleculeRow { Name = item.Name, CAS = item.CAS, MWvalue = DataFromExcel.MW, Vvalue = Double.Parse(item.VValue), mvalue = null, nvalue = new double(), Denvalue = DataFromExcel.dValue, Mpvalue = DataFromExcel.mpValue, Bpvalue = DataFromExcel.bpValue, Ratio = null });
            }
           // List<MoleculeRow> pMoleculeRow = new List<MoleculeRow>();
            foreach (var item in ProductList)
            {
                Generator.Product = (new MoleculeRow { Name = "Product", CAS = "", MWvalue = Double.Parse(item.MWValue), Vvalue = null, mvalue = null, nvalue = Double.Parse(item.nValue), Denvalue = null, Mpvalue = "", Bpvalue = "", Ratio = Double.Parse(item.Ratio) });
            }

            Generator.ProcedureText = ThisReaction.Procedure;
            Generator.Yield = ThisReaction.Yield;
            Generator.ObservationText = ThisReaction.Observation;
            List<string> obsimg = new List<string>();
            obsimg.Add(ObservationImgPath);
            if (ObservationImgPath != null) Generator.ObservationImg = obsimg;

            if (sketch) Generator.GenerateReport(false);
            else Generator.GenerateReport(true);
            return "Done!";
            }
            catch
            {
                return "Something essential is missing...";
            }
        }
        
    }
}
