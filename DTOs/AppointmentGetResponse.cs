namespace APBD_Template.DTOs;

public record AppointmentGetResponse(
    int AppointmentId,
    DateTime AppointmentDate,
    string Status,
    DoctorResponse Doctor,
    IEnumerable<AppointmentServiceGetResponse> Services
);