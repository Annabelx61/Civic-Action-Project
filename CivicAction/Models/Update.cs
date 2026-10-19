namespace CivicAction.Models;

using System.ComponentModel.DataAnnotations;

public class Update
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Date is required")]
    public DateOnly Date { get; set; }
    
    public TimeOnly? StartTime { get; set; }
    
    public TimeOnly? EndTime { get; set; }
    
    public double HoursDone { get; set; }

    public int? ProjectId { get; set; }
    public Project? Project { get; set; } 

    public string? StudentId { get; set; }
    public AppUser? Student { get; set; }
    
    public int? VerificationId { get; set; }
    public Verification? Verification { get; set; }
}