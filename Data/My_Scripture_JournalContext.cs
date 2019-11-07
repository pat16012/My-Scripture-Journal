using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace My_Scripture_Journal.Models
{
    public class My_Scripture_JournalContext : DbContext
    {
        public My_Scripture_JournalContext (DbContextOptions<My_Scripture_JournalContext> options)
            : base(options)
        {
        }

        public DbSet<My_Scripture_Journal.Models.JournalEntry> JournalEntry { get; set; }
    }
}
