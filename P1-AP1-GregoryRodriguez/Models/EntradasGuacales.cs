using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P1_AP1_GregoryRodriguez.Models;
public class EntradasGuacales
{
    [Key]
    public int IdEntrada { get; set; }

    [Required(ErrorMessage = "El nombre es un campo obligatorio")]
    public string NombreCliente { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage ="La cantidad es un campo obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad no puede ser cero o menor")]
    public int Cantidad { get; set; }

    [Required(ErrorMessage ="El precio es un campo obligatorio")]
    [Range(0.1, double.MaxValue, ErrorMessage = "El precio no puede ser cero o menor")]
    public double Precio { get; set; }

    [InverseProperty("EntradaHuacal")]
    public virtual ICollection<EntradasHuacalesDetalle> EntradaHuacalDetalle { get; set; } = new List<EntradasHuacalesDetalle>();
}

