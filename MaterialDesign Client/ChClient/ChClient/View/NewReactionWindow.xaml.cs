using ChClient.Model;
using ChClient.ViewModel;
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
using System.Windows.Shapes;

namespace ChClient.View
{
    /// <summary>
    /// Interaction logic for NewReactionWindow.xaml
    /// </summary>
    public partial class NewReactionWindow : Window
    {
        private NewReactionViewModel NewReactionVM;
        public NewReactionWindow()
        {
            InitializeComponent();
            NewReactionVM = new NewReactionViewModel();
            DataContext = NewReactionVM;
            StartDP.SelectedDate = DateTime.Now;
            ClosureDP.SelectedDate = DateTime.Now;
            


        }

        private void AddReactionImgBtn_Click(object sender, RoutedEventArgs e)
        {
            ReactionImgProblemTB.Text = "";
            ReactionImgImg.Visibility = Visibility.Collapsed;
            ReactionImgProblemTB.Visibility = Visibility.Collapsed;
            string tmp= NewReactionVM.AddReactionImg();
            try
            {
                ReactionImgImg.Source = new BitmapImage(new Uri(tmp));
                ReactionImgImg.Visibility = Visibility.Visible;
            }
            catch
            {
                ReactionImgProblemTB.Visibility = Visibility.Visible;
                ReactionImgProblemTB.Text = "Invalid file";
            }
            
        }
        private void AddObservationImgBtn_Click(object sender, RoutedEventArgs e)
        {
            ObservationImgProblemTB.Text = "";
            ObservationImgImg.Visibility = Visibility.Collapsed;
            ObservationImgProblemTB.Visibility = Visibility.Collapsed;
            string tmp = NewReactionVM.AddObservationImg();
            try
            {
                ObservationImgImg.Source = new BitmapImage(new Uri(tmp));
                ObservationImgImg.Visibility = Visibility.Visible;
            }
            catch
            {
                ObservationImgProblemTB.Text = "Invalid file";
                ObservationImgProblemTB.Visibility = Visibility.Visible;
            }
            
        }

        private void StartingMaterialAddBtn_Click(object sender, RoutedEventArgs e)
        {
            NewReactionVM.AddStartingMaterial(StartingMaterialAddNameTB.Text,
                StartingMaterialAddCASTB.Text,
                StartingMaterialAddmTB.Text,
                StartingMaterialAddVTB.Text);
        }

        private void ReagentAddBtn_Click(object sender, RoutedEventArgs e)
        {
            NewReactionVM.AddReagent(ReagentAddNameTB.Text,
                ReagentAddCASTB.Text,
                ReagentAddRatioTB.Text);
        }

        private void SolventAddBtn_Click(object sender, RoutedEventArgs e)
        {
            NewReactionVM.AddSolvent(SolventAddNameTB.Text,
                SolventAddCASTB.Text,
                SolventAddVTB.Text);
        }

        private void ProductAddBtn_Click(object sender, RoutedEventArgs e)
        {
            NewReactionVM.AddProduct(ProductAddMWTB.Text,
                ProductAddRatioTB.Text,
                ProductAddnTB.Text);
        }

        private void SaveLocationBtn_Click(object sender, RoutedEventArgs e)
        {
            string tmp = NewReactionVM.AddFileLocation();
            if (tmp.Substring(0, 6) == "Report")
            {
                DocxLocationProblemTB.Text = tmp;
                DocxLocationProblemTB.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                DocxLocationProblemTB.Text = tmp;
                DocxLocationProblemTB.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void SaveDocxBtn_Click(object sender, RoutedEventArgs e)
        {
            NewReactionVM.ThisReaction.Code = ReactionCodeTB.Text;            
            NewReactionVM.ThisReaction.Chemist = ChemistTB.Text;
            NewReactionVM.ThisReaction.Chiefchemist = ChiefchemistTB.Text;
            NewReactionVM.ThisReaction.Project = ProjectTB.Text;
            NewReactionVM.ThisReaction.Laboratory = LaboratoryTB.Text;
            NewReactionVM.ThisReaction.StartDate = StartDP.SelectedDate.ToString();
            NewReactionVM.ThisReaction.ClosureDate = ClosureDP.SelectedDate.ToString();
            NewReactionVM.ThisReaction.PreviousStep = PreviousTB.Text;
            NewReactionVM.ThisReaction.Literature = LiteratureTB.Text;

            NewReactionVM.ThisReaction.Procedure = ProcedureTB.Text;
            NewReactionVM.ThisReaction.Yield = YieldTB.Text;
            NewReactionVM.ThisReaction.Observation = ObservationTB.Text;

            DocxLocationProblemTB.Text = "";
            DocxSaveProblemTB.Text = "";

            string tmp = NewReactionVM.SaveDocx(false);

            if(tmp.Substring(0,4)== "Done")
            {
                DocxSaveProblemTB.Text = tmp;
                DocxLocationProblemTB.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                DocxSaveProblemTB.Text = tmp;
                DocxLocationProblemTB.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void NewReactionBtn_Click(object sender, RoutedEventArgs e)
        {
            NewReactionVM.DocxLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            NewReactionVM.ReactionImgPath = "";
            NewReactionVM.ObservationImgPath = "";

            if (Boolean.Parse(ClearHeaderCB.IsChecked.ToString()))
            {
                ReactionCodeTB.Text = "";
                ChemistTB.Text = "";
                ChiefchemistTB.Text = "";
                ProjectTB.Text = "";
                LaboratoryTB.Text = "";
                StartDP.SelectedDate = DateTime.Now;
                ClosureDP.SelectedDate = DateTime.Now;
                PreviousTB.Text = "";
                LiteratureTB.Text = "";

                ReactionImgProblemTB.Text = "";
                ReactionImgImg.Visibility = Visibility.Collapsed;
                ReactionImgProblemTB.Visibility = Visibility.Collapsed;

            }
            if (Boolean.Parse(ClearStartingMaterialCB.IsChecked.ToString()))
            {
                NewReactionVM.StartingMaterialList.Clear();
                if (Boolean.Parse(ClearStartingMaterialInputsCB.IsChecked.ToString()))
                {
                    StartingMaterialAddNameTB.Text = "";
                    StartingMaterialAddCASTB.Text = "";
                    StartingMaterialAddmTB.Text = "";
                    StartingMaterialAddVTB.Text = "";
                }
            }

            if (Boolean.Parse(ClearReagentCB.IsChecked.ToString()))
            {
                NewReactionVM.ReagentList.Clear();
                if (Boolean.Parse(ClearReagentInputsCB.IsChecked.ToString()))
                {
                    ReagentAddNameTB.Text = "";
                    ReagentAddCASTB.Text = "";
                    ReagentAddRatioTB.Text = "";
                }
            }

            if (Boolean.Parse(ClearSolventCB.IsChecked.ToString()))
            {
                NewReactionVM.SolventList.Clear();
                if (Boolean.Parse(ClearSolventInputsCB.IsChecked.ToString()))
                {
                    SolventAddNameTB.Text = "";
                    SolventAddCASTB.Text = "";
                    SolventAddVTB.Text = "";
                }
            }

            if (Boolean.Parse(ClearProductCB.IsChecked.ToString()))
            {
                NewReactionVM.ProductList.Clear();
                if (Boolean.Parse(ClearProductInputsCB.IsChecked.ToString()))
                {
                    ProductAddMWTB.Text = "";
                ProductAddRatioTB.Text = "";
                ProductAddnTB.Text = "";
                }
            }

            if (Boolean.Parse(ClearProcedureCB.IsChecked.ToString()))
            {
                ProcedureTB.Text = "";
                YieldTB.Text = "";
                ObservationTB.Text = "";
                ObservationImgProblemTB.Text = "";
                ObservationImgImg.Visibility = Visibility.Collapsed;
                ObservationImgProblemTB.Visibility = Visibility.Collapsed;
            }
            

        }

        
    }
}
