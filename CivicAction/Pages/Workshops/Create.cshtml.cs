using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CivicAction.Data;
using CivicAction.Models;

namespace CivicAction.Pages.Workshops;

[Authorize]
public class CreateModel(CivicActionContext context, UserManager<AppUser> userManager) : PageModel
{
    [BindProperty]
    public Project Project { get; set; } = default!;

    public AppUser? CurrentUser { get; set; }

    public double? WorkshopHours;

    public List<AppUser> AllStudents = new List<AppUser>();

    public List<AppUser> WorkshopStudents = new List<AppUser>();

    public async Task<IActionResult> OnGet()
    {
        CurrentUser = await userManager.GetUserAsync(User);
        IQueryable<AppUser> allStudentsIQ = from a in context.AppUsers select a;
        allStudentsIQ = allStudentsIQ.Where(a => a.IsAdmin == false);
        AllStudents = allStudentsIQ.ToList();
        ViewData["Title"] = "Create";
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        CurrentUser = await userManager.GetUserAsync(User);
        if (CurrentUser == null) return RedirectToPage("/Account/Login");
        
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            foreach (AppUser student in WorkshopStudents)
            {
                // Create a new inclusion to connect that student to this project
                Inclusion Inclusion = new Inclusion
                {
                    Student = student,
                    StudentId = student.Id,
                    Project = Project,
                    ProjectId = Project.Id,
                    Hours = WorkshopHours ?? 0.0
                };
                Project.Inclusions.Add(Inclusion);
                student.Inclusions.Add(Inclusion);
            }
            Project.IsWorkshop = true;
            Project.Status = Status.Approved;
            Project.IsArchived = false;
            context.Projects.Add(Project);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error saving project: {ex.Message}");
            return Page();
        }

        return RedirectToPage("./Index");
    }

    private void PopulateWorkshopStudents(Project workshop)
    {
        var allStudents = context.AppUsers.Where(a => a.IsAdmin == false);
        var workshopInclusions = new HashSet<int>(
            workshop.Inclusions.Select(i => i.Id));

    }
}