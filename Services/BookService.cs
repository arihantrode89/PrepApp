using Entities;
using RepositoryContracts;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookService:IBookService
    {
        private IBookRepository _bookRepository;
        public BookService(IBookRepository bookrepo)
        {
            _bookRepository = bookrepo;
        }

        public Book AddBookData(Book book)
        {
            return _bookRepository.AddBook(book);
        }

        public async Task<bool> BorrowBook(BorrowRecord record)
        {
            var borrowed = await _bookRepository.BorrowBook(record);
            return borrowed;
        }

        public async Task<bool> DeleteBookData(List<int> bookIds)
        {
            var isDeleted = await _bookRepository.DeleteBooks(bookIds);
            return isDeleted;
        }

        public List<Book> GetAllBookData()
        {
            return _bookRepository.GetAllBooks();
        }

        public async Task<int> UpdateBookData(Book book)
        {
            var updatedData = await _bookRepository.UpdateBookDetails(book);
            return updatedData;
            
        }
    }
}
