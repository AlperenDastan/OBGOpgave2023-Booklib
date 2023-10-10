using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booklib
{

    public class BookRepository
    {
        private int _nextId = 1;
        private readonly List<Book> _books = new();

        public BookRepository()
        {
            _books.Add(new Book() { Id = _nextId++, Title = "Six of Crows", Price = 1000 });
            _books.Add(new Book() { Id = _nextId++, Title = "Shadow & Bone", Price = 499 });
            _books.Add(new Book() { Id = _nextId++, Title = "The Silmarillion", Price = 399 });
            _books.Add(new Book() { Id = _nextId++, Title = "Laen VS Adem: The Fight for 1st Division", Price = 599 });
            _books.Add(new Book() { Id = _nextId++, Title = "Harry Potter & The Prisoner Of Azkaban", Price = 99 });
        }

        public IEnumerable<Book> Get(string? title = null, double? price = null, string? orderBy = null)
        {
            IEnumerable<Book> result = new List<Book>(_books);

            // Filtering
            if (title != null)
            {
                result = result.Where(book => book.Title.Contains(title));
            }
            if (price != null)
            {
                result = result.Where(book => book.Price >= price);
            }

            // Ordering
            if (orderBy != null)
            {
                orderBy = orderBy.ToLower();
                switch (orderBy)
                {
                    case "title":
                    case "title_asc":
                        result = result.OrderBy(book => book.Title);
                        break;
                    case "title_desc":
                        result = result.OrderByDescending(book => book.Title);
                        break;
                    case "price":
                    case "price_asc":
                        result = result.OrderBy(book => book.Price);
                        break;
                    case "price_desc":
                        result = result.OrderByDescending(book => book.Price);
                        break;
                    default:
                        return result;
                }
            }

            return result;
        }

        public Book? GetById(int id)
        {
            return _books.Find(book => book.Id == id);
        }

        public Book Add(Book book)
        {
            book.ValidateTitle();
            book.ValidatePrice();

            book.Id = _nextId++;
            _books.Add(book);
            return book;
        }

        public Book? Remove(int id)
        {
            Book? book = GetById(id);
            if (book == null)
            {
                return null;
            }
            _books.Remove(book);
            return book;
        }

        public Book? Update(int id, Book book)
        {
            book.ValidateTitle();
            book.ValidatePrice();

            Book? existingBook = GetById(id);
            if (existingBook == null)
            {
                return null;
            }
            existingBook.Title = book.Title;
            existingBook.Price = book.Price;
            return existingBook;
        }
    }
}
