using System.ComponentModel.DataAnnotations;
namespace HotelNetwork.DAL.Entities
{
    public class Hotel : AuditBase
    {
        [Display(Name = "Hotel")] // Para identificar el nombre más fácil
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")] // Longitud máx
        [Required(ErrorMessage = "Es campo {0} es obligatorio")] // Campo obligatorio
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Stars { get; set; }

        [Display(Name = "Habitaciones")]
        //relación con Room 
        public ICollection<Room>? Rooms { get; set; }
    }
}
