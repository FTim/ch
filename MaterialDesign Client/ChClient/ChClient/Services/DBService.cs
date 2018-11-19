using ChClient.Models;
using ChDbProject;
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

        public async Task AddReaction()
        {

        }

        public async Task AddProject(string name, string leader, string goal, string description, string planimg)
        {
            
            await DbAccess.AddProject(name, leader, goal, description, convertImg(planimg));
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
