using APBD_zajecia11.Services.Patient;
using Microsoft.AspNetCore.Mvc;

namespace APBD_zajecia11.Controllers;

[Route("api/{controller}")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var data = await _patientService.Get(id);
            return Ok(data);
        }
        catch (Exception exc)
        {
            return BadRequest(exc.Message);
        }
    }
}