using LibraryManagement.Application.DTOs;

namespace LibraryManagement.Infrastructure.Repositories
{
    public interface ICategoryRepository
    {
        List<CategoryDTO> GetAll();

        CategoryDTO? GetById(int id);

        void Add(CategoryDTO category);

        void Update(CategoryDTO category);

        void Delete(int id);
    }
}