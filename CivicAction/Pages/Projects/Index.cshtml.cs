using CivicAction.Data;
using CivicAction.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Common;
using SQLitePCL;
using Microsoft.AspNetCore.Components.Web;


namespace CivicAction.Pages.Projects
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly CivicAction.Data.CivicActionContext _context;
        private readonly UserManager<AppUser> _userManager;

        public IndexModel(CivicAction.Data.CivicActionContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Project> Project { get;set; } = default!;
        
        [BindProperty(SupportsGet = true)]
        public string? SelectedWorkshop {get; set;}

        public List<SelectListItem> WorkshopOptions {get; set;} = new(); 
        public bool CanFilterWorkshops { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Challenge();
            }

            CanFilterWorkshops = user.IsAdmin;
            if (CanFilterWorkshops)
            {
                WorkshopOptions = await _context.Projects
                    .AsNoTracking()
                    .Where(p => p.IsWorkshop)
                    .Select(p => p.Title)
                    .Distinct()
                    .OrderBy(title => title)
                    .Select(title => new SelectListItem
                    {
                        Text = title,
                        Value = title
                    })
                    .ToListAsync();
            }

            IQueryable<Project> projectsQuery = _context.Projects
                .Include(p => p.Student)
                .AsNoTracking();

            if (user.IsAdmin)
            {
                if (!string.IsNullOrWhiteSpace(SelectedWorkshop))
                {
                    projectsQuery = projectsQuery
                        .Where(p => p.IsWorkshop && p.Title == SelectedWorkshop);
                }
            }
            else
            {
                projectsQuery = projectsQuery.Where(p => p.StudentId == user.Id);
            }

            Project = await projectsQuery
                .OrderBy(p => p.Title)
                .ToListAsync();

            return Page();
        }
    }
}
