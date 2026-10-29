using LibraryManagement.Application.DTOs;

namespace LibraryManagement.Infrastructure.Repositories
{

    public interface IBorrowingRepository
    {
        List<BorrowingDTO> GetAll();

        BorrowingDTO? GetById(int id);

        void Add(BorrowingDTO borrowing);

        void Update(BorrowingDTO borrowing);

        void Delete(int id);
    }
}