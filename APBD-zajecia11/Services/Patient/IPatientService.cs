using APBD_zajecia11.DTO;

namespace APBD_zajecia11.Services.Patient;

public interface IPatientService
{
    Task<GetPatientDTO> Get(int id);
}