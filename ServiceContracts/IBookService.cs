using Entities;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IBookService
    {
        public List<Book> GetAllBookData();

        public Book AddBookData(Book book);

        public Task<bool> BorrowBook(BorrowRecord record);

        public Task<int> UpdateBookData(Book book);

        public Task<bool> DeleteBookData(List<int> bookIds);

    }
}
