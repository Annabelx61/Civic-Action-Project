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

namespace CivicAction.Pages.Updates
{
    public class CreateModel(CivicActionContext context, UserManager<AppUser> userManager, IWebHostEnvironment hostEnvironment) : PageModel
    {

        string? CurrentUserId;
        AppUser? CurrentUser;

        public IActionResult OnGet()
        {
            LoadDropdowns();
            return Page();
        }

        private void LoadDropdowns()
        {
            string? userId = userManager.GetUserId(User);
            IQueryable<Project> projectsIQ = from p in context.Projects select p;

            var inclusions = context.Inclusions
                .Where(i => i.StudentId == userId)
                .ToList();
            var nonWorkshops = projectsIQ.Where(p => inclusions.Select(i => i.ProjectId).Contains(p.Id) && p.IsWorkshop == false);
            ViewData["ProjectId"] = new SelectList(nonWorkshops, "Id", "Title");
        }

        [BindProperty]
        public Update Update { get; set; } = default!;

        [BindProperty]
        public FileViewModel FileUpload { get; set; } = new();

        public class FileViewModel
        {
            public IFormFile? FormFile { get; set; }
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) 
            {
                LoadDropdowns();
                return Page();
            }

            var file = FileUpload.FormFile;

            if (file != null && file.Length > 0)
            {
                string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("FileUpload.FormFile", "Only JPG, JPEG, PNG, and GIF files are allowed.");
                    LoadDropdowns();
                    return Page();
                }

                if (file.Length > 2097152)
                {
                    ModelState.AddModelError("FileUpload.FormFile", "Image must be smaller than 2 MB.");
                    LoadDropdowns();
                    return Page();
                }
            }

            try
            {
                string? userId = userManager.GetUserId(User);
                if (userId == null)
                {
                    return Page();
                }
                Update.StudentId = userId;
                Update.Student = await userManager.FindByIdAsync(userId);
                var thisProject = context.Projects.Where(p => p.Id == Update.ProjectId).FirstOrDefault();
                if (thisProject == null)
                {
                    ModelState.AddModelError("Update.ProjectId", "The selected project does not exist.");
                    LoadDropdowns();
                    return Page();
                }
                Update.Project = thisProject;
                Update.Status = Status.Pending;
                thisProject.Status = Status.Pending;
                context.Updates.Add(Update);
                await context.SaveChangesAsync();

                if (file != null && file.Length > 0)
                {
                    string folder = Path.Combine(hostEnvironment.WebRootPath, "uploadfiles");
                    Directory.CreateDirectory(folder);

                    string fileName =
                        $"update-{Update.Id}-{Guid.NewGuid():N}" +
                        Path.GetExtension(file.FileName).ToLowerInvariant();
                    string filePath = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    Update.FilePath = "/uploadfiles/" + fileName;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error saving project: {ex.Message}");
                LoadDropdowns();
                return Page();
            }
            

            return RedirectToPage("./Index");
        }
    }
}

