//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fonoteka2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Instrument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Instrument()
        {
            this.Umiejetnosc = new HashSet<Umiejetnosc>();
        }
    
        public int IdInstrumentu { get; set; }
        public string Nazwa { get; set; }
        public string Rodzaj { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Umiejetnosc> Umiejetnosc { get; set; }
    }
}
