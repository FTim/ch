using ChClient.Models;
using ChDbProject;
using ChDbProject.DTOs;
using InventoryReader;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ChClient.Services
{
    public class DBService : IDBService
    {
        private IExcelReaderService _excelReaderService;
        public DBService(IExcelReaderService exceldatareader)
        {
            _excelReaderService = exceldatareader;
        }


        public async Task<string> InitMolecules()
        {
            string result =
                await Task.Run(() =>
                {
                    List<MoleculeData> molecules = new List<MoleculeData>();
                    molecules = _excelReaderService.GetMoleculesFromInventoryFile();
                    string res = AddMoleculesFromExcel(molecules);
                    return res;
                });
            return result;
        }

        public string AddMoleculesFromExcel(List<MoleculeData> molecules)
        {
            
           
                string result = "";
                //ui-t nem blokkol teszt
                //System.Threading.Thread.Sleep(5000);

                try
                {
                    foreach (var item in molecules)
                    {
                    if (!string.IsNullOrEmpty(item.Name))
                    {
                        try
                        {
                            DbAccess.AddLocation(item.Location);
                            DbAccess.AddMoleculeStatic(item.Name, item.CAS, item.Mvalue, item.dvalue, item.mpvalue, item.bpvalue, item.purity);
                            DbAccess.ConnectLocationMoleculestatic(item.CAS, item.Location, item.mvalue, item.Vvalue);
                        }
                        catch
                        {
                            throw new Exception(item.Name);
                        }
                       
                    }
                    }

                    result = "Done";
                }
                catch(Exception e)
                {
                    result = e.Message;
                }


                return result;
           
            
        }

        public async Task AddReaction(ReactionInfo reaction)
        {
            ReactionDTO reactionDTO = new ReactionDTO
            {
                Code = reaction.Code,
                Chemist = reaction.Chemist,
                Chiefchemist = reaction.Chiefchemist,
                Project = reaction.Project,
                Laboratory = reaction.Laboratory,
                StartDate = reaction.StartDate,
                ClosureDate = reaction.ClosureDate,
                PreviousStep = reaction.PreviousStep,
                Literature = reaction.Literature,
                IsSketch = reaction.IsSketch,
                ReactionImg = convertImg(reaction.ReactionImgPath),
                Procedure = reaction.Procedure,
                Yield = reaction.Yield,
                Observation = reaction.Observation
            };

            StartingMaterialDTO smDTO = new StartingMaterialDTO { MoleculeCAS = reaction.StartingMaterial.CAS, ReactionName = reaction.Code, mValue = reaction.StartingMaterial.mValue, nValue = reaction.StartingMaterial.nValue, VValue = reaction.StartingMaterial.VValue };

            List<ReagentDTO> reagentDTOs = new List<ReagentDTO>();
            foreach (var item in reaction.Reagents)
            {
                reagentDTOs.Add(new ReagentDTO { MoleculeCAS = item.CAS, ReactionName = reaction.Code, Ratio = item.Ratio });
            }
            List<SolventDTO> solventDTOs = new List<SolventDTO>();
            foreach (var item in reaction.Solvents)
            {
                solventDTOs.Add(new SolventDTO { MoleculeCAS = item.CAS, ReactionName = reaction.Code, VValue = item.VValue });
            }
            List<ProductDTO> productDTOs = new List<ProductDTO>();
            foreach (var item in reaction.Products)
            {
                productDTOs.Add(new ProductDTO { ReactionName = reaction.Code, MW = item.mValue, Ratio = item.Ratio });
            }
            List<byte[]> observationImgs = new List<byte[]>();
            foreach (var item in reaction.ObservationImgPaths)
            {
                observationImgs.Add(convertImg(item));
            }

            reactionDTO.StartingMaterial = smDTO;
            reactionDTO.Reagents = reagentDTOs;
            reactionDTO.Solvents = solventDTOs;
            reactionDTO.Products = productDTOs;

            reactionDTO.ObservationImgs = observationImgs;


            await DbAccess.AddReaction(reactionDTO);
        }

        public async Task AddProject(Models.Project project)
        {
            //ProjectDTO projectDTO = new ProjectDTO();
            await DbAccess.AddProject(new ProjectDTO { Name = project.Name, Leader = project.Leader, Goal = project.Goal, Description = project.Description , PlanImg = convertImg(project.CurrentPlan) });
        }

        public async Task AddUser(string name)
        {
            await DbAccess.AddPerson(name);
        }

        public async Task<List<Models.Project>> GetProjects()
        {
            var projects = await DbAccess.GetProjectsAsync();

            List<Models.Project> result = new List<Models.Project>();

            foreach (var item in projects)
            {
                Models.Project tmp = new Models.Project();

                tmp.Name = item.Name;
                tmp.Leader = item.Leader.Name;
                tmp.Goal = item.Goal;
                tmp.Description = item.Description;
                //List<string> tmpplans = new List<string>();
                foreach (var planitem in item.ProjectPlans)
                {
                    tmp.ProjectPlanByreArrays.Add(planitem.img);
                }
                tmp.LastPlan = item.ProjectPlans.Last().img;
                //tmp.ProjectPlanImgPaths = tmpplans;

                result.Add(tmp);
            }

            return result;
        }

        public async Task<List<string>> GetUsersAsync()
        {
            List<string> result = new List<string>();
            result = await DbAccess.GetPeopleAsync();
            return result;
        }

        public async Task<List<string>> GetProjectNamesAsync()
        {
            List<string> result = new List<string>();
            result = await DbAccess.GetProjectNamesAsync();
            return result;
        }

        public async Task<List<string>> GetReactionCodesAsync()
        {
            List<string> result = new List<string>();
            result = await DbAccess.GetReactionCodesAsync();
            return result;
        }

        public async Task<List<SelectedMolecule>> GetMoleculesAsync()
        {
            List<SelectedMolecule> result = new List<SelectedMolecule>();

            var locationmolecules = await DbAccess.GetMoleculesAsync();

            foreach (var item in locationmolecules)
            {
                
                result.Add(new SelectedMolecule { Name = item.MoleculeStatic.Name, CAS = item.MoleculeCAS, Location = item.Location.Code, mAvailable = item.m, VAvailable = item.v, MW = item.MoleculeStatic.M_gpermol, Den = item.MoleculeStatic.d, mpValue = item.MoleculeStatic.mp, bpValue = item.MoleculeStatic.bp });
            }


            return result;
        }

        public void ResetAll()
        {
            DbAccess.Reset();
        }

        private byte[] convertImg(string path)
        {
            byte[] imgToByte = File.ReadAllBytes(path);
            return imgToByte;
        }

        

        
    }
}
