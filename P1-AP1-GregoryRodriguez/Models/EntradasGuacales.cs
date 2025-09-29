using System.ComponentModel.DataAnnotations;

namespace P1_AP1_GregoryRodriguez.Models;
public class EntradasGuacales
{
    [Key]
    public int Id { get; set; }
    public string Nombres { get; set; }
}

