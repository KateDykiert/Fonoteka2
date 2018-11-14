﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FonotekaDBEntities3 : DbContext
    {
        public FonotekaDBEntities3()
            : base("name=FonotekaDBEntities3")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Album> Album { get; set; }
        public virtual DbSet<Gatunek> Gatunek { get; set; }
        public virtual DbSet<Instrument> Instrument { get; set; }
        public virtual DbSet<Playlista> Playlista { get; set; }
        public virtual DbSet<Przynaleznosc> Przynaleznosc { get; set; }
        public virtual DbSet<Umiejetnosc> Umiejetnosc { get; set; }
        public virtual DbSet<Utwor> Utwor { get; set; }
        public virtual DbSet<Wykonawca> Wykonawca { get; set; }
        public virtual DbSet<Zespol> Zespol { get; set; }
    
        public virtual int DodajUtwor(Nullable<int> idZespolu, Nullable<int> idAlbumu, Nullable<int> idGatunku, string tytul, Nullable<System.TimeSpan> czasTrwania)
        {
            var idZespoluParameter = idZespolu.HasValue ?
                new ObjectParameter("IdZespolu", idZespolu) :
                new ObjectParameter("IdZespolu", typeof(int));
    
            var idAlbumuParameter = idAlbumu.HasValue ?
                new ObjectParameter("IdAlbumu", idAlbumu) :
                new ObjectParameter("IdAlbumu", typeof(int));
    
            var idGatunkuParameter = idGatunku.HasValue ?
                new ObjectParameter("IdGatunku", idGatunku) :
                new ObjectParameter("IdGatunku", typeof(int));
    
            var tytulParameter = tytul != null ?
                new ObjectParameter("Tytul", tytul) :
                new ObjectParameter("Tytul", typeof(string));
    
            var czasTrwaniaParameter = czasTrwania.HasValue ?
                new ObjectParameter("CzasTrwania", czasTrwania) :
                new ObjectParameter("CzasTrwania", typeof(System.TimeSpan));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DodajUtwor", idZespoluParameter, idAlbumuParameter, idGatunkuParameter, tytulParameter, czasTrwaniaParameter);
        }
    
        public virtual int UsunUtwor(Nullable<int> idUtworu)
        {
            var idUtworuParameter = idUtworu.HasValue ?
                new ObjectParameter("IdUtworu", idUtworu) :
                new ObjectParameter("IdUtworu", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UsunUtwor", idUtworuParameter);
        }
    
        public virtual int UsunWszystkieUtworuDanegoGatunku(string nazwaGatunku)
        {
            var nazwaGatunkuParameter = nazwaGatunku != null ?
                new ObjectParameter("NazwaGatunku", nazwaGatunku) :
                new ObjectParameter("NazwaGatunku", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UsunWszystkieUtworuDanegoGatunku", nazwaGatunkuParameter);
        }
    
        public virtual int ZmienTytul(Nullable<int> idUtworu, string tytul)
        {
            var idUtworuParameter = idUtworu.HasValue ?
                new ObjectParameter("IdUtworu", idUtworu) :
                new ObjectParameter("IdUtworu", typeof(int));
    
            var tytulParameter = tytul != null ?
                new ObjectParameter("Tytul", tytul) :
                new ObjectParameter("Tytul", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ZmienTytul", idUtworuParameter, tytulParameter);
        }
    
        public virtual ObjectResult<wyswietlinstrumentywykonawcy_Result> wyswietlinstrumentywykonawcy(Nullable<int> idwykonawcy)
        {
            var idwykonawcyParameter = idwykonawcy.HasValue ?
                new ObjectParameter("Idwykonawcy", idwykonawcy) :
                new ObjectParameter("Idwykonawcy", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<wyswietlinstrumentywykonawcy_Result>("wyswietlinstrumentywykonawcy", idwykonawcyParameter);
        }
    
        public virtual int DodajWykonawce(Nullable<int> idWykonawcy, Nullable<int> idZespolu, string imie, string nazwisko, string pseudonim)
        {
            var idWykonawcyParameter = idWykonawcy.HasValue ?
                new ObjectParameter("IdWykonawcy", idWykonawcy) :
                new ObjectParameter("IdWykonawcy", typeof(int));
    
            var idZespoluParameter = idZespolu.HasValue ?
                new ObjectParameter("IdZespolu", idZespolu) :
                new ObjectParameter("IdZespolu", typeof(int));
    
            var imieParameter = imie != null ?
                new ObjectParameter("Imie", imie) :
                new ObjectParameter("Imie", typeof(string));
    
            var nazwiskoParameter = nazwisko != null ?
                new ObjectParameter("Nazwisko", nazwisko) :
                new ObjectParameter("Nazwisko", typeof(string));
    
            var pseudonimParameter = pseudonim != null ?
                new ObjectParameter("Pseudonim", pseudonim) :
                new ObjectParameter("Pseudonim", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DodajWykonawce", idWykonawcyParameter, idZespoluParameter, imieParameter, nazwiskoParameter, pseudonimParameter);
        }
    }
}