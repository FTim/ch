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

        public async Task<string> AddMolecule(SelectedMolecule molecule)
        {
            
            string result = "";
            try
            {
                await Task.Run(() =>
                  {
                      
                          DbAccess.AddLocation(molecule.Location);

                          DbAccess.AddMoleculeStatic(molecule.Name, molecule.CAS, molecule.MW, molecule.Den, molecule.mpValue, molecule.bpValue, molecule.Purity);

                          DbAccess.ConnectLocationMoleculestatic(molecule.CAS, molecule.Location, molecule.mAvailable, molecule.VAvailable);
                          result = "Done";
                      
                      
                  });
            } catch(Exception e)
            {
                throw e;
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

            StartingMaterialDTO smDTO = new StartingMaterialDTO { MoleculeCAS = reaction.StartingMaterial.CAS, ReactionName = reaction.Code, mValue = reaction.StartingMaterial.mValue, nValue = reaction.StartingMaterial.nValue, VValue = reaction.StartingMaterial.VValue , Location=reaction.StartingMaterial.Location};

            List<ReagentDTO> reagentDTOs = new List<ReagentDTO>();
            foreach (var item in reaction.Reagents)
            {
                reagentDTOs.Add(new ReagentDTO { MoleculeCAS = item.CAS, ReactionName = reaction.Code, Ratio = item.Ratio , Location= item.Location, mValue=item.mValue, VValue=item.VValue});
            }
            List<SolventDTO> solventDTOs = new List<SolventDTO>();
            foreach (var item in reaction.Solvents)
            {
                solventDTOs.Add(new SolventDTO { MoleculeCAS = item.CAS, ReactionName = reaction.Code, VValue = item.VValue, Location=item.Location });
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

                tmp.ProjectID = item.ID;
                tmp.Name = item.Name;
                tmp.Leader = item.Leader.Name;
                tmp.Goal = item.Goal;
                tmp.Description = item.Description;
                //List<string> tmpplans = new List<string>();
                foreach (var planitem in item.ProjectPlans)
                {
                    tmp.ProjectPlanByreArrays.Add(planitem.img);
                }
                item.ProjectPlans.OrderBy(pl => pl.ID);
                tmp.LastPlan = item.ProjectPlans.Last().img;
                //tmp.ProjectPlanImgPaths = tmpplans;

                result.Add(tmp);
            }

            return result;
        }

        public async Task<List<Models.Project>> GetProjects(string leader)
        {
            var projects = await DbAccess.GetProjectsAsync(leader);

            List<Models.Project> result = new List<Models.Project>();

            foreach (var item in projects)
            {
                Models.Project tmp = new Models.Project();

                tmp.ProjectID = item.ID;
                tmp.Name = item.Name;
                tmp.Leader = item.Leader.Name;
                tmp.Goal = item.Goal;
                tmp.Description = item.Description;
                //List<string> tmpplans = new List<string>();
                foreach (var planitem in item.ProjectPlans)
                {
                    tmp.ProjectPlanByreArrays.Add(planitem.img);
                }
                item.ProjectPlans.OrderBy(pl => pl.ID);
                tmp.LastPlan = item.ProjectPlans.Last().img;
                //tmp.ProjectPlanImgPaths = tmpplans;

                result.Add(tmp);
            }

            return result;
        }

        public async Task UpdateProject(int id, string name, string leader, string goal, string description, string newplanpath)
        {
            await DbAccess.UpdateProject(id, new ProjectDTO { Name = name, Leader = leader, Goal = goal, Description = description, PlanImg = convertImg(newplanpath) });
        }

        public async Task<List<ReactionInfo>> GetReactions(int projectID)
        {
            var reactions = await DbAccess.GetReactionsAsync(projectID);

            List<ReactionInfo> result = new List<ReactionInfo>();

            foreach (var item in reactions)
            {
                ReactionInfo tmp = new ReactionInfo();
                tmp.ReactionID = item.ID;
                tmp.Code = item.ReactionCode;
                tmp.Chemist = item.Chemist.Name;
                tmp.Chiefchemist = item.Chiefchemist.Name;
                tmp.Laboratory = item.Laboratory;
                tmp.StartDate = item.StartDate;
                tmp.IsSketch = item.Sketch.Value;
                

                result.Add(tmp);
            }

            return result;
        }

        public async Task<List<ReactionInfo>> GetReactions()
        {
            var reactions = await DbAccess.GetReactionsAsync();

            List<ReactionInfo> result = new List<ReactionInfo>();

            foreach (var item in reactions)
            {
                ReactionInfo tmp = new ReactionInfo();
                tmp.ReactionID = item.ID;
                tmp.Code = item.ReactionCode;
                tmp.Chemist = item.Chemist.Name;
                tmp.Chiefchemist = item.Chiefchemist.Name;
                tmp.Laboratory = item.Laboratory;
                tmp.StartDate = item.StartDate;
                tmp.IsSketch = item.Sketch.Value;


                result.Add(tmp);
            }

            return result;
        }
        public async Task<List<ReactionInfo>> GetReactions(string chemist)
        {
            var reactions = await DbAccess.GetReactionsAsync(chemist);

            List<ReactionInfo> result = new List<ReactionInfo>();

            foreach (var item in reactions)
            {
                ReactionInfo tmp = new ReactionInfo();
                tmp.ReactionID = item.ID;
                tmp.Code = item.ReactionCode;
                tmp.Chemist = item.Chemist.Name;
                tmp.Chiefchemist = item.Chiefchemist.Name;
                tmp.Laboratory = item.Laboratory;
                tmp.StartDate = item.StartDate;
                tmp.IsSketch = item.Sketch.Value;


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
                
                result.Add(new SelectedMolecule { Name = item.MoleculeStatic.Name, CAS = item.MoleculeCAS, Location = item.Location.Code, mAvailable = item.m, VAvailable = item.v, MW = item.MoleculeStatic.M_gpermol, Den = item.MoleculeStatic.d, mpValue = item.MoleculeStatic.mp, bpValue = item.MoleculeStatic.bp, Purity=item.MoleculeStatic.purity });
            }


            return result;
        }

        public async Task DeleteProjct(int id)
        {
            await DbAccess.DeleteProject(id);
        }
        public async Task DeleteReaction(int id)
        {
            await DbAccess.DeleteReaction(id);
        }
        public async Task ModifyMoleculeAvailable(string CAS, string location, double? mValue, double? vValue)
        {
            await DbAccess.ModifyMoleculeAvailable(CAS, location, mValue, vValue);
        }

        public async Task FinishSketchReaction(int id, DateTime closuredate, string procedure, string observation, string yield, List<string> observationimgpaths)
        {
            List<byte[]> tmp = new List<byte[]>();
            foreach (var item in observationimgpaths)
            {
                tmp.Add(convertImg(item));
            }
            await DbAccess.FinishSketchReaction(id, new ReactionDTO { ClosureDate = closuredate, Procedure = procedure, Observation = observation, Yield = yield, ObservationImgs = tmp });
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

        public async Task<ReactionInfo> GetReactionAsync(int reactionId)
        {
            var tmp = await DbAccess.GetReactionAsync(reactionId);
            ReactionInfo result = new ReactionInfo(){ Code = tmp.Code, Chemist = tmp.Chemist, Chiefchemist = tmp.Chiefchemist, Project = tmp.Project, Laboratory = tmp.Laboratory, StartDate = tmp.StartDate, PreviousStep = tmp.PreviousStep, Observation = tmp.Observation, Procedure = tmp.Procedure, Yield = tmp.Yield, IsSketch = tmp.IsSketch, Literature = tmp.Literature, ReactionImgByteArray = tmp.ReactionImg };

            if(!tmp.IsSketch) result.ClosureDate = tmp.ClosureDate;


            return result;
        }

        public async Task<Models.StartingMaterial> GetStartingMaterial(int reactionId)
        {
            var tmp = await DbAccess.GetStartingMaterialAsync(reactionId);
            Models.StartingMaterial result = new Models.StartingMaterial();

            result.CAS = tmp.MoleculeCAS;
            
            result.Name = tmp.Name;
            if (tmp.mValue.HasValue) result.mValue = tmp.mValue.Value;
            if (tmp.VValue.HasValue) result.VValue = tmp.VValue.Value;
            
            result.mpValue = tmp.mp;
            result.bpValue = tmp.bp;
            if(tmp.den.HasValue) result.Den = tmp.den.Value;

            return result;
        }

        public async Task<List<Models.Reagent>> GetReagents(int reactionId)
        {
            var tmp = await DbAccess.GetReagentsAsync(reactionId);
            List<Models.Reagent> result = new List<Models.Reagent>();

            foreach (var item in tmp)
            {
                Models.Reagent tmpr = new Models.Reagent() { CAS = item.MoleculeCAS, Ratio = item.Ratio, Name = item.Name, mpValue=item.mp, bpValue=item.bp, Den=item.den };
                result.Add(tmpr);
            }

            

            return result;
        }

        public async Task<List<Models.Solvent>> GetSolvents(int reactionId)
        {
            var tmp = await DbAccess.GetSolventsAsync(reactionId);
            List<Models.Solvent> result = new List<Models.Solvent>();

            foreach (var item in tmp)
            {
                Models.Solvent tmpr = new Models.Solvent() { CAS = item.MoleculeCAS, VValue=item.VValue, Name = item.Name, mpValue=item.mp, bpValue=item.bp };
                result.Add(tmpr);
            }



            return result;
        }

        public async Task<List<Models.Product>> GetProducts(int reactionId)
        {
            var tmp = await DbAccess.GetProductsAsync(reactionId);
            List<Models.Product> result = new List<Models.Product>();

            foreach (var item in tmp)
            {
                Models.Product tmpr = new Models.Product() { MW=item.MW, Ratio=item.Ratio};
                result.Add(tmpr);
            }



            return result;
        }

        public async Task<List<byte[]>> GetObsImgs(int reactionId)
        {
            var result = await DbAccess.GetObsImgByteArraysAsync(reactionId);
            
            return result;
        }
    }
}
