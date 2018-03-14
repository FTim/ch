//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBtoEF
{
    using System;
    using System.Collections.Generic;
    
    public partial class MoleculeStatic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MoleculeStatic()
        {
            this.LocationMolecule = new HashSet<LocationMolecule>();
            this.Reagent = new HashSet<Reagent>();
            this.Solvent = new HashSet<Solvent>();
            this.StartingMaterial = new HashSet<StartingMaterial>();
        }
    
        public string Name { get; set; }
        public string CAS { get; set; }
        public double M_gpermol { get; set; }
        public double d { get; set; }
        public double mp { get; set; }
        public double dp { get; set; }
        public double purity { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocationMolecule> LocationMolecule { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reagent> Reagent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solvent> Solvent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StartingMaterial> StartingMaterial { get; set; }
    }
}