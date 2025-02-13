using AppStore.Models.Domain;
using AppStore.Models.DTO;
using AppStore.Repositories.Abstract;

namespace AppStore.Repositories.Implementation

{
    public class BookService : IBookService
    {
        private readonly DatabaseContext _context;

        public BookService(DatabaseContext contextParam)
        {
            _context = contextParam;
        }

        public bool AddBook(Book book)
        {
            try
            {
                _context.Books!.Add(book);
                _context.SaveChanges();
                foreach (var categoryId in book.Categories!)
                {
                    var categoryBook = new CategoryBook
                    {
                        BookId = book.Id,
                        CategoryId = categoryId
                    };
                    _context.CategoryBooks!.Add(categoryBook);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateBook(Book book)
        {
            try
            {
                var categoriesToRemove = _context.CategoryBooks!.Where(x => x.BookId == book.Id);
                _context.CategoryBooks!.RemoveRange(categoriesToRemove);
                foreach (var categoryId in book.Categories!)
                {
                    var categoryBook = new CategoryBook
                    {
                        BookId = book.Id,
                        CategoryId = categoryId
                    };
                    _context.CategoryBooks!.Add(categoryBook);
                }
                _context.Books!.Update(book);
                _context.SaveChanges();
                return true;

                //Example without RemoveRange
                // var categoriesToRemove = _context.CategoryBooks!.Where(x => x.BookId == book.Id).ToList();
                // foreach (var categoryBook in categoriesToRemove)
                // {
                //     _context.CategoryBooks!.Remove(categoryBook);
                // }
            
                
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteBook(int id)
        {
            try
            {
                var book = GetBookById(id);
                if (book == null) 
                    return false;
                var categoryBooks = _context.CategoryBooks!.Where(x => x.BookId == id).ToList();
                _context.CategoryBooks!.RemoveRange(categoryBooks);
                _context.Books!.Remove(book);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Book GetBookById(int id)
        {
            return _context.Books!.Find(id)!;
        }

        public BookListVm List(string term = "", bool pagination = false, int currentPage = 0)
        {
            var data = new BookListVm();
            var list = _context.Books!.ToList();
            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(x => x.Title!.ToLower().StartsWith(term)).ToList();
            }
            if (pagination)
            {
                int pageSize = 5;
                int count = list.Count;
                int totalPage = (int)Math.Ceiling((double)count / pageSize);
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.TotalPages = totalPage;
                data.CurrentPage = currentPage;
                
            }
            
            foreach (var book in list)
            {
                var categories = (
                    from Category in _context.Categories
                    join lc in _context.CategoryBooks! 
                    on Category.Id equals lc.CategoryId
                    where lc.BookId == book.Id
                    select Category.Name
                ).ToList();
                string categoryNames = string.Join(", ", categories);
                book.CategoryNames = categoryNames;
            }
            data.BookList = list.AsQueryable();
            
            return data;
        }

        public List<int> GetCategoryByBookId(int BookId)
        {
            return _context.CategoryBooks!.Where(x => x.BookId == BookId).Select(x => x.CategoryId).ToList();
        }
    }
}