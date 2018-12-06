using ChDbProject.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChDbProject
{
    public class DbAccess
    {
        public static void AddLocation(string locationcode)
        {
            var newLocation = new Location() { Code = locationcode };
            using (var db = new ChContext())
            {
                var query = db.Locations.Where(loc => loc.Code == locationcode).Select(loc=> loc.Code).ToList();

                if (!query.Contains(locationcode))
                {
                    db.Locations.Add(newLocation);
                    db.SaveChanges();
                }
            }
        }
        public static void AddMoleculeStatic(string name, string cas, double m_gpermol, double? dparam, string mpparam, string bpparam, string purityparam)
        {

            var newMoleculeStatic = new MoleculeStatic()
            {
                Name = name,
                CAS = cas,
                M_gpermol=m_gpermol,
                d = dparam,
                mp = mpparam,
                bp = bpparam, 
                purity = purityparam
            };

            using (var db = new ChContext())
            {
                var query = db.MoleculeStatics.Where(molec => molec.CAS == cas).Select(molec => molec.CAS).ToList();

                if (!query.Contains(cas))
                {
                    db.MoleculeStatics.Add(newMoleculeStatic);
                    db.SaveChanges();
                }

            }

        }
        public static void ConnectLocationMoleculestatic(string cas, string location, double? m, double? V)
        {
            using (var db = new ChContext())
            {
                var thisMolecule = db.MoleculeStatics.Where(molec => molec.CAS == cas).FirstOrDefault();
                var thisLocation = db.Locations.Where(loc => loc.Code == location).FirstOrDefault();

                var thisLocationMolecule = new LocationMolecule();
                thisLocationMolecule.Location = thisLocation;
                thisLocationMolecule.MoleculeStatic = thisMolecule;

                if (m.HasValue)
                {
                    thisLocationMolecule.m = m.Value;
                }
                else
                {
                    if (V.HasValue)
                    {
                        thisLocationMolecule.v = V.Value;
                    }
                    else
                    {
                        
                        //throw new Exception("No m and V values in excel");
                    }
                }

                db.LocationMolecules.Add(thisLocationMolecule);

                db.SaveChanges();
            }
        }

       public static async Task<List<LocationMolecule>> GetMoleculesAsync()
        {
            List<LocationMolecule> result = new List<LocationMolecule>();

            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    result = db.LocationMolecules.ToList();

                    foreach (var item in result)
                    {
                        item.Location = db.Locations.Where(l => l.Code == item.LocationID).First();
                        item.MoleculeStatic = db.MoleculeStatics.Where(m => m.CAS == item.MoleculeCAS).First();
                    }
                }
                
            });

            return result;
        }



        public static async Task<List<string>> GetPeopleAsync()
        {
            List<string> result = new List<string>();
            await Task.Run(() =>
             {
                 using (var db = new ChContext())
                 {
                     var query = db.People.ToList();
                     

                     foreach (var item in query)
                     {
                         result.Add(item.Name);
                     }
                 }
                 //ui-t nem blokkol teszt
                 //System.Threading.Thread.Sleep(5000);
             });
            
            return result;
        }
        public static async Task<List<string>> GetProjectNamesAsync()
        {
            List<string> result = new List<string>();
            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    var query = db.Projects.ToList();


                    foreach (var item in query)
                    {
                        result.Add(item.Name);
                    }
                }
                //ui-t nem blokkol teszt
                //System.Threading.Thread.Sleep(5000);
            });

            return result;
        }
        public static async Task<List<string>> GetReactionCodesAsync()
        {
            List<string> result = new List<string>();
            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    var query = db.Reactions.ToList();


                    foreach (var item in query)
                    {
                        result.Add(item.ReactionCode);
                    }
                }
                //ui-t nem blokkol teszt
                //System.Threading.Thread.Sleep(5000);
            });

            return result;
        }

        public static async Task<List<Project>> GetProjectsAsync()
        {
            List<Project> result = new List<Project>();
            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    var query = db.Projects.Include(p=>p.ProjectPlans);


                    foreach (var item in query)
                    {
                        
                        item.Leader = db.People.Where(p => p.ID == item.LeaderID).First();
                        //item.ProjectPlans = db.ProjectPlans.Where(pl => pl.ProjectID == item.ID).ToList();
                        result.Add(item);
                    }
                }
                //ui-t nem blokkol teszt
                //System.Threading.Thread.Sleep(5000);
            });

            return result;
        }

        public static async Task<List<Project>> GetProjectsAsync(string leader)
        {
            List<Project> result = new List<Project>();
            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    var query = db.Projects.Include(p => p.ProjectPlans).Where(p=>p.Leader.Name==leader);


                    foreach (var item in query)
                    {

                        item.Leader = db.People.Where(p => p.ID == item.LeaderID).First();
                        //item.ProjectPlans = db.ProjectPlans.Where(pl => pl.ProjectID == item.ID).ToList();
                        result.Add(item);
                    }
                }
                //ui-t nem blokkol teszt
                //System.Threading.Thread.Sleep(5000);
            });

            return result;
        }

        public static async Task UpdateProject(int id, ProjectDTO projectDTO)
        {
            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    var updateproject = db.Projects.Where(p => p.ID == id).First();

                    updateproject.Name = projectDTO.Name;
                    updateproject.Goal = projectDTO.Goal;
                    updateproject.Description = projectDTO.Description;
                    updateproject.Leader = db.People.Where(p => p.Name == projectDTO.Leader).First();

                    var newplan = new ProjectPlan { img = projectDTO.PlanImg, Project = updateproject };

                    db.ProjectPlans.Add(newplan);

                    db.SaveChanges();
                    /*
                    var project = projectDTO.TransformToProject();
                    project.Leader = db.People.Where(p => p.Name == projectDTO.Leader).First();

                    var projectplan = new ProjectPlan { img = projectDTO.PlanImg, Project = project };

                    db.Projects.Add(project);
                    db.ProjectPlans.Add(projectplan);

                    db.SaveChanges();*/
                }

            });
        }

        public static async Task FinishSketchReaction(int id, ReactionDTO reaction)
        {
            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    var finishreaction = db.Reactions.Where(r => r.ID == id).First();

                    finishreaction.ClosureDate = reaction.ClosureDate;
                    finishreaction.ProcedureText = reaction.Procedure;
                    finishreaction.Yield = reaction.Yield;
                    finishreaction.Observation = reaction.Observation;
                    foreach (var item in reaction.ObservationImgs)
                    {
                        var tmp = new ObservationImg { img = item, Reaction = finishreaction };
                        db.ObservationImgs.Add(tmp);
                        finishreaction.ObservationImgs.Add(tmp);
                    }
                    finishreaction.Sketch = false;

                    db.SaveChanges();
                    
                }

            });
        }
        public static async Task<List<Reaction>> GetReactionsAsync(int projectID)
        {
            List<Reaction> result = new List<Reaction>();
            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    var query = db.Reactions.Where(p => p.ProjectID == projectID).ToList();

                    
                    foreach (var item in query)
                    {
                        item.Chemist = db.People.Where(p => p.ID == item.ChemistID).First();
                        item.Chiefchemist = db.People.Where(p => p.ID == item.ChiefchemistID).First();
                        result.Add(item);
                    }
                }
                
            });

            return result;
        }
        public static async Task<List<Reaction>> GetReactionsAsync(string chemist)
        {
            List<Reaction> result = new List<Reaction>();
            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    var query = db.Reactions.Where(r => r.Chemist.Name == chemist).ToList();

                    foreach (var item in query)
                    {
                        item.Chemist = db.People.Where(p => p.ID == item.ChemistID).First();
                        item.Chiefchemist = db.People.Where(p => p.ID == item.ChiefchemistID).First();
                        result.Add(item);
                    }
                }

            });

            return result;
        }
        public static async Task<List<Reaction>> GetReactionsAsync()
        {
            List<Reaction> result = new List<Reaction>();
            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    var query = db.Reactions.ToList();

                    foreach (var item in query)
                    {
                        item.Chemist = db.People.Where(p => p.ID == item.ChemistID).First();
                        item.Chiefchemist = db.People.Where(p => p.ID == item.ChiefchemistID).First();
                        result.Add(item);
                    }
                }

            });

            return result;
        }
        public static async Task<ReactionDTO> GetReactionAsync(int reactionId)
        {
            ReactionDTO result = new ReactionDTO();

            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    var query = db.Reactions.Where(r=> r.ID==reactionId).First();

                    result = new ReactionDTO() { Code = query.ReactionCode, Chemist = query.Chemist.Name, Chiefchemist = query.Chiefchemist.Name, Project = query.Project.Name, Laboratory = query.Laboratory, StartDate = query.StartDate, Observation = query.Observation, Procedure = query.ProcedureText, Yield = query.Yield, IsSketch=query.Sketch.Value, ReactionImg=query.ReactionImg };
                    if (query.ClosureDate.HasValue) result.ClosureDate = query.ClosureDate.Value;
                    if (query.PreviousStep != null) result.PreviousStep = query.PreviousStep.ReactionCode;
                }

            });

            return result;

        }

        public static async Task<StartingMaterialDTO> GetStartingMaterialAsync(int reactionId)
        {
            StartingMaterialDTO result = new StartingMaterialDTO();

            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    var query = db.StartingMaterials.Where(r => r.ReactionID == reactionId).Include(m=> m.MoleculeStatic).First();

                    result.Name = query.MoleculeStatic.Name;
                    result.MoleculeCAS = query.MoleculeCAS;
                    if (query.m.HasValue) result.mValue = query.m.Value;
                    if (query.v.HasValue) result.VValue = query.v.Value;

                    result.mp = query.MoleculeStatic.mp;
                    result.bp = query.MoleculeStatic.bp;
                    result.den = query.MoleculeStatic.d;
                }

            });

            return result;

        }
        public static async Task<List<ReagentDTO>> GetReagentsAsync(int reactionId)
        {
            List<ReagentDTO> result = new List<ReagentDTO>();

            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    var query = db.Reagents.Where(r => r.ReactionID == reactionId).ToList();

                    foreach (var item in query)
                    {
                        ReagentDTO tmp = new ReagentDTO() { MoleculeCAS = item.MoleculeCAS, Ratio = item.Ratio, Name = item.MoleculeStatic.Name, den=item.MoleculeStatic.d, bp=item.MoleculeStatic.bp, mp=item.MoleculeStatic.mp };
                        result.Add(tmp);
                    }

                    
                }

            });

            return result;

        }

        public static async Task<List<SolventDTO>> GetSolventsAsync(int reactionId)
        {
            List<SolventDTO> result = new List<SolventDTO>();

            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    var query = db.Solvents.Where(r => r.ReactionID == reactionId).ToList();

                    foreach (var item in query)
                    {
                        SolventDTO tmp = new SolventDTO() { MoleculeCAS = item.MoleculeCAS, VValue=item.v, Name = item.MoleculeStatic.Name, bp=item.MoleculeStatic.bp, mp=item.MoleculeStatic.mp};
                        result.Add(tmp);
                    }


                }

            });

            return result;

        }

        public static async Task<List<ProductDTO>> GetProductsAsync(int reactionId)
        {
            List<ProductDTO> result = new List<ProductDTO>();

            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    var query = db.Products.Where(r => r.ReactionID == reactionId).ToList();

                    foreach (var item in query)
                    {
                        ProductDTO tmp = new ProductDTO() { MW=item.MW, Ratio=item.Ratio };
                        result.Add(tmp);
                    }


                }

            });

            return result;

        }

        public static async Task<List<byte[]>> GetObsImgByteArraysAsync(int reactionId)
        {
            List<byte[]> result = new List<byte[]>();

            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    var query = db.ObservationImgs.Where(r => r.ReactionID == reactionId).ToList();

                    foreach (var item in query)
                    {
                        
                        result.Add(item.img);
                    }


                }

            });

            return result;

        }



        public static async Task AddPerson(string name)
        {
            
            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    var person = new Person { Name = name };
                    db.People.Add(person);
                    db.SaveChanges();

                }
                
            });

            
        }

        public static async Task AddProject(ProjectDTO projectDTO)
        {

            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    

                    var project = projectDTO.TransformToProject();
                    project.Leader = db.People.Where(p => p.Name == projectDTO.Leader).First();

                    var projectplan = new ProjectPlan { img = projectDTO.PlanImg, Project=project };

                    db.Projects.Add(project);
                    db.ProjectPlans.Add(projectplan);

                    db.SaveChanges();
                }

            });


        }

        public static async Task AddReaction(ReactionDTO reactionDTO)
        {
            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    var reaction = reactionDTO.TransformToReaction();

                    reaction.Chemist = db.People.Where(p => p.Name == reactionDTO.Chemist).First();
                    reaction.Chiefchemist = db.People.Where(p => p.Name == reactionDTO.Chiefchemist).First();
                    reaction.Project = db.Projects.Where(p => p.Name == reactionDTO.Project).First();
                    if (!string.IsNullOrEmpty(reactionDTO.PreviousStep))
                    {
                        reaction.PreviousStep = db.Reactions.Where(re => re.ReactionCode == reactionDTO.PreviousStep).First();
                    }
                    if (!reaction.Sketch.Value)
                    {
                        foreach (var item in reactionDTO.ObservationImgs)
                        {
                            var tmp = new ObservationImg { img = item, Reaction = reaction };
                            db.ObservationImgs.Add(tmp);
                            reaction.ObservationImgs.Add(tmp);
                        }
                        
                    }

                    var tmpsm = reactionDTO.StartingMaterial.TransformToStartingMaterial();
                    tmpsm.Reaction = reaction;
                    db.StartingMaterials.Add(tmpsm);
                    reaction.StartingMaterials.Add(tmpsm);

                    

                    foreach (var item in reactionDTO.Reagents)
                    {
                        var tmp = item.TransformToReagent();
                        tmp.Reaction = reaction;

                        db.Reagents.Add(tmp);
                        reaction.Reagents.Add(tmp);
                    }

                    foreach (var item in reactionDTO.Solvents)
                    {
                        var tmp = item.TransformToSolvent();
                        tmp.Reaction = reaction;

                        db.Solvents.Add(tmp);
                        reaction.Solvents.Add(tmp);
                    }

                    foreach (var item in reactionDTO.Products)
                    {
                        var tmp = item.TransformToProduct();
                        tmp.Reaction = reaction;

                        db.Products.Add(tmp);
                        reaction.Products.Add(tmp);
                    }
                    db.Reactions.Add(reaction);

                    db.SaveChanges();

                    //var sm1 = db.LocationMolecules.Where(m => m.MoleculeCAS == reactionDTO.StartingMaterial.MoleculeCAS && m.Location.Code == reactionDTO.StartingMaterial.Location).ToList();
                    var sm = db.LocationMolecules.Where(m => m.MoleculeCAS == reactionDTO.StartingMaterial.MoleculeCAS && m.Location.Code== reactionDTO.StartingMaterial.Location).First();
                    if (sm.v.HasValue)
                    {
                        sm.v = sm.v -reactionDTO.StartingMaterial.VValue;
                    }
                    else
                    {
                        sm.m = sm.m - reactionDTO.StartingMaterial.mValue;
                    }
                    db.SaveChanges();

                    LocationMolecule r = new LocationMolecule();
                    //LocationMolecule r2 = new LocationMolecule();
                    foreach (var item in reactionDTO.Reagents)
                    {
                        //var r2 = db.LocationMolecules.Where(m => m.MoleculeCAS == item.MoleculeCAS && m.Location.Code == item.Location).ToList();
                        r = db.LocationMolecules.Where(m => m.MoleculeCAS == item.MoleculeCAS && m.Location.Code == item.Location).First();
                        if (r.v.HasValue)
                        {
                            r.v = r.v - item.VValue;
                        }
                        else
                        {
                            r.m = r.m - item.mValue;
                        }
                        db.SaveChanges();
                    }

                    LocationMolecule s = new LocationMolecule();

                    foreach (var item in reactionDTO.Solvents)
                    {
                        //var s2 = db.LocationMolecules.Where(m => m.MoleculeCAS == item.MoleculeCAS && m.Location.Code == item.Location).ToList();
                        s = db.LocationMolecules.Where(m => m.MoleculeCAS == item.MoleculeCAS && m.Location.Code == item.Location).First();
                       
                           s.v = s.v - item.VValue;
                       
                        db.SaveChanges();
                    }

                }

            });
        }

        public static async Task DeleteProject(int projectID)
        {
            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    var project = db.Projects.Where(p => p.ID == projectID).First();

                    List<ProjectPlan> plans = new List<ProjectPlan>();

                    List<Reaction> reactions = new List<Reaction>();
                    StartingMaterial sm = new StartingMaterial();
                    List<Reagent> reagents = new List<Reagent>();
                    List<Solvent> solvents = new List<Solvent>();
                    List<Product> products = new List<Product>();
                    List<ObservationImg> observationImgs = new List<ObservationImg>();

                    plans = db.ProjectPlans.Where(pl => pl.ProjectID == projectID).ToList();
                    db.ProjectPlans.RemoveRange(plans);

                    reactions = db.Reactions.Where(r => r.ProjectID == projectID).ToList();
                foreach (var item in reactions)
                {
                    if (item.NextStep.Count != 0) throw new Exception("One or more reaction cannot be deleted because used as previous step for another reaction!");
                    }
                    foreach (var item in reactions)
                    {
                        sm = db.StartingMaterials.Where(stm => stm.ReactionID == item.ID).First();
                        db.StartingMaterials.Remove(sm);

                        reagents = db.Reagents.Where(r => r.ReactionID == item.ID).ToList();
                        db.Reagents.RemoveRange(reagents);

                        solvents = db.Solvents.Where(s => s.ReactionID == item.ID).ToList();
                        db.Solvents.RemoveRange(solvents);

                        products = db.Products.Where(pr => pr.ReactionID == item.ID).ToList();
                        db.Products.RemoveRange(products);

                        observationImgs = db.ObservationImgs.Where(obs => obs.ReactionID == item.ID).ToList();
                        db.ObservationImgs.RemoveRange(observationImgs);

                        db.SaveChanges();

                    }

                    db.Reactions.RemoveRange(reactions);
                    db.Projects.Remove(project);

                    db.SaveChanges();


                }

            });
         }

        public static async Task DeleteReaction(int reactionID)
        {
            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    var reaction = db.Reactions.Where(r => r.ID == reactionID).First();
                    if(reaction.NextStep.Count!=0) throw new Exception("One or more reaction cannot be deleted because used as previous step for another reaction!");
                    StartingMaterial sm = new StartingMaterial();
                    List<Reagent> reagents = new List<Reagent>();
                    List<Solvent> solvents = new List<Solvent>();
                    List<Product> products = new List<Product>();
                    List<ObservationImg> observationImgs = new List<ObservationImg>();

                        sm = db.StartingMaterials.Where(stm => stm.ReactionID == reactionID).First();
                        db.StartingMaterials.Remove(sm);

                        reagents = db.Reagents.Where(r => r.ReactionID == reactionID).ToList();
                        db.Reagents.RemoveRange(reagents);

                        solvents = db.Solvents.Where(s => s.ReactionID == reactionID).ToList();
                        db.Solvents.RemoveRange(solvents);

                        products = db.Products.Where(pr => pr.ReactionID == reactionID).ToList();
                        db.Products.RemoveRange(products);

                        observationImgs = db.ObservationImgs.Where(obs => obs.ReactionID == reactionID).ToList();
                        db.ObservationImgs.RemoveRange(observationImgs);

                    db.Reactions.Remove(reaction);

                    db.SaveChanges();


                }

            });
        }

        public static async Task ModifyMoleculeAvailable(string CAS, string location, double? mValue, double? vValue)
        {
            await Task.Run(() =>
            {
                using (var db = new ChContext())
                {
                    var molecule = db.LocationMolecules.Where(m => m.MoleculeCAS == CAS && m.Location.Code == location).First();

                    if (mValue.HasValue)
                    {
                        molecule.m = mValue;
                    }
                    else
                    {
                        molecule.v = vValue;
                    }
                    db.SaveChanges();
                }
            });

        }


        public static void Reset()
        {
            
            using (var db = new ChContext())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM Product");

                db.Database.ExecuteSqlCommand("DELETE FROM StartingMaterial");

                db.Database.ExecuteSqlCommand("DELETE FROM Solvent");

                db.Database.ExecuteSqlCommand("DELETE FROM Reagent");

                db.Database.ExecuteSqlCommand("DELETE FROM LocationMolecule");
                db.Database.ExecuteSqlCommand("DELETE FROM Location");
                db.Database.ExecuteSqlCommand("DELETE FROM MoleculeStatic");

                db.Database.ExecuteSqlCommand("DELETE FROM ObservationImg");
                db.Database.ExecuteSqlCommand("DELETE FROM Reaction");
                db.Database.ExecuteSqlCommand("DELETE FROM ProjectPlan");
                db.Database.ExecuteSqlCommand("DELETE FROM Project");
                db.Database.ExecuteSqlCommand("DELETE FROM Person");

            }
            
        }
    }
}
