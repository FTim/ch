using System;
using System.Collections.Generic;
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
            using (var db = new ChDbContext())
            {
                //newLocation.Code = locationcode;
                db.Locations.Add(newLocation);
                db.SaveChanges();
            }
        }
        public static void AddMoleculeStatic(string name, string cas, double m_gpermol, double dparam, double mpparam, double dpparam, double purityparam)
        {
            var newMoleculeStatic = new MoleculeStatic()
            {
                Name = name,
                CAS = cas,
                d = dparam,
                mp = mpparam,
                dp = dpparam, //dp lenni bp, TODO javítani
                purity = purityparam
            };

            using (var db = new ChDbContext())
            {
                //newLocation.Code = locationcode;
                db.MoleculeStatics.Add(newMoleculeStatic);
                db.SaveChanges();
            }

        }
    }
}
