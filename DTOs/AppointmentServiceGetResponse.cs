namespace APBD_Template.DTOs;

public record AppointmentServiceGetResponse(
    int ServiceId,
    string Name,
    string Description,
    decimal Price,
    int DurationMinutes,
    int Quantity,
    DateTime PerformedAt
);