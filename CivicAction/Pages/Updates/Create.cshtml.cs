using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CivicAction.Data;
using CivicAction.Models;

namespace CivicAction.Pages.Updates
{
    public class CreateModel : PageModel
    {
        private readonly CivicAction.Data.CivicActionContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CreateModel(CivicAction.Data.CivicActionContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
        ViewData["VerificationId"] = new SelectList(_context.Verifications, "Id", "Id");

        string? userId = _userManager.GetUserId(User);
        var userProjects = _context.Projects.Where(p => p.StudentId == userId);
        ViewData["ProjectId"] = new SelectList(userProjects, "Id", "Title");
        return Page();
        }

        [BindProperty]
        public Update Update { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Updates.Add(Update);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

