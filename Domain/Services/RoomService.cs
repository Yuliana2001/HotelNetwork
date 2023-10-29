using Microsoft.EntityFrameworkCore;
using HotelNetwork.DAL;
using HotelNetwork.DAL.Entities;
using HotelNetwork.Domain.Interfaces;



namespace HotelNetwork.Domain.Services
{
    public class RoomService: IRoomService
    {
        private readonly DataBaseContext _context; //FALTAAAAAAAAAAA
        public RoomService(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetRoomsByHotelkIdAsync(Guid roomId)
        {
            return await _context.Rooms
                .Where(s => s.HotelId == hotelId)
                .ToListAsync();
        }

        public async Task<Hotel> CreateRoomAsync(Room room, Guid hotelId)
        {
            try
            {
                room.Id = Guid.NewGuid();
                room.CreatedDate = DateTime.Now;
                room.HotelId = hotelId;
                room.Hotel = await _context.Hotels.FirstOrDefaultAsync(c => c.Id == hotelId);
                room.ModifiedDate = null;

                _context.Rooms.Add(room);
                await _context.SaveChangesAsync();

                return room;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }
        public async Task<Hotel> GetHotelByIdAsync(Guid id)
        {
            return await _context.Hotels.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Room> GetRoomByIdAsync(Guid id)
        {
            return await _context.Rooms.FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<Room> EditRoomAsync(Room room, Guid id)
        {
            try
            {
                room.ModifiedDate = DateTime.Now;

                _context.Rooms.Update(room);
                await _context.SaveChangesAsync();

                return room;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }
        public async Task<Room> DeleteRoomAsync(Guid id)
        {
            try
            {
                var room = await _context.Rooms.FirstOrDefaultAsync(s => s.Id == id);
                if (room == null) return null;

                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();

                return room;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }



    }
}
