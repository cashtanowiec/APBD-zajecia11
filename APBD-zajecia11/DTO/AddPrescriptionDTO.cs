using System.ComponentModel.DataAnnotations;

namespace APBD_zajecia11.DTO;

public class AddPrescriptionDTO
{
    [Required]
    public int IdDoctor { get; set; }
    [Required]
    public PatientDTO PatientDto { get; set; }
    [Required]
    public ICollection<DTO.MedicamentDTO> Medicaments { get; set; }
    [Required]
    public DateOnly Date { get; set; }
    [Required]
    public DateOnly DueDate { get; set; }
}

public class PatientDTO()
{
    [Required]
    public int IdPatient { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public DateOnly BirthDate { get; set; }
}

public class MedicamentDTO()
{
    [Required]
    public int IdMedicament { get; set; }
    [Required]
    public int? Dose { get; set; }
    [Required]
    [MaxLength(100)]
    public string Details { get; set; }
}