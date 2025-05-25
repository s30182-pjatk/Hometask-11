using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutorial11.Models;

public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
    [DataType(DataType.Date)]
    public DateTime DueDate { get; set; }
    
    public int DoctorId { get; set; }
    [ForeignKey(nameof(DoctorId))]
    public virtual Doctor Doctor { get; set; }
    public int PatientId { get; set; }
    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; }
    
    public virtual ICollection<Prescription_Medicament> Prescription_Medicaments { get; set; }
}