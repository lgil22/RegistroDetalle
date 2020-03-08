using Microsoft.EntityFrameworkCore;
using RegistroDetails.Entidades;
using Microsoft.Data.Sqlite;

namespace RegistroDetails.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Persona> Persona { get; set; }

       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            ///optionsBuilder.UseSqlServer(@"Server = .\SqlExpress; Database = PersonasDb; Trusted_Connection = True; ");
            optionsBuilder.UseSqlite(@" Data Source = Persona.db");
        }

    }

}
