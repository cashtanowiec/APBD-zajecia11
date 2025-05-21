using APBD_zajecia11.DTO;
using Microsoft.AspNetCore.Mvc;

namespace APBD_zajecia11.Services.Prescription;

public interface IPrescriptionService
{
    Task<int> Add(AddPrescriptionDTO addPrescriptionDto, CancellationToken cancellationToken);
}