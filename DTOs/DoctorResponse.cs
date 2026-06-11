namespace APBD_Template.DTOs;

public record DoctorResponse(
    int DoctorId,
    string FirstName,
    string LastName,
    string Specialization,
    string? Phone
);