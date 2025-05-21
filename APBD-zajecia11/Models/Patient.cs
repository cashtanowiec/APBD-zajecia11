using System.ComponentModel.DataAnnotations;

namespace APBD_zajecia11.Models;

public class Patient
{
    [Key]
    public int IdPatient { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }

    public ICollection<Prescription> PatientPrescriptions { get; set; }
}