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
    
    public partial class Utwor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Utwor()
        {
            this.Przynaleznosc = new HashSet<Przynaleznosc>();
        }
    
        public int IdUtworu { get; set; }
        public int IdZespolu { get; set; }
        public int IdAlbumu { get; set; }
        public int IdGatunku { get; set; }
        public string Tytul { get; set; }
        public System.TimeSpan CzasTrwania { get; set; }
    
        public virtual Album Album { get; set; }
        public virtual Gatunek Gatunek { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Przynaleznosc> Przynaleznosc { get; set; }
        public virtual Zespol Zespol { get; set; }
    }
}
