using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public string Author { get; set; }

        [Required]
        public int PublicationYear { get; set; }

        [Required]
        public int CopiesAvailable { get; set; }

        public ICollection<Loan> Loans { get; set; }

        [NotMapped]
        public int TimesBorrowed => Loans?.Count ?? 0;
    }
}
