//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChDbProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class ObservationImg
    {
        public int ReactionID { get; set; }
        public int ID { get; set; }
        public byte[] img { get; set; }
    
        public virtual Reaction Reaction { get; set; }
    }
}