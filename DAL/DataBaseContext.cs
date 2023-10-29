using Microsoft.EntityFrameworkCore;
using HotelNetwork.DAL.Entities;
using System.Diagnostics.Metrics;

namespace HotelNetwork.DAL
{
    public class DataBaseContext: DbContext
    {
        //Así me conecto a la BD por medio de este constructor
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        //Este método que es propio de EF CORE me sirve para configurar unos índices de cada campo de una tabla en BD
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Hotel>().HasIndex(c => c.Name).IsUnique(); //Aquí creo un índice del campo Name para la tabla Hotels
            modelBuilder.Entity<Room>().HasIndex("Id", "HotelId").IsUnique(); //Indices Compuestos
        }
        #region DbSets

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }

        #endregion

    }
}
