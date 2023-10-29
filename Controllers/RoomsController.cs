using HotelNetwork.DAL.Entities;
using HotelNetwork.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelNetwork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController
    {
        private readonly IRoomService _roomService;
        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoomsByHotelIdAsync(Guid hotelId)
        {
            var rooms = await _roomService.GetRoomsByHotelIdAsync(hotelId);
            if (rooms == null || !rooms.Any()) return NotFound();

            return Ok(rooms);
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateRoomAsync(Room room, Guid hotelId)
        {
            try
            {
                var createdRoom = await _roomService.CreateRoomAsync(room, hotelId);

                if (createdRoom == null) return NotFound();

                return Ok(createdRoom);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return Conflict(String.Format("La habitación {0} ya existe.", room.Id));
                }

                return Conflict(ex.Message);
            }
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")]
        public async Task<ActionResult<Room>> GetRoomByIdAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var room = await _roomService.GetRoomByIdAsync(id);

            if (room == null) return NotFound();

            return Ok(room);
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult<Room>> EditRoomAsync(Room room, Guid id)
        {
            try
            {
                var editedRoom = await _roomService.EditRoomAsync(room, id);
                return Ok(editedRoom);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", room.Id));

                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<Room>> DeleteRoomAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var deletedRoom = await _roomService.DeleteRoomAsync(id);

            if (deletedRoom == null) return NotFound("Hotel no encontrado!");

            return Ok(deletedRoom);
        }
    }
}
