using APBD_Template.DTOs;
using APBD_Template.Exceptions;
using APBD_Template.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Template.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController(IPatientService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? lastName, CancellationToken cancellationToken)
    {
        return Ok(await service.GetAllAsync(lastName, cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreatePatientRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var patient = await service.AddAsync(request, cancellationToken);
            return Created(string.Empty, patient);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
    }
}