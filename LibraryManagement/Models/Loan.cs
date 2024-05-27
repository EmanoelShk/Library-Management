using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public DateTime LoanDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public Client Client { get; set; }
        public Book Book { get; set; }

        public double CalculateOverdueFee()
        {
            if (ReturnDate == null)
            {
                var daysOverdue = (DateTime.Now - LoanDate).Days - 30;
                return daysOverdue > 0 ? daysOverdue * 1.0 : 0;
            }
            else
            {
                var daysOverdue = (ReturnDate.Value - LoanDate).Days - 30;
                return daysOverdue > 0 ? daysOverdue * 1.0 : 0;
            }
        }
    }
}
