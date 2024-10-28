using Microsoft.AspNetCore.Mvc;
using LibraryContext;
using Microsoft.EntityFrameworkCore;

public class BookViewComponent : ViewComponent
{
    private readonly LibraryContext_Db _context;

    public BookViewComponent(LibraryContext_Db context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync(int bookid)
    {
        var book = await _context.Books.Include(b => b.Author)
                                       .FirstOrDefaultAsync(b => b.Id == bookid);
        return View(book);
    }
}
