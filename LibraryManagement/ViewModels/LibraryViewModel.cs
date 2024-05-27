using LibraryManagement.Core;
using LibraryManagement.Models;
using LibraryManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManagement.ViewModels
{
    public class LibraryViewModel : ObservableObject
    {
        private readonly LoanService _loanService;

        public LibraryViewModel(LoanService loanService)
        {
            _loanService = loanService;
            Books = new ObservableCollection<Book>();
            Clients = new ObservableCollection<Client>();
            OverdueClients = new ObservableCollection<Client>();
            PopularBooks = new ObservableCollection<Book>();
            ActiveClients = new ObservableCollection<Client>();
            LoadData();
        }

        // Properties bound to the UI
        private string _bookTitle;
        public string BookTitle
        {
            get => _bookTitle;
            set { _bookTitle = value; OnPropertyChanged(); }
        }

        private string _bookAuthor;
        public string BookAuthor
        {
            get => _bookAuthor;
            set { _bookAuthor = value; OnPropertyChanged(); }
        }

        private int _publicationYear;
        public int PublicationYear
        {
            get => _publicationYear;
            set { _publicationYear = value; OnPropertyChanged(); }
        }

        private int _copiesAvailable;
        public int CopiesAvailable
        {
            get => _copiesAvailable;
            set { _copiesAvailable = value; OnPropertyChanged(); }
        }

        private string _clientFirstName;
        public string ClientFirstName
        {
            get => _clientFirstName;
            set { _clientFirstName = value; OnPropertyChanged(); }
        }

        private string _clientLastName;
        public string ClientLastName
        {
            get => _clientLastName;
            set { _clientLastName = value; OnPropertyChanged(); }
        }

        private DateTime _clientDateOfBirth;
        public DateTime ClientDateOfBirth
        {
            get => _clientDateOfBirth;
            set { _clientDateOfBirth = value; OnPropertyChanged(); }
        }

        private string _clientGender;
        public string ClientGender
        {
            get => _clientGender;
            set { _clientGender = value; OnPropertyChanged(); }
        }

        private int _selectedClientId;
        public int SelectedClientId
        {
            get => _selectedClientId;
            set { _selectedClientId = value; OnPropertyChanged(); }
        }

        private int _selectedBookId;
        public int SelectedBookId
        {
            get => _selectedBookId;
            set { _selectedBookId = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Book> Books { get; set; }
        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<Client> OverdueClients { get; set; }
        public ObservableCollection<Book> PopularBooks { get; set; }
        public ObservableCollection<Client> ActiveClients { get; set; }

        // Commands
        public ICommand AddBookCommand => new RelayCommand(AddBook);
        public ICommand AddClientCommand => new RelayCommand(AddClient);
        public ICommand BorrowBookCommand => new RelayCommand(BorrowBook);
        public ICommand ReturnBookCommand => new RelayCommand(ReturnBook);

        private void AddBook()
        {
            _loanService.AddBook(BookTitle, BookAuthor, PublicationYear, CopiesAvailable);
            LoadBooks();
        }

        private void AddClient()
        {
            _loanService.AddClient(ClientFirstName, ClientLastName, ClientDateOfBirth, ClientGender);
            LoadClients();
        }

        private void BorrowBook()
        {
            _loanService.BorrowBook(SelectedClientId, SelectedBookId);
            LoadData();
        }

        private void ReturnBook()
        {
            _loanService.ReturnBook(SelectedClientId, SelectedBookId);
            LoadData();
        }

        private void LoadData()
        {
            LoadBooks();
            LoadClients();
            LoadOverdueClients();
            LoadPopularBooks();
            LoadActiveClients();
        }

        private void LoadBooks()
        {
            Books.Clear();
            foreach (var book in _loanService.GetBooks())
            {
                Books.Add(book);
            }
        }

        private void LoadClients()
        {
            Clients.Clear();
            foreach (var client in _loanService.GetClients())
            {
                Clients.Add(client);
            }
        }

        private void LoadOverdueClients()
        {
            OverdueClients.Clear();
            foreach (var client in _loanService.GetOverdueClients())
            {
                OverdueClients.Add(client);
            }
        }

        private void LoadPopularBooks()
        {
            PopularBooks.Clear();
            foreach (var book in _loanService.GetPopularBooks())
            {
                PopularBooks.Add(book);
            }
        }

        private void LoadActiveClients()
        {
            ActiveClients.Clear();
            foreach (var client in _loanService.GetActiveClients())
            {
                ActiveClients.Add(client);
            }
        }
    }
}
