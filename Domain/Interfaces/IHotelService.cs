using HotelNetwork.DAL.Entities;

using System.Diagnostics.Metrics;

namespace HotelNetwork.Domain.Interfaces
{
    public class IHotelService
    {
        Task<IEnumerable<Hotel>> GetHotelsAsync();
        Task<Hotel> CreateHotelAsync(Hotel hotel);
        Task<Hotel> GetHotelByIdAsync(Guid id);
        Task<Hotel> GetHotelByNameAsync(string name);
        Task<Hotel> EditHotelAsync(Hotel hotel);
        Task<Hotel> DeleteHotelAsync(Guid id);
    }
}
