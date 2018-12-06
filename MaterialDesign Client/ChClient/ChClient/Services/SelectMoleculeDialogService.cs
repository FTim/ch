using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChClient.Models;
using ChClient.ViewModels;
using ChClient.Views;

namespace ChClient.Services
{
    public class SelectMoleculeDialogService : ISelectMoleculeDialogService
    {
        private IDBService _dbService;
        public SelectMoleculeDialogService(IDBService dbService)
        {
            _dbService = dbService;
            //ConfigureAsync();
        }
        public SelectedMolecule ShowMoleculeSelectWindow()
        {
            
            MaterialSelectWindow MoleculeSelect = new MaterialSelectWindow();
            MaterialSelectWindowViewModel dataContext=(MaterialSelectWindowViewModel)MoleculeSelect.DataContext;
            dataContext.SetAllMolecules(_allMolecules);
            MoleculeSelect.ShowDialog();
            //System.Windows.MessageBox.Show(res.ToString());

            //MoleculeSelect.DataContext
            SelectedMolecule tmp2 = dataContext.Selected;

            MoleculeSelect = null;
            dataContext = null;
            //SelectedMolecule tmp = new SelectedMolecule("name", "CAS", "there");
            return tmp2;
        }

        private List<SelectedMolecule> _allMolecules;
        public List<SelectedMolecule> FoundMolecules;

        public async void ConfigureAsync()
        {
            _allMolecules = new List<SelectedMolecule>();
            _allMolecules = await _dbService.GetMoleculesAsync();

            //_allMolecules.AddRange(_excelReaderService.GetMoleculesAsync());
        }
    }
}
