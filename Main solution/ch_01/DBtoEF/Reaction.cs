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
    
    public partial class Reaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reaction()
        {
            this.Reaction1 = new HashSet<Reaction>();
            this.Reagent = new HashSet<Reagent>();
            this.Solvent = new HashSet<Solvent>();
            this.StartingMaterial = new HashSet<StartingMaterial>();
        }
    
        public int ID { get; set; }
        public string ReactionCode { get; set; }
        public int ChemistID { get; set; }
        public int ChiefchemistID { get; set; }
        public int ProjectID { get; set; }
        public string Laboratory { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> ClosureDate { get; set; }
        public Nullable<int> PreviousStepID { get; set; }
        public string Literature { get; set; }
        public bool Sketch { get; set; }
        public byte[] ReactionImg { get; set; }
        public string ProcedureText { get; set; }
        public Nullable<double> Yield { get; set; }
        public string Observation { get; set; }
    
        public virtual Person Person { get; set; }
        public virtual Person Person1 { get; set; }
        public virtual Project Project { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reaction> Reaction1 { get; set; }
        public virtual Reaction Reaction2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reagent> Reagent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solvent> Solvent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StartingMaterial> StartingMaterial { get; set; }
    }
}