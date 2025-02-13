using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppStore.Models.Domain;

public class Book
{
    [Key]
    [Required]
    [MaxLength(200)]
    public int Id { get; set; }

    public string? Title { get; set; }
    public string? CreateDate { get; set; }
    public string? Image { get; set; }

    [Required]
    public string? Author { get; set; }

    public virtual ICollection<Category>? Category_Relation { get; set; }
    public virtual ICollection<CategoryBook>? CategoryBooks_Relation { get; set; }
    [NotMapped]
    public List<int>? Categories { get; set; }
    [NotMapped]
    public string CategoryNames { get; internal set; }
}
