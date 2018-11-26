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
        Task<List<ReactionInfo>> GetReactions(int projectID);
        Task<List<ReactionInfo>> GetReactions(string chemist);
        Task<List<ReactionInfo>> GetReactions();

        Task<ReactionInfo> GetReactionAsync(int reactionId);
        Task<StartingMaterial> GetStartingMaterial(int reactionId);
        Task<List<Reagent>> GetReagents(int reactionId);
        Task<List<Solvent>> GetSolvents(int reactionId);
        Task<List<Product>> GetProducts(int reactionId);
        Task<List<byte[]>> GetObsImgs(int reactionId);
        

        Task AddUser(string name);

        Task AddProject(Project project);

        Task AddReaction(ReactionInfo reaction);

        Task<List<Project>> GetProjects();
        Task<List<Project>> GetProjects(string leader);
        Task DeleteProjct(int id);
        Task DeleteReaction(int id);

        Task<string> InitMolecules();
        string AddMoleculesFromExcel(List<MoleculeData> molecules);
        Task ModifyMoleculeAvailable(string CAS, string location, double? mValue, double? vValue);

        void ResetAll();
    }
}
