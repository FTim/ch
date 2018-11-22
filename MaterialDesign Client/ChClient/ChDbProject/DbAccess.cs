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
            using (var db = new chdbContext())
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

            using (var db = new chdbContext())
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
            using (var db = new chdbContext())
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
                using (var db = new chdbContext())
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
                 using (var db = new chdbContext())
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
                using (var db = new chdbContext())
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
                using (var db = new chdbContext())
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
                using (var db = new chdbContext())
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

        public static async Task AddPerson(string name)
        {
            
            await Task.Run(() =>
            {
                using (var db = new chdbContext())
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
                using (var db = new chdbContext())
                {
                    //var plan = new ProjectPlan { img = projectDTO.PlanImg };

                    //Person Leader = new Person();
                    /*
                    entites.Termek
                    .Where(t => t.Raktarkeszlet > 0)
                    .OrderBy(t => t.Nev)
                    .Select(t => t.Nev)
                    .ToList()
                    .ForEach(n => Console.WriteLine(n));*/

                    //var Leader=db.People.Where(p => p.Name == projectDTO.Leader).First();


                    var project = projectDTO.TransformToProject();
                    
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
                using (var db = new chdbContext())
                {
                    var reaction = reactionDTO.TransformToReaction();

                    reaction.Chemist = db.People.Where(p => p.Name == reactionDTO.Chemist).First();
                    reaction.Chiefchemist = db.People.Where(p => p.Name == reactionDTO.Chiefchemist).First();
                    reaction.Project = db.Projects.Where(p => p.Name == reactionDTO.Project).First();
                    if (!string.IsNullOrEmpty(reactionDTO.PreviousStep))
                    {
                        reaction.PreviousStep = db.Reactions.Where(r => r.ReactionCode == reactionDTO.PreviousStep).First();
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
                }

            });
        }

       
        public static void Reset()
        {
            
            using (var db = new chdbContext())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM LocationMolecule");
                db.Database.ExecuteSqlCommand("DELETE FROM Location");
                db.Database.ExecuteSqlCommand("DELETE FROM MoleculeStatic");

                db.Database.ExecuteSqlCommand("DELETE FROM ObservationImg");

                db.Database.ExecuteSqlCommand("DELETE FROM ProjectPlan");
                db.Database.ExecuteSqlCommand("DELETE FROM Project");
                db.Database.ExecuteSqlCommand("DELETE FROM Person");

                db.Database.ExecuteSqlCommand("DELETE FROM Product");

                db.Database.ExecuteSqlCommand("DELETE FROM StartingMaterial");

                db.Database.ExecuteSqlCommand("DELETE FROM Solvent");

                db.Database.ExecuteSqlCommand("DELETE FROM Reagent");

                db.Database.ExecuteSqlCommand("DELETE FROM Reaction");

                

            }
            
        }
    }
}
