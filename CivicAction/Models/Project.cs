using System.ComponentModel.DataAnnotations;

namespace CivicAction.Models;

public enum ProjectType 
{
    Individual, Group, Workshop
}

public class Project
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Description is required")]
    [DataType(DataType.MultilineText)]
    public string Description { get; set; } = string.Empty;

    public double Hours { get; set; }
    public string? Organization { get; set; } = string.Empty;
    public string SiteLocation { get; set; } = string.Empty;
    public string School { get; set; } = string.Empty;

    public DateOnly Start { get; set; }
    public DateOnly End { get; set; }

    public bool IsApproved { get; set; }
    public bool IsWorkshop { get; set; }
    public bool IsArchived { get; set; }

    public ICollection<Verification> Verifications { get; set; } = new List<Verification>();
}