using System.ComponentModel.DataAnnotations;

namespace APBD_Template.DTOs;

public record CreateAppointmentServiceRequest(
    int ServiceId,

    [Range(1, int.MaxValue)]
    int Quantity,

    DateTime PerformedAt
);