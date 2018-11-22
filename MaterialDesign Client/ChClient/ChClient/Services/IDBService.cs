using ChClient.Models;
using InventoryReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.Services
{
    public interface IDBService
    {
        Task<List<string>> GetUsersAsync();
        Task<List<string>> GetProjectNamesAsync();
        Task<List<string>> GetReactionCodesAsync();
        Task<List<SelectedMolecule>> GetMoleculesAsync();

        Task AddUser(string name);

        Task AddProject(Project project);

        Task AddReaction(ReactionInfo reaction);

        Task<List<Project>> GetProjects();


        Task<string> InitMolecules();
        string AddMoleculesFromExcel(List<MoleculeData> molecules);

        void ResetAll();
    }
}
