using AppStore.Models.Domain;
using AppStore.Models.DTO;

namespace AppStore.Repositories.Abstract
{
    public interface IBookService
    {
        bool AddBook(Book book);
        bool UpdateBook(Book book);
        bool DeleteBook(int id);
        Book GetBookById(int id);
        BookListVm List(string term = "", bool pagination = false, int currentPage = 0);
        List<int> GetCategoryByBookId(int id);
    }
}