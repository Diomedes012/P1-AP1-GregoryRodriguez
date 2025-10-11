using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace P1_AP1_GregoryRodriguez.Models;

public class TiposHuacales
{
    [Key]
    public int TipoId { get; set; }

    [Required(ErrorMessage ="La descripcion es requerida")]
    public string Descripcion { get; set; }

    [Range(0, int.MaxValue, ErrorMessage ="La existencia no puede ser inferior a 0")]
    public int Existencia { get; set; }

    [InverseProperty("TipoHuacal")]
    public virtual ICollection<EntradasHuacalesDetalle> EntradaHuacalDetalle {  get; set; } = new List<EntradasHuacalesDetalle>();
}
