using LibraryManagement.Application.DTOs;
using LibraryManagement.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Infrastructure.Repositories;
[Authorize]

public class CategoriesController : Controller
{

    private readonly ICategoryRepository _repository;



    public CategoriesController(ICategoryRepository repository)
    {

        _repository = repository;
    }



    // GET: CATEGORYS
    public IActionResult Index()

    {  
        return View(_repository.GetAll());


}

    // GET: CATEGORYS/Details/5

    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = _repository.GetById(id.Value);
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // GET: CATEGORYS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CATEGORYS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CategoryDTO category)
    {
        if (ModelState.IsValid)
       {
            _repository.Add(category);
            
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    // GET: CATEGORYS/Edit/5
    
    public IActionResult Edit(int? id)
    {

    
        if (id == null)
        {
            return NotFound();
        }

        var category = _repository.GetById(id.Value);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    // POST: CATEGORYS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int? id, CategoryDTO category)

    {
        if (id != category.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _repository.Update(category);
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    // GET: CATEGORYS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = _repository.GetById(id.Value); if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // POST: CATEGORYS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int? id)

    {

        if (id == null)

        {

            return NotFound();

        }

        _repository.Delete(id.Value);
        return RedirectToAction(nameof(Index));
    }

    private bool CategoryExists(int? id)
    {

        return id.HasValue && _repository.GetById(id.Value) != null;
    }
}
