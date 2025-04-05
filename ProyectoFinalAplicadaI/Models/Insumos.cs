using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalAplicadaI.Models;

public class Insumos
{
    [Key]
    public int InsumosId { get; set; }
    [Required(ErrorMessage = "Este campo es obligaotio")]
    [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "El Nombre debe contener solo letras.")]
    public string? NombreInsumo { get; set; }

    public int Cantidad { get; set; }
    public DateTime fechaRegistro { get; set; }
    [Required(ErrorMessage = "Este campo es obligaotio")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "Debe especificar el precio")]
    [Range(1.00, float.MaxValue, ErrorMessage = "EL precio debe ser mayor que 1.00")]
    public float Precio { get; set; }
}