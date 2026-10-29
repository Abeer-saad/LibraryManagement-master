using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Models;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Infrastructure.Repositories;
[Authorize]
public class BooksController : Controller
{


    private readonly IBookRepository _repository;

    public BooksController(IBookRepository repository)

    {

        _repository = repository;

    }

    // GET: BOOKS

    public IActionResult Index()

    {

        var books = _repository.GetAll();

        return View(books);

    }
    // GET: BOOKS/Details/5
    public IActionResult Details(int? id)

    {

        if (id == null)

        {

            return NotFound();

        }

        var book = _repository.GetById(id.Value);

        if (book == null)

        {

            return NotFound();

        }

        return View(book);

    }



    // GET: BOOKS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: BOOKS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]

    public IActionResult Create(BookDTO book)

    {

        if (ModelState.IsValid)

        {

            _repository.Add(book);

            return RedirectToAction(nameof(Index));

        }

        return View(book);

    }


    // GET: BOOKS/Edit/5

    public IActionResult Edit(int? id)

    {

        if (id == null)

        {

            return NotFound();

        }

        var book = _repository.GetById(id.Value);

        if (book == null)

        {

            return NotFound();

        }

        return View(book);

    }

    // POST: BOOKS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]

    public IActionResult Edit(int? id, BookDTO book)

    {

        if (id == null || id != book.Id)

        {

            return NotFound();

        }

        if (ModelState.IsValid)

        {

            if (_repository.GetById(book.Id) == null)

            {

                return NotFound();

            }

            _repository.Update(book);

            return RedirectToAction(nameof(Index));

        }

        return View(book);

    }


    // GET: BOOKS/Delete/5

    public IActionResult Delete(int? id)

    {

        if (id == null)

        {

            return NotFound();

        }

        var book = _repository.GetById(id.Value);

        if (book == null)

        {

            return NotFound();

        }

        return View(book);

    }



    // POST: BOOKS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int? id)

    {

        if (id == null)

        {

            return NotFound();

        }

        var book = _repository.GetById(id.Value);

        if (book == null)

        {

            return NotFound();

        }

        _repository.Delete(id.Value);

        return RedirectToAction(nameof(Index));

    }
}
