using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using My_Scripture_Journal.Models;

namespace My_Scripture_Journal.Pages.JournalEntries
{
    public class IndexModel : PageModel
    {
        private readonly My_Scripture_Journal.Models.My_Scripture_JournalContext _context;

        public IndexModel(My_Scripture_Journal.Models.My_Scripture_JournalContext context)
        {
            _context = context;
        }

        public string BookSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<JournalEntry> JournalEntry { get;set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public SelectList Books { get; set; }
        [BindProperty(SupportsGet = true)]
        public string BookTitle { get; set; }

        public IList<JournalEntry> JournalEntries { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            BookSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc"  : "Date";

            CurrentFilter = searchString;

            // Use LINQ to get list of genres.
            IQueryable<string> bookQuery = from b in _context.JournalEntry
                                    orderby b.Book
                                    select b.Book;
        
           
            IQueryable<JournalEntry> journalEntries = from o in _context.JournalEntry
                                              select o;

            if (!String.IsNullOrEmpty(CurrentFilter))
            {
                journalEntries = journalEntries.Where(s => s.JournalEntryInput.Contains(CurrentFilter)
                || s.Book.Contains(CurrentFilter));
            }

            if (!String.IsNullOrEmpty(BookTitle))
            {
                journalEntries = journalEntries.Where(x => x.Book == BookTitle);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    journalEntries = journalEntries.OrderByDescending(o => o.Book);
                    break;
                case "Date":
                    journalEntries = journalEntries.OrderBy(o => o.EntryAdded);
                    break;
                case "date_desc":
                    journalEntries = journalEntries.OrderByDescending(o => o.EntryAdded);
                    break;
                default:
                    journalEntries = journalEntries.OrderByDescending(o => o.Book);
                    break;
            }

            Books = new SelectList(await bookQuery.Distinct().ToListAsync());            
            JournalEntry = await journalEntries.Distinct().ToListAsync();
            
            
        }
    }
}
