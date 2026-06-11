namespace APBD_Template.DTOs;

public record PatientGetResponse(
    int Id,
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string? Phone,
    IEnumerable<AppointmentGetResponse> Appointments
);