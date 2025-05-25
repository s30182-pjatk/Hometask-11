using System.ComponentModel.DataAnnotations;

namespace Tutorial11.Models;

public class Patient
{
    [Key]
    public int IdPatient { get; set; }
    [StringLength(100)]
    public string FirstName { get; set; }
    [StringLength(100)]
    public string LastName { get; set; }
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
    
    public virtual ICollection<Prescription> Prescriptions { get; set; }
}