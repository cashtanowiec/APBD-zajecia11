using System.Runtime.InteropServices.JavaScript;

namespace APBD_zajecia11.DTO;

public class GetPatientDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public ICollection<PrescriptionDTO> Prescriptions { get; set; }
}

public class PrescriptionDTO
{
    public int IdPrescription { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
    public ICollection<MedicamentDTO> Medicaments { get; set; }
    public DoctorDTO Doctor { get; set; }
}

public class DoctorDTO
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}