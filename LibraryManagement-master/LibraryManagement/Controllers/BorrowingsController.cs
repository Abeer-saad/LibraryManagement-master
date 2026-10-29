using LibraryManagement.Application.DTOs;
using LibraryManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{

    [Authorize]

    public class BorrowingsController : Controller

    {

        private readonly IBorrowingRepository _repository;

        public BorrowingsController(IBorrowingRepository repository)

        {

            _repository = repository;

        }

        public IActionResult Index()

        {

            return View(_repository.GetAll());

        }

        public IActionResult Details(int? id)

        {

            if (id == null)

                return NotFound();

            var borrowing = _repository.GetById(id.Value);

            if (borrowing == null)

                return NotFound();

            return View(borrowing);

        }

        public IActionResult Create()

        {

            return View();

        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        public IActionResult Create(BorrowingDTO borrowing)

        {

            if (ModelState.IsValid)

            {

                _repository.Add(borrowing);

                return RedirectToAction(nameof(Index));

            }

            return View(borrowing);

        }

        public IActionResult Edit(int? id)

        {

            if (id == null)

                return NotFound();

            var borrowing = _repository.GetById(id.Value);

            if (borrowing == null)

                return NotFound();

            return View(borrowing);

        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        public IActionResult Edit(int id, BorrowingDTO borrowing)

        {

            if (id != borrowing.Id)

                return NotFound();

            if (ModelState.IsValid)

            {

                _repository.Update(borrowing);

                return RedirectToAction(nameof(Index));

            }

            return View(borrowing);

        }

        public IActionResult Delete(int? id)

        {

            if (id == null)

                return NotFound();

            var borrowing = _repository.GetById(id.Value);

            if (borrowing == null)

                return NotFound();

            return View(borrowing);

        }

        [HttpPost, ActionName("Delete")]

        [ValidateAntiForgeryToken]

        public IActionResult DeleteConfirmed(int? id)

        {

            if (id == null)

                return NotFound();

            var borrowing = _repository.GetById(id.Value);

            if (borrowing == null)

                return NotFound();

            _repository.Delete(id.Value);

            return RedirectToAction(nameof(Index));

        }
    }
}