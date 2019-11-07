using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_Scripture_Journal.Models
{
    public class JournalEntry
    {
        public int ID { get; set; }

        [Display(Name = "Book of Scripture")]
        [Required]
        public string Book { get; set; }

        [StringLength(5000, MinimumLength = 3)]
        [Required]
        [Display(Name = "Notes")]
        public string JournalEntryInput { get; set; }

        [Display(Name = "Date Recorded")]
        [DataType(DataType.Date)]
        public DateTime EntryAdded { get; set; }
    }
}
