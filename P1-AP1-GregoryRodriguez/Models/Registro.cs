using System.ComponentModel.DataAnnotations;

namespace P1_AP1_GregoryRodriguez.Models;
public class Registro
{
    [Key]
    public int Id { get; set; }
    public string Nombres { get; set; }
}

