using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalAplicadaI.Models;

public class Compras
{
    [Key]
    public int ComprasId { get; set; }
    [Required(ErrorMessage = "Debe seleccionar un usario")]
    public DateTime FechaCompra { get; set; }
    [Required(ErrorMessage = "Agregue una descripcion a esta compra")]
    public string? Descripcion { get; set; }
    [ForeignKey("ComprasId")]
    public ICollection<ComprasDetalle> ComprasDetalle { get; set; } = new List<ComprasDetalle>();

}