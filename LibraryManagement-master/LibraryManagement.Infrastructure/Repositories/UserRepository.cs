using System.Linq;
using LibraryManagement.Domain.Models;
using LibraryManagement.Models;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User? GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(
                user => user.Email == email);
        }

        public User? GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(
                user => user.Username == username);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}