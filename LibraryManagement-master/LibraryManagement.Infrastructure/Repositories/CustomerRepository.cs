using LibraryManagement.Application.DTOs;
using LibraryManagement.Domain.Models;
using LibraryManagement.Infrastructure.Repositories;
using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{

    public class CustomerRepository : ICustomerRepository

    {

        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)

        {

            _context = context;

        }

        public List<CustomerDTO> GetAll()

        {

            return _context.Customers

                .Select(c => new CustomerDTO

                {

                    Id = c.Id,

                    Name = c.Name,

                    Email = c.Email,

                    Phone = c.Phone

                })

                .ToList();

        }

        public CustomerDTO? GetById(int id)

        {

            return _context.Customers

                .Where(c => c.Id == id)

                .Select(c => new CustomerDTO

                {

                    Id = c.Id,

                    Name = c.Name,

                    Email = c.Email,

                    Phone = c.Phone

                })

                .FirstOrDefault();

        }

        public void Add(CustomerDTO customer)

        {

            var entity = new Customer

            {

                Name = customer.Name,

                Email = customer.Email,

                Phone = customer.Phone

            };

            _context.Customers.Add(entity);

            _context.SaveChanges();

        }

        public void Update(CustomerDTO customer)

        {

            var entity = _context.Customers.Find(customer.Id);

            if (entity == null) return;

            entity.Name = customer.Name;

            entity.Email = customer.Email;

            entity.Phone = customer.Phone;

            _context.SaveChanges();

        }

        public void Delete(int id)

        {

            var entity = _context.Customers.Find(id);

            if (entity == null) return;

            _context.Customers.Remove(entity);

            _context.SaveChanges();

        }

    }

}