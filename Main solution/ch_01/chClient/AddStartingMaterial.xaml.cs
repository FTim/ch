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

namespace chClient
{
    /// <summary>
    /// Interaction logic for AddStartingMaterial.xaml
    /// </summary>
    /// 
    
    public partial class AddStartingMaterial : Window
    {
        private List<StartingMaterialInputData> sData;
        public AddStartingMaterial(List<StartingMaterialInputData> StartingMat)
        {
            sData = new List<StartingMaterialInputData>();
            sData.AddRange(StartingMat);
            InitializeComponent();
            
        }
        
private void AddStartingMAterialOKBtn_Click(object sender, RoutedEventArgs e)
        {
            double? tmp_m;
            double? tmp_V;

            if (StartingMaterialmInput.Text == null || StartingMaterialmInput.Text == "") tmp_m = null;
            else tmp_m = Double.Parse(StartingMaterialmInput.Text);

            if (StartingMaterialVInput.Text == null || StartingMaterialVInput.Text == "") tmp_V = null;
            else tmp_V = Double.Parse(StartingMaterialVInput.Text);

            sData.Add(new StartingMaterialInputData
            {
                Name = StartingMaterialNameInputTb.Text,
                CAS = StartingMaterialCASInputTb.Text,
                m = tmp_m,
                V = tmp_V
            });
            
        }
    }
}
