using Microsoft.AspNetCore.Mvc;
using HotelNetwork.DAL.Entities;
using HotelNetwork.Domain.Interfaces;
using System.Diagnostics.Metrics;

namespace HotelNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet, ActionName("Get")]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotelsAsync()
        {
            var hotels = await _hotelService.GetHotelsAsync();

            if (hotels == null || !hotels.Any()) return NotFound();

            return Ok(hotels);
        }
        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateHotelAsync(Hotel hotel)
        {
            try
            {
                var createdHotel = await _hotelService.CreateHotelAsync(hotel);
                return Ok(createdHotel);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", hotel.Name));

                return Conflict(ex.Message);
            }
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")] //URL: api/hotels/get
        public async Task<ActionResult<Hotel>> GetHotelByIdAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var hotel = await _hotelService.GetHotelByIdAsync(id);

            if (hotel == null) return NotFound(); // 404

            return Ok(hotel); // 200
        }

        [HttpGet, ActionName("Get")]
        [Route("GetByName/{name}")] //URL: api/hotels/get
        public async Task<ActionResult<Hotel>> GetHotelByNameAsync(string name)
        {
            if (name == null) return BadRequest("Nombre del hotel requerido!");

            var hotel = await _hotelService.GetHotelByNameAsync(name);

            if (hotel == null) return NotFound(); // 404

            return Ok(hotel); // 200
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult<Hotel>> EditHotelAsync(Hotel hotel)
        {
            try
            {
                var editedHotel = await _hotelService.EditHotelAsync(hotel);
                return Ok(editedHotel);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", hotel.Name));

                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<Hotel>> DeleteHotelAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var deletedHotel = await _hotelService.DeleteHotelAsync(id);

            if (deletedHotel == null) return NotFound("Hotel no encontrado!");

            return Ok(deletedHotel);
        }
    }
}