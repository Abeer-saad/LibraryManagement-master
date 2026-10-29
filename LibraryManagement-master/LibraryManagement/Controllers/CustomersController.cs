using LibraryManagement.Application.DTOs;
using LibraryManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers

{

    [Authorize]

    public class CustomersController : Controller

    {

        private readonly ICustomerRepository _repository;

        public CustomersController(ICustomerRepository repository)

        {

            _repository = repository;

        }

        // GET: Customers

        public IActionResult Index()

        {

            return View(_repository.GetAll());

        }

        // GET: Customers/Details/5

        public IActionResult Details(int? id)

        {

            if (id == null)

                return NotFound();

            var customer = _repository.GetById(id.Value);

            if (customer == null)

                return NotFound();

            return View(customer);

        }

        // GET: Customers/Create

        public IActionResult Create()

        {

            return View();

        }

        // POST: Customers/Create

        [HttpPost]

        [ValidateAntiForgeryToken]

        public IActionResult Create(CustomerDTO customer)

        {

            if (ModelState.IsValid)

            {

                _repository.Add(customer);

                return RedirectToAction(nameof(Index));

            }

            return View(customer);

        }

        // GET: Customers/Edit/5

        public IActionResult Edit(int? id)

        {

            if (id == null)

                return NotFound();

            var customer = _repository.GetById(id.Value);

            if (customer == null)

                return NotFound();

            return View(customer);

        }

        // POST: Customers/Edit/5

        [HttpPost]

        [ValidateAntiForgeryToken]

        public IActionResult Edit(int id, CustomerDTO customer)

        {

            if (id != customer.Id)

                return NotFound();

            if (ModelState.IsValid)

            {

                _repository.Update(customer);

                return RedirectToAction(nameof(Index));

            }

            return View(customer);

        }

        // GET: Customers/Delete/5

        public IActionResult Delete(int? id)

        {

            if (id == null)

                return NotFound();

            var customer = _repository.GetById(id.Value);

            if (customer == null)

                return NotFound();

            return View(customer);

        }

        // POST: Customers/Delete/5

        [HttpPost, ActionName("Delete")]

        [ValidateAntiForgeryToken]

        public IActionResult DeleteConfirmed(int? id)

        {

            if (id == null)

                return NotFound();

            var customer = _repository.GetById(id.Value);

            if (customer == null)

                return NotFound();

            _repository.Delete(id.Value);

            return RedirectToAction(nameof(Index));

        }
    }
}