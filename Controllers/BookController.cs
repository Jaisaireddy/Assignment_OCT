using Microsoft.AspNetCore.Mvc;
using Models_1;
using LibraryContext;
using Microsoft.EntityFrameworkCore;
[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly LibraryContext_Db _context;

    public BooksController(LibraryContext_Db context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromForm] int bookid)
    {
        // Assume book creation logic with given bookid
        var book = new Book { Id = bookid, Title = "Sample Book", ISBN = "123-456" };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return Ok(book);
    }

    [HttpGet("GetBook/{bookid:int}")]
    public async Task<IActionResult> GetBook(int bookid)
    {
        // Eager Loading
        var bookWithAuthor = await _context.Books.Include(b => b.Author)
                                                 .FirstOrDefaultAsync(b => b.Id == bookid);

        return Ok(bookWithAuthor);
    }

    [HttpGet("GetBooks")]
    public async Task<IActionResult> GetBooks([FromQuery] int bookid)
    {
        // Lazy Loading - assuming it is configured in DbContext
        var book = await _context.Books.FindAsync(bookid);
        return Ok(book);
    }
}
