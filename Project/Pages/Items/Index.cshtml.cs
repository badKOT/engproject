using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Pages.Items
{
    public class IndexModel : PageModel
    {
        private readonly Project.Data.ProjectContext _context;

        public IndexModel(Project.Data.ProjectContext context)
        {
            _context = context;
        }

        public IList<Item> Item { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Tags { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ItemTag { get; set; }

        public async Task OnGetAsync()
        {
            // Use LINQ to get list of tags.
            IQueryable<string> tagQuery = from m in _context.Item
                                            orderby m.Tag
                                            select m.Tag;

            var items = from m in _context.Item
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                items = items.Where(s => s.Title.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(ItemTag))
            {
                items = items.Where(x => x.Tag == ItemTag);
            }
            Tags = new SelectList(await tagQuery.Distinct().ToListAsync());
            Item = await items.ToListAsync();
        }
    }
}
