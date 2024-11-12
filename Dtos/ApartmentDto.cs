using System.ComponentModel.DataAnnotations;

namespace EP02_LP2.Dtos
{
    public class ApartmentDto
    {
        [Required(ErrorMessage = "El nombre del apartamento es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
        [Phone(ErrorMessage = "El número de teléfono no es válido.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "El piso es obligatorio.")]
        public int Floor { get; set; }

        [Required(ErrorMessage = "El bloque es obligatorio.")]
        [MaxLength(10, ErrorMessage = "El bloque no puede exceder los 10 caracteres.")]
        public string Block { get; set; }
    }

    public class AvailableApartmentDto
    {
        [Required(ErrorMessage = "El piso es obligatorio.")]
        public int Floor { get; set; }

        [Required(ErrorMessage = "El bloque es obligatorio.")]
        [MaxLength(10, ErrorMessage = "El bloque no puede exceder los 10 caracteres.")]
        public string Block { get; set; }
    }
}
