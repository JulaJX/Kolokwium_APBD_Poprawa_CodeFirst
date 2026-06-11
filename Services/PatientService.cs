using APBD_Template.Data;
using APBD_Template.DTOs;
using APBD_Template.Entities;
using APBD_Template.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace APBD_Template.Services;

public class PatientService(DatabaseContext ctx) : IPatientService
{
    public async Task<IEnumerable<PatientGetResponse>> GetAllAsync(string? lastName, CancellationToken cancellationToken)
    {
        var query = ctx.Patients.AsQueryable();

        if (!string.IsNullOrWhiteSpace(lastName))
        {
            query = query.Where(p => p.LastName.Contains(lastName));
        }

        return await query
            .OrderBy(p => p.LastName)
            .ThenBy(p => p.FirstName)
            .Select(p => new PatientGetResponse(
                p.PatientId,
                p.FirstName,
                p.LastName,
                p.DateOfBirth,
                p.Phone,
                p.Appointments
                    .OrderBy(a => a.AppointmentDate)
                    .Select(a => new AppointmentGetResponse(
                        a.AppointmentId,
                        a.AppointmentDate,
                        a.Status,
                        new DoctorResponse(
                            a.Doctor.DoctorId,
                            a.Doctor.FirstName,
                            a.Doctor.LastName,
                            a.Doctor.Specialization,
                            a.Doctor.Phone
                        ),
                        a.AppointmentServices
                            .Select(s => new AppointmentServiceGetResponse(
                                s.ServiceId,
                                s.MedicalService.Name,
                                s.MedicalService.Description,
                                s.MedicalService.Price,
                                s.MedicalService.DurationMinutes,
                                s.Quantity,
                                s.PerformedAt
                            ))
                    ))
            ))
            .ToListAsync(cancellationToken);
    }

    public async Task<PatientGetResponse> AddAsync(CreatePatientRequest request, CancellationToken cancellationToken)
    {
        var doctor = await ctx.Doctors
            .FirstOrDefaultAsync(d => d.DoctorId == request.DoctorId, cancellationToken);

        if (doctor == null)
        {
            throw new NotFoundException($"Doctor with id {request.DoctorId} not found");
        }

        if (request.AppointmentDate.Date < DateTime.UtcNow.Date)
        {
            throw new BadRequestException("Appointment date cannot be earlier than current date");
        }

        var serviceIds = request.Services.Select(s => s.ServiceId).Distinct().ToList();

        var existingServices = await ctx.MedicalServices
            .Where(ms => serviceIds.Contains(ms.ServiceId))
            .ToListAsync(cancellationToken);

        if (existingServices.Count != serviceIds.Count)
        {
            throw new BadRequestException("One or more medical services do not exist");
        }

        await using var transaction = await ctx.Database.BeginTransactionAsync(cancellationToken);

        var patient = new Patient
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Phone = request.Phone
        };

        ctx.Patients.Add(patient);
        await ctx.SaveChangesAsync(cancellationToken);

        var appointment = new Appointment
        {
            PatientId = patient.PatientId,
            DoctorId = request.DoctorId,
            AppointmentDate = request.AppointmentDate,
            Status = request.Status
        };

        ctx.Appointments.Add(appointment);
        await ctx.SaveChangesAsync(cancellationToken);

        var appointmentServices = request.Services.Select(s => new AppointmentService
        {
            AppointmentId = appointment.AppointmentId,
            ServiceId = s.ServiceId,
            Quantity = s.Quantity,
            PerformedAt = s.PerformedAt
        });

        ctx.AppointmentServices.AddRange(appointmentServices);
        await ctx.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return new PatientGetResponse(
            patient.PatientId,
            patient.FirstName,
            patient.LastName,
            patient.DateOfBirth,
            patient.Phone,
            new List<AppointmentGetResponse>
            {
                new AppointmentGetResponse(
                    appointment.AppointmentId,
                    appointment.AppointmentDate,
                    appointment.Status,
                    new DoctorResponse(
                        doctor.DoctorId,
                        doctor.FirstName,
                        doctor.LastName,
                        doctor.Specialization,
                        doctor.Phone
                    ),
                    request.Services.Select(s =>
                    {
                        var service = existingServices.First(es => es.ServiceId == s.ServiceId);
                        return new AppointmentServiceGetResponse(
                            service.ServiceId,
                            service.Name,
                            service.Description,
                            service.Price,
                            service.DurationMinutes,
                            s.Quantity,
                            s.PerformedAt
                        );
                    })
                )
            }
        );
    }
}