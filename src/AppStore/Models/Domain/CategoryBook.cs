using System.ComponentModel.DataAnnotations;

namespace AppStore.Models.Domain;

public class CategoryBook
{
    [Key]
    [Required]
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public int BookId { get; set; }
    public Book? Book { get; set; }
}
