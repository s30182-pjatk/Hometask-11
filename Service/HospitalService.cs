using Tutorial11.DAL;
using Tutorial11.Models;

namespace Tutorial11.Service;

public class HospitalService : IHospitalService
{
    
    private readonly HospitalDBContext _context;

    public HospitalService(HospitalDBContext context)
    {
        _context = context;
    }

    public async Task<int> addPrescription(PrescriptionDTO prescription)
    {
        //Check for Patient
        var patient = _context.Patients.FirstOrDefault(p => p.IdPatient == prescription.Patient.IdPatient);
        if (patient == null)
        {
            _context.Patients.Add(new Patient()
            {
                IdPatient = prescription.Patient.IdPatient,
                BirthDate = prescription.Patient.BirthDate,
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
            });
            
            await _context.SaveChangesAsync(); 
        }
        
        //Check prescription length
        if (prescription.Medicaments.Count >= 10)
        {
            throw new Exception("Medicaments count is greater than 10");
        }
        
        //Check Medicaments
        foreach (var medicament in prescription.Medicaments)
        {
            if (!await medicationExists(medicament))
            {
                throw new Exception("Medicament not found");
            }
        }
        
        //Check dates
        if (DateTime.Parse(prescription.DueDate) < DateTime.Parse(prescription.Date))
        {
            throw new Exception("Due date cannot be earlier than date of issue");
        }
        
        //Insert prescription
        _context.Prescriptions.Add(new Prescription()
        {
            PatientId = prescription.Patient.IdPatient,
            DoctorId = prescription.Doctor.IdDoctor,
            Date = DateTime.Parse(prescription.Date),
            DueDate = DateTime.Parse(prescription.DueDate),
        });
        
        await _context.SaveChangesAsync();
        
        //Insert Medications
        var prescriptionId = _context.Prescriptions.OrderByDescending(p => p.IdPrescription).First().IdPrescription;
        foreach (var medication in prescription.Medicaments)
        {
            _context.Prescription_Medicaments.Add(new Prescription_Medicament()
            {
                Details = medication.Details,
                MedicamentId = medication.IdMedicament,
                PrescriptionId = prescriptionId,
                Dose = medication.Dose
            });
        }
        
        await _context.SaveChangesAsync(); 

        return prescriptionId;
    }

    public async Task<GetPatientDto> getPatientById(int id)
    {
        var result = _context.Patients
            .Where(p => p.IdPatient == id)
            .Select(p => new GetPatientDto()
            {
                PatientId = p.IdPatient,
                FirstName = p.FirstName,
                LastName = p.LastName,
                BirthDate = p.BirthDate,
                Prescriptions = p.Prescriptions
                    .OrderBy(pr => pr.DueDate)
                    .Select(pr => new PrescriptionDto()
                    {
                        PrescriptionId = pr.IdPrescription,
                        Date = pr.Date,
                        DueDate = pr.DueDate,
                        Doctor = new DoctorDto()
                        {
                            Email = pr.Doctor.Email,
                            FirstName = pr.Doctor.FirstName,
                            LastName = pr.Doctor.LastName,
                            IdDoctor = pr.Doctor.IdDoctor
                        },
                        
                        Medications = pr.Prescription_Medicaments
                            .Select(pm => new MedicamentDto(){
                                Description = pm.Medicament.Description,
                                Details = pm.Details,
                                Dose = pm.Dose,
                                IdMedicament = pm.Medicament.IdMedicament,
                                Name = pm.Medicament.Name,
                                Type = pm.Medicament.Type
                            }).ToList()
                    }
                    ).ToList()
            }).First();
        return result;
    }

    private async Task<bool> medicationExists(MedicamentDTO medicament)
    {
        var result = _context.Medicaments.FirstOrDefault(m => m.IdMedicament == medicament.IdMedicament);
        return result != null;
    } 
}