using LibraryManagement.Domain.Models;

namespace LibraryManagement.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        User? GetByEmail(string email);

        User? GetByUsername(string username);

        void Add(User user);
    }
}