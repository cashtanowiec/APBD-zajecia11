using APBD_zajecia11.DAL;
using APBD_zajecia11.DTO;
using Microsoft.EntityFrameworkCore;

namespace APBD_zajecia11.Services.Patient;

public class PatientService : IPatientService
{
    private readonly DatabaseContext _databaseContext;
    public PatientService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task<GetPatientDTO> Get(int id)
    {
        var dto = await _databaseContext.Patients.Where(patient => patient.IdPatient == id).Select(patient => new GetPatientDTO()
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate,
            Prescriptions = patient.PatientPrescriptions.Select(prescriptions => new PrescriptionDTO()
            {
                IdPrescription = prescriptions.IdPrescription,
                Date = prescriptions.Date,
                DueDate = prescriptions.DueDate,
                Doctor = new DoctorDTO()
                {
                    IdDoctor = prescriptions.Doctor.IdDoctor,
                    FirstName = prescriptions.Doctor.FirstName,
                    LastName = prescriptions.Doctor.LastName
                },
                Medicaments = prescriptions.PrescriptionMedicaments.Select(med => new MedicamentDTO()
                {
                    IdMedicament = med.IdMedicament,
                    Dose = med.Dose,
                    Details = med.Details
                }).ToList()
            }).ToList()
        }).FirstOrDefaultAsync();
        
        
        if (dto == null)
        {
            throw new ArgumentException("Wrong patient id!");
        }
        
        return dto;
    }
}