using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CivicAction.Data;
using CivicAction.Models;

namespace CivicAction.Pages.Updates
{
    public class DeleteModel : PageModel
    {
        private readonly CivicAction.Data.CivicActionContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public DeleteModel(CivicAction.Data.CivicActionContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public Update Update { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var update = await _context.Updates.FirstOrDefaultAsync(m => m.Id == id);

            if (update is not null)
            {
                Update = update;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var update = await _context.Updates.FindAsync(id);
            if (update != null)
            {
                Update = update;
                string? oldFilePath = update.FilePath;

                _context.Updates.Remove(Update);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrWhiteSpace(oldFilePath))
                {
                    var fileName = Path.GetFileName(oldFilePath);
                    var path = Path.Combine(_hostEnvironment.WebRootPath,"uploadfiles",fileName);

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
