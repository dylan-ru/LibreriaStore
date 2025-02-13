using AppStore.Models.Domain;

namespace AppStore.Models.DTO;

public class BookListVm{

    public IQueryable<Book>? BookList {get; set;}
    
    public int PageSize {get; set;}
    public int CurrentPage {get; set;}
    public int TotalPages {get; set;}
    public string? Term {get; set;}
}