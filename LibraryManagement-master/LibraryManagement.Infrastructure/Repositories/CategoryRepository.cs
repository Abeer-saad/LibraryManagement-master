using LibraryManagement.Application.DTOs;
using LibraryManagement.Domain.Models;
using LibraryManagement.Infrastructure.Repositories;
using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {

            _context = context;

        }

        public List<CategoryDTO> GetAll()

        {

            return _context.Categories

                .Select(c => new CategoryDTO

                {

                    Id = c.Id,

                    Name = c.Name

                })

                .ToList();

        }

        public CategoryDTO? GetById(int id)

        {

            return _context.Categories

                .Where(c => c.Id == id)

                .Select(c => new CategoryDTO

                {

                    Id = c.Id,

                    Name = c.Name

                })

                .FirstOrDefault();

        }

        public void Add(CategoryDTO category)

        {

            var entity = new Category

            {

                Name = category.Name

            };

            _context.Categories.Add(entity);

            _context.SaveChanges();

        }

        public void Update(CategoryDTO category)

        {

            var entity = _context.Categories.Find(category.Id);

            if (entity == null) return;

            entity.Name = category.Name;

            _context.SaveChanges();

        }

        public void Delete(int id)

        {

            var entity = _context.Categories.Find(id);

            if (entity == null) return;

            _context.Categories.Remove(entity);

            _context.SaveChanges();

        }
    }
}