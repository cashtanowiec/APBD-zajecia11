using APBD_zajecia11.DTO;
using APBD_zajecia11.Services.Prescription;
using Microsoft.AspNetCore.Mvc;

namespace APBD_zajecia11.Controllers;

[Route("api/{controller}")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;
    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(AddPrescriptionDTO addPrescriptionDto, CancellationToken cancellationToken)
    {
        try
        {
            var id = await _prescriptionService.Add(addPrescriptionDto, cancellationToken);
            return Ok(id);
        }
        catch (Exception exc)
        {
            return BadRequest(exc.Message);
        }
    }
}