using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BookRepository : IBookRepository
    {
        private ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Book AddBook(Book book)
        {
            try
            {
                var data = _context.books.Add(new Book() { Title=book.Title,Author=book.Author,AvailableCopies=book.AvailableCopies});
                _context.SaveChanges();
                return data.Entity;
            }
             catch (Exception ex)
            {
                return new Book();
            }
             
        }

        public async Task<bool> BorrowBook(BorrowRecord bRecord)
        {
            try
            {
                await _context.borrowRecords.AddAsync(bRecord);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteBooks(List<int> bookIds)
        {
            try
            {
                var listBook = _context.books.AsQueryable().Where(x => bookIds.Contains(x.Id));
                _context.books.RemoveRange(listBook);
                await _context.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public List<Book> GetAllBooks()
        {
            return _context.books.ToList();
        }

        public async Task<int> UpdateBookDetails(Book book)
        {
            try
            {
                var oldBook = _context.books.Where(x => x.Id == book.Id);
                if (oldBook.ToList().Count== 1)
                {
                    var rowsAffected = await oldBook.ExecuteUpdateAsync(data => data
                .SetProperty(x => x.Title, book.Title)
                .SetProperty(x => x.Author, book.Author)
                .SetProperty(x => x.AvailableCopies, book.AvailableCopies));
                    return rowsAffected;
                }
                throw new Exception("Book doesnot exist");
            }
            catch(Exception ex)
            {
                throw ex; 
            }
        }
    }
}
