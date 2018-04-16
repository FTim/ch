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

using WpfApp4.Model;

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
        
        public MainPage()
        {
            smaterialList = new ObservableCollection<StartingMaterial>();
            reagentList = new ObservableCollection<Reagent>();
            solventList = new ObservableCollection<Solvent>();
            DataContext = this;
            InitializeComponent();

            StartingMaterialList.DataContext = this;
            ReagentsList.DataContext = this;
            SolventsList.DataContext = this;
            
            
        }

        private void btnSaveDocx_Click(object sender, RoutedEventArgs e)
        {
            /*
            DocxGenerator gen = new DocxGenerator();
            gen.ReactionCode = txtReactionCode.Text;
            gen.Chemist = txtChemist.Text;
            gen.Chiefchemist = txtChiefchemist.Text;
            gen.ProjectName = txtProject.Text;
            gen.Laboratory = txtLaboratory.Text;
            gen.StartDate = datetimeStart.DisplayDate;
            gen.ClosureDate = datetimeClosure.DisplayDate;
            gen.PreviousStep = txtPreviousStep.Text;
            gen.Literature = txtLiterature.Text;
            gen.ReactionImgPath = @"C:\Users\Timea\Desktop\img.jpg";

            
            gen.StartingMaterial = new MoleculeRow();
            gen.StartingMaterial.Name = StartingMaterialGrid.ToString();
            
            GridViewRowPresenter sm_row = new GridViewRowPresenter();
            */
           
        }

        

        private void btnSAdd_Click(object sender, RoutedEventArgs e)
        {
            solventList.Add(new Solvent { Name = txtSAddName.Text, CAS = txtSAddCAS.Text, VValue = txtSAddVValue.Text });
            //MessageBox.Show(solventList.ElementAt(0).Name);
        }

        private void btnSMAdd_Click(object sender, RoutedEventArgs e)
        {
            smaterialList.Add(new StartingMaterial { Name = txtSMAddName.Text, CAS = txtSMAddCAS.Text, mValue=txtSMAddmValue.Text, VValue = txtSMAddVValue.Text });
        }

        private void btnRAdd_Click(object sender, RoutedEventArgs e)
        {
            reagentList.Add(new Reagent { Name = txtRAddName.Text, CAS = txtRAddCAS.Text, Ratio = txtRAddRatio.Text });
        }
    }
}
