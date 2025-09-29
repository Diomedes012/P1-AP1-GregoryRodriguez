using System.ComponentModel.DataAnnotations;

namespace P1_AP1_GregoryRodriguez.Models;
public class EntradasGuacales
{
    [Key]
    public int IdEntrada { get; set; }

    [Required(ErrorMessage = "El nombre es un campo obligatorio")]
    public string NombreCliente { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Range(0, int.MaxValue, ErrorMessage = "La cantidad no puede ser menor a cero")]
    public int Cantidad { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "El precio no puede ser menor que cero")]
    public double Precio { get; set; }
}

