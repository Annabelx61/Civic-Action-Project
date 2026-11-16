using CivicAction.Data;
using CivicAction.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CivicAction.Pages.Projects;

[Authorize]
public class CreateModel(CivicActionContext context, UserManager<AppUser> userManager) : PageModel
{
    [BindProperty]
    public Project Project { get; set; } = default!;

    public IActionResult OnGet() => Page();

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null) return RedirectToPage("/Account/Login");

        // Remove StudentID from validation since we set it server-side
        //ModelState.Remove("Project.StudentID");

        if (!ModelState.IsValid) return Page();

        try
        {
            Inclusion Inclusion = new Inclusion
            {
                StudentId = user.Id,
                Student = user,
                // Check if the project is actually assigned here
                Project = Project,
                ProjectId = Project.Id,
                Hours = 0,
            };
            Project.IsWorkshop = false;
            context.Projects.Add(Project);
            context.Inclusions.Add(Inclusion);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error saving project: {ex.Message}");
            return Page();
        }

        return RedirectToPage("./Index");
    }
}