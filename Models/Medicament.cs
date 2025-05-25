using System.ComponentModel.DataAnnotations;

namespace Tutorial11.Models;

public class Medicament
{
    [Key]
    public int IdMedicament { get; set; }
    [StringLength(100)]
    public string Name { get; set; }
    [StringLength(100)]
    public string Description { get; set; }
    [StringLength(100)]
    public string Type { get; set; }
    
    public virtual ICollection<Prescription_Medicament> Prescription_Medicaments { get; set; }
}