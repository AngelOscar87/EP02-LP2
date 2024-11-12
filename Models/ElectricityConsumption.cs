using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EP02_LP2.Models
{
    public class ElectricityConsumption
    {
        [Key] // Define la clave primaria
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID del apartamento es obligatorio.")]
        [ForeignKey("Apartment")]
        public int ApartmentId { get; set; } // Relación con Apartment

        [Required(ErrorMessage = "La fecha del consumo es obligatoria.")]
        public DateTime Date { get; set; } // Fecha del consumo eléctrico

        [Required(ErrorMessage = "La cantidad de kilowatts es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad de kilowatts debe ser mayor que 0.")]
        public int QuantityKw { get; set; } // Cantidad de kilowatts consumidos

        // Relación de navegación con Apartment
        public Apartment Apartment { get; set; }
    }
}
