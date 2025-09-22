using System.ComponentModel.DataAnnotations;

namespace P1_AP1_GregoryRodriguez.Models;
public class Registro
{
    [Key]
    int Id { get; set; }
    string Nombres { get; set; }
}

