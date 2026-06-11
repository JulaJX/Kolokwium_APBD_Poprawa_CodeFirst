using APBD_Template.DTOs;

namespace APBD_Template.Services;

public interface IPatientService
{
    Task<IEnumerable<PatientGetResponse>> GetAllAsync(string? lastName, CancellationToken cancellationToken);
    Task<PatientGetResponse> AddAsync(CreatePatientRequest request, CancellationToken cancellationToken);
}