using Microsoft.EntityFrameworkCore;
using HotelNetwork.DAL;
using HotelNetwork.DAL.Entities;
using HotelNetwork.Domain.Interfaces;
using System.Diagnostics.Metrics;

namespace HotelNetwork.Domain.Services
{
    public class HotelService:IHotelService
    {
        private readonly DataBaseContext _context;

        public HotelService(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Hotel>> GetHotelsAsync()
        {
            return await _context.Hotels
                .Include(c => c.Rooms)
                .ToListAsync();
        }
        public async Task<Hotel> CreateHotelAsync(Hotel hotel)
        {
            try
            {
                hotel.Id = Guid.NewGuid();
                hotel.CreatedDate = DateTime.Now;

                _context.Hotels.Add(hotel);
                await _context.SaveChangesAsync();

                return hotel;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }
        public async Task<Hotel> GetHotelByIdAsync(Guid id)
        {
            //return await _context.Hotels.FindAsync(id); // FindAsync es un método propio del DbContext (DbSet)
            //return await _context.Hotels.FirstAsync(x => x.Id == id); //FirstAsync es un método de EF CORE
            return await _context.Hotels.FirstOrDefaultAsync(c => c.Id == id); //FirstOrDefaultAsync es un método de EF CORE
        }

        public async Task<Hotel> GetHotelByNameAsync(string name)
        {
            return await _context.Hotels.FirstOrDefaultAsync(c => c.Name == name);
        }
        public async Task<Hotel> EditHotelAsync(Hotel hotel)
        {
            try
            {
                hotel.ModifiedDate = DateTime.Now;

                _context.Hotels.Update(hotel); //El método Update que es de EF CORE me sirve para Actualizar un objeto
                await _context.SaveChangesAsync();

                return hotel;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }
        public async Task<Hotel> DeleteHotelAsync(Guid id)
        {
            try
            {
                //Aquí, con el ID que traigo desde el controller, estoy recuperando el país que luego voy a eliminar.
                //Ese país que recupero lo guardo en la variable hotel
                var hotel = await _context.Hotels.FirstOrDefaultAsync(c => c.Id == id);
                if (hotel == null) return null; //Si el país no existe, entonces me retorna un NULL

                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();

                return hotel;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }


    }
}
