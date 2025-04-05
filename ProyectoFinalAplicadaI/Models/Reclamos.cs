using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalAplicadaI.Models;

public class Reclamos
{
    [Key]
    public int ReclamoId { get; set; }
    public DateTime FechaReclamo { get; set; }
    [ForeignKey("ClienteId")]
    public int ClienteId { get; set; }
    [Required(ErrorMessage = "Se requiere una breve descripción del Reclamo.")]
    public string? Descripcion { get; set; }
    [Required(ErrorMessage = "Se requiere ingresar la Acción tomada.")]
    public string? AccionTomada { get; set; }
}