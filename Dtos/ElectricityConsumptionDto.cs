using System;
using System.ComponentModel.DataAnnotations;

namespace EP02_LP2.Dtos
{
    public class ElectricityConsumptionDto
    {
        [Required(ErrorMessage = "El ID del apartamento es obligatorio.")]
        public int ApartmentId { get; set; }

        [Required(ErrorMessage = "La fecha del consumo es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "La fecha no tiene un formato válido.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "La cantidad de kilowatts es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad de kilowatts debe ser mayor que 0.")]
        public int QuantityKw { get; set; }
    }
}
