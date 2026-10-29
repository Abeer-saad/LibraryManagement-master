using LibraryManagement.Application.DTOs;
using LibraryManagement.Domain.Models;
using LibraryManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.API.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;

        private readonly PasswordHasher<User> _hasher = new();

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User Register(RegisterDTO data)
        {
            string email = data.Email.Trim().ToLowerInvariant();
            string username = data.Username.Trim();

            if (string.IsNullOrWhiteSpace(data.Name) ||
                string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException(
                    "Name and username are required");
            }

            if (_repository.GetByEmail(email) != null)
            {
                throw new ArgumentException(
                    "Email already registered");
            }

            if (_repository.GetByUsername(username) != null)
            {
                throw new ArgumentException(
                    "Username already registered");
            }

            var user = new User
            {
                Name = data.Name.Trim(),
                Email = email,
                Username = username
            };

            user.PasswordHash =
                _hasher.HashPassword(user, data.Password);

            _repository.Add(user);

            return user;
        }

        public User? Login(LoginDTO data)
        {
            string email = data.Email.Trim().ToLowerInvariant();

            var user = _repository.GetByEmail(email);

            if (user == null)
            {
                return null;
            }

            var result = _hasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                data.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return user;
        }
    }
}