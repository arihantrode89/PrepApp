using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Caching.Memory;
using ServiceContracts;

namespace PrepApi.Controllers
{
    //[Authorize(Roles ="Admin")]
    [Authorize(Policy = "AllowAdmin")]
    [Route("PrepApi")]
    public class BookController : Controller
    {
        private IBookService _book;
        private IMemoryCache _cache;
        private IServiceProvider _provider;
        public BookController(IBookService book,IMemoryCache cache,IServiceProvider provider)
        { 
            _book = book;
            _cache = cache;
            _provider = provider;
        }

        [HttpGet]
        //[OutputCache]
        [Route("books")]
        public IActionResult BooksData()
        {
            //var cachedData = _cache.Get<IEnumerable<Book>>("books");
            //if(cachedData != null) 
            //{ 
            //    return Json(cachedData);
            //}
            //var data = _provider.GetService<IBookService>().GetAllBookData();
            //_cache.Set("books",data, DateTime.Now.AddMinutes(2));
            var data = _book.GetAllBookData();
            return Json(data);
        }

        [HttpPost]
        [Route("addBook")]
        public IActionResult AddBook([FromBody] Book book)
        {
            var data = _book.AddBookData(book);
            return Ok(data);
        }

        [HttpPost]
        [Route("borrow")]
        public async Task<IActionResult> BorrowBook(BorrowRecord record)
        {
            var borrowed = await _book.BorrowBook(record);
            if (borrowed)
            {
                return Ok($"Book has been borrowed");
            }
            return BadRequest("Issue with borrowing book");
        }

        [HttpPut]
        [Route("updatebook")]
        public async Task<IActionResult> UpdateBook([FromBody]Book book)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var updatedRow = await _book.UpdateBookData(book);
                if (updatedRow > 0)
                {
                    return Ok("Data has been succesfully updated");
                }
                return NoContent();
            }
            catch(Exception ex)
            {
                if(ex.Message=="Book doesnot exist")
                {
                    return NotFound(ex.Message);
                }
                return StatusCode(500,ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteBooks([FromBody] List<int> booksIds)
        {
            var deleted = await _book.DeleteBookData(booksIds);
            //var jj = new JsonResult();
            if (deleted)
            {
                return Ok(new Dictionary<string,List<int>>() { { "Ids",booksIds} });
            }
            return NoContent();
        }

    }
}
