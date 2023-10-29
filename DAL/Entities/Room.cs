using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Xml.Linq;
namespace HotelNetwork.DAL.Entities
{
    public class Room : AuditBase
    {
        [Display(Name = "Habitaciones")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Required(ErrorMessage = "¡El campo {0} es obligatorio!")]
        public string Id { get; set; }
        public string Number { get; set; }
        public string MaxGuest { get; set; }
        public string Availability { get; set; }


        [Display(Name = "Hotel")]
        //Relación con Hotel
        public Hotel? Hotel { get; set; } //Este representa un OBJETO DE Hotel

        [Display(Name = "Id Hotel")]
        public Guid HotelId { get; set; } //FK

    }
}
