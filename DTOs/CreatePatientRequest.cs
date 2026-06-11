using System.ComponentModel.DataAnnotations;

namespace APBD_Template.DTOs;

public record CreatePatientRequest(
    [MaxLength(50)]
    string FirstName,

    [MaxLength(100)]
    string LastName,

    DateTime DateOfBirth,

    [MaxLength(9), MinLength(9)]
    string? Phone,

    int DoctorId,

    DateTime AppointmentDate,

    [MaxLength(50)]
    string Status,

    [MinLength(1)]
    IEnumerable<CreateAppointmentServiceRequest> Services
);