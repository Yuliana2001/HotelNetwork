using HotelNetwork.DAL.Entities;

namespace HotelNetwork.Domain.Interfaces
{
    public class IRoomService
    {
        public interface IRoomService
        {
            Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(Guid hotelId);
            Task<Room> CreateRoomAsync(Room room, Guid hotelId);
            Task<Room> GetRoomByIdAsync(Guid id);
            Task<Room> EditRoomAsync(Room room, Guid id);
            Task<Room> DeleteRoomAsync(Guid id);
        }
    }
}
