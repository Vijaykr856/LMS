using LMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers
{

    //[Route("/books")]
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // List all books
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books.ToListAsync();
            return View(books);
        }

        // Add a book
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // Edit a book
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (ModelState.IsValid)
            {
                var existingBook = await _context.Books.FindAsync(id);
                if (existingBook == null)
                {
                    return NotFound();
                }

                // Update the properties of the existing book
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Genre = book.Genre;
                existingBook.TotalCopies = book.TotalCopies;
                existingBook.AvailableCopies = book.AvailableCopies;

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(book);
        }


        // Delete a book
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Search for a book
        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var books = await _context.Books
                .Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm))
                .ToListAsync();
            return View("Search", books);
        }


        #region issue-book


        [HttpGet]
        public async Task<IActionResult> Issue()
        {
            var availableBooks = await _context.Books
      .Where(b => b.AvailableCopies > 0)
      .ToListAsync();

            // Pass the available books to the view using ViewBag or ViewModel
            ViewBag.AvailableBooks = new SelectList(availableBooks, "BookId", "Title");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> IssuedBook()
        {

            var issuedBooks = await _context.IssuedBooks
                .Include(ib => ib.Book) // Include book details for display
                .Where(ib => ib.ReturnDate == null) // Only show books that are not returned
                .ToListAsync();

            return View(issuedBooks);

        }

        [HttpPost]
        public async Task<IActionResult> Issue(int bookId, string issuedTo)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null || book.AvailableCopies <= 0)
            {
                return NotFound("Book not available.");
            }

            var issuedBook = new IssuedBook
            {
                BookId = bookId,
                IssuedTo = issuedTo,
                IssueDate = DateTime.UtcNow
            };

            book.AvailableCopies -= 1;

            _context.IssuedBooks.Add(issuedBook);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Books");
        }


        public async Task<IActionResult> Return()
        {
            var issuedBooks = await _context.IssuedBooks
                .Include(ib => ib.Book) // Include book details for display
                .Where(ib => ib.ReturnDate == null) // Only show books that are not returned
                .ToListAsync();
            return View(issuedBooks);
        }
        // Return a book
        [HttpPost]
        public async Task<IActionResult> Return(int id)
        {
            var issuedBook = await _context.IssuedBooks.FindAsync(id);
            if (issuedBook == null || issuedBook.ReturnDate != null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(issuedBook.BookId);
            if (book != null)
            {
                book.AvailableCopies += 1;
            }

            issuedBook.ReturnDate = DateTime.UtcNow;

            _context.Update(issuedBook);
            await _context.SaveChangesAsync();

            return RedirectToAction("IssuedBook");
        }

        #endregion

    }


}

