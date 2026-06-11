using System.ComponentModel.DataAnnotations;

namespace APBD_Template.DTOs;

public record CreateStudentRequest(

    [MaxLength(100)]
    string FirstName,
    [MaxLength(100)]
    string LastName,
    [MaxLength(11),MinLength(11)]
    string Pesel,
    [MaxLength(9),MinLength(9)]
    string? Phone
    
);