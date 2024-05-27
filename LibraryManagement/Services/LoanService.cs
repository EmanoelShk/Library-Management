using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{

    public class LoanService
    {
        private readonly LibraryContext _context;

        public LoanService(LibraryContext context)
        {
            _context = context;
        }

        public void AddBook(string title, string author, int publicationYear, int copiesAvailable)
        {
            var book = new Book
            {
                Title = title,
                Author = author,
                PublicationYear = publicationYear,
                CopiesAvailable = copiesAvailable
            };

            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void AddClient(string firstName, string lastName, DateTime dateOfBirth, string gender)
        {
            var client = new Client
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Gender = gender
            };

            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public void BorrowBook(int clientId, int bookId)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null && book.CopiesAvailable > 0)
            {
                var loan = new Loan
                {
                    ClientId = clientId,
                    BookId = bookId,
                    LoanDate = DateTime.Now
                };

                book.CopiesAvailable -= 1;
                _context.Loans.Add(loan);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("No copies available for this book.");
            }
        }

        public void ReturnBook(int clientId, int bookId)
        {
            var loan = _context.Loans.FirstOrDefault(l => l.ClientId == clientId && l.BookId == bookId && l.ReturnDate == null);
            if (loan != null)
            {
                loan.ReturnDate = DateTime.Now;
                loan.CalculateOverdueFee();
                var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
                if (book != null)
                {
                    book.CopiesAvailable += 1;
                    _context.SaveChanges();
                }
            }
            else
            {
                throw new Exception("Loan record not found.");
            }
        }

        public IEnumerable<Book> GetBooks()
        {
            return _context.Books.ToList();
        }

        public IEnumerable<Client> GetClients()
        {
            return _context.Clients.Include(c => c.Loans).ToList();
        }

        public IEnumerable<Client> GetOverdueClients()
        {
            var clients = _context.Clients
                .Include(c => c.Loans)
                .ToList();

            var overdueClients = new List<Client>();

            foreach (var client in clients)
            {
                var overdueLoans = client.Loans
                    .Where(l => l.ReturnDate == null && (DateTime.Now - l.LoanDate).Days > 30)
                    .ToList();

                if (overdueLoans.Any())
                {
                    client.OverdueFee = overdueLoans.Sum(l => (DateTime.Now - l.LoanDate).Days - 30);
                    overdueClients.Add(client);
                }
            }

            return overdueClients;
        }

        public IEnumerable<Book> GetPopularBooks()
        {
            return _context.Books
                .OrderByDescending(b => b.Loans.Count)
                .Take(3)
                .ToList();
        }

        public IEnumerable<Client> GetActiveClients()
        {
            return _context.Clients
                .OrderByDescending(c => c.Loans.Count)
                .Take(3)
                .ToList();
        }
    }
}
