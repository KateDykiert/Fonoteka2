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
    
    public partial class Przynaleznosc
    {
        public int IdPrzynaleznosci { get; set; }
        public int IdPlaylisty { get; set; }
        public int IdUtworu { get; set; }
    
        public virtual Playlista Playlista { get; set; }
        public virtual Utwor Utwor { get; set; }
    }
}
