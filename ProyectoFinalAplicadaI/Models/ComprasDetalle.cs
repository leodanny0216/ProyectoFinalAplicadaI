using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalAplicadaI.Models;

public class ComprasDetalle
{
    [Key]
    public int ComprasDetalleId { get; set; }
    [ForeignKey("InsumoId")]
    public int InsumoId { get; set; }
    [Required(ErrorMessage = "Debe especificar la cantidad a comprar")]
    [Range(5, int.MaxValue, ErrorMessage = "La cantidad minima de productos a comprar es 5")]
    public int Cantidad { get; set; }
    public string? ImagenURL { get; set; }
}