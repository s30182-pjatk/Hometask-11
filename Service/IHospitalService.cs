using Tutorial11.Models;

namespace Tutorial11.Service;

public interface IHospitalService
{
    Task<int> addPrescription(PrescriptionDTO prescription);
    Task<GetPatientDto> getPatientById(int id);
}