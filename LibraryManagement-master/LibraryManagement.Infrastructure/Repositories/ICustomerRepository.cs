using LibraryManagement.Application.DTOs;

namespace LibraryManagement.Infrastructure.Repositories
{
    public interface ICustomerRepository
    {
        List<CustomerDTO> GetAll();

        CustomerDTO? GetById(int id);

        void Add(CustomerDTO customer);

        void Update(CustomerDTO customer);

        void Delete(int id);
    }
}