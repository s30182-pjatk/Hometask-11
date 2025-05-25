using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tutorial11.Models;
[PrimaryKey("MedicamentId", "PrescriptionId")]
public class Prescription_Medicament
{
    public int? Dose { get; set; }
    [MaxLength(100)]
    public string Details { get; set; }
    public int MedicamentId { get; set; }
    [ForeignKey(nameof(MedicamentId))]
    public virtual Medicament Medicament { get; set; }
    public int PrescriptionId { get; set; }
    [ForeignKey(nameof(PrescriptionId))]
    public virtual Prescription Prescription { get; set; }
}