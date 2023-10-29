using HotelNetwork.DAL.Entities;
using System.Diagnostics.Metrics;

namespace HotelNetwork.DAL
{
    public class SeederDB
    {
        private readonly DataBaseContext _context;

        public SeederDB(DataBaseContext context)
        {
            _context = context;
        }
        //Crearemos un método llamado SeederAsync()
        //Este método es una especie de MAIN()
        //Este método tendrá la responsabilidad de prepoblar mis diferentes tablas de la BD.

        public async Task SeederAsync()
        {
            //Primero: agregaré un método propio de EF que hace las veces del comando 'update-database'
            //En otras palabras: un método que me creará la BD inmediatamente ponga en ejecución mi API
            await _context.Database.EnsureCreatedAsync();

            //A partir de aquí vamos a ir creando métodos que me sirvan para prepoblar mi BD
            await PopulateHotelsAsync();

            await _context.SaveChangesAsync(); //Esta línea me guarda ls datos en BD
        }

        #region Private Methos
        private async Task PopulateHotelsAsync()
        {
            //El método Any() me indica si la tabla Hotels tiene al menos un registro
            //El método Any negado (!) me indica que no hay absolutamente nada en la tabla Hotels.

            if (!_context.Hotels.Any())
            {
                //Así creo yo un objeto país con sus respectivos estados
                _context.Hotels.Add(new Hotel
                {
                    CreatedDate = DateTime.Now,
                    Name = " Hotel York Luxury ",
                    Rooms = new List<Room>()
                    {
                        new Room
                        {
                            CreatedDate = DateTime.Now,
                            Id = "111"
                        },

                        new Room
                        {
                            CreatedDate = DateTime.Now,
                            Id = "112"
                        }
                    }
                });

                //Aquí creo otro nuevo país
                _context.Hotels.Add(new Hotel
                {
                    CreatedDate = DateTime.Now,
                    Name = "Hotel Du Parc",
                    Rooms = new List<Room>()
                    {
                        new Room
                        {
                            CreatedDate = DateTime.Now,
                            Id = "113"
                        }
                    }
                });
            }
        }
    }

    #endregion
}
