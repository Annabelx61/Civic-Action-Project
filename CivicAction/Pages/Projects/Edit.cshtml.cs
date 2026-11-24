using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CivicAction.Data;
using CivicAction.Models;

namespace CivicAction.Pages.Projects
{
    public class EditModel : PageModel
    {
        private readonly CivicAction.Data.CivicActionContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EditModel(CivicAction.Data.CivicActionContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public Project Project { get; set; } = default!;
        
        [BindProperty]
        public FileViewModel FileUpload { get; set; } = new();


        public class FileViewModel 
        { 
        public IFormFile? FormFile { get; set; } 
        } 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project =  await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            Project = project;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var file = FileUpload.FormFile;

            if (file != null && file.Length > 0)
            {
                string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("FileUpload.FormFile", "Only JPG, JPEG, PNG, and GIF files are allowed.");
                }

                if (file.Length > 2097152)
                {
                    ModelState.AddModelError("FileUpload.FormFile", "Image must be smaller than 2 MB.");
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var project = await _context.Projects.FindAsync(Project.Id);
            if (project == null)
            {
                return NotFound();
            }

            project.Title = Project.Title;
            project.Description = Project.Description;
            project.Organization = Project.Organization;
            project.SiteLocation = Project.SiteLocation;
            project.Start = Project.Start;
            project.End = Project.End;

            string? oldFilePath = project.FilePath;
            string? newPhysicalPath = null;

            try
            {
                if (file != null && file.Length > 0)
                {
                    string folder = Path.Combine(_hostEnvironment.WebRootPath, "uploadfiles");
                    Directory.CreateDirectory(folder);

                    string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                    string fileName =
                        $"project-{project.Id}-{Guid.NewGuid():N}{extension}";
                    newPhysicalPath = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(newPhysicalPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    project.FilePath = "/uploadfiles/" + fileName;
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (newPhysicalPath != null &&
                    System.IO.File.Exists(newPhysicalPath))
                {
                    System.IO.File.Delete(newPhysicalPath);
                }

                if (!ProjectExists(Project.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch
            {
                if (newPhysicalPath != null &&
                    System.IO.File.Exists(newPhysicalPath))
                {
                    System.IO.File.Delete(newPhysicalPath);
                }

                throw;
            }

            if (newPhysicalPath != null &&
                !string.IsNullOrWhiteSpace(oldFilePath))
            {
                string oldFileName = Path.GetFileName(oldFilePath);
                string oldPhysicalPath =
                    Path.Combine(_hostEnvironment.WebRootPath, "uploadfiles", oldFileName);

                if (System.IO.File.Exists(oldPhysicalPath))
                {
                    System.IO.File.Delete(oldPhysicalPath);
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
