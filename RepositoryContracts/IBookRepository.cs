using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IBookRepository
    {
        public List<Book> GetAllBooks();

        public Book AddBook(Book book);

        public Task<bool> BorrowBook(BorrowRecord bRecord);

        public Task<int> UpdateBookDetails(Book book);

        public Task<bool> DeleteBooks(List<int> bookIds);
    }
}
