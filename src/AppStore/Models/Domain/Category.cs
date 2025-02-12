using System.ComponentModel.DataAnnotations;

namespace AppStore.Models.Domain;

public class Category
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public virtual ICollection<CategoryBook>? CategoryBooks_Relation { get; set; }
    public virtual ICollection<Book>? Books_Relation { get; set; }
}