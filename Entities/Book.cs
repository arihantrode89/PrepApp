using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Book
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(50)]
        public string Author { get; set; }
        [Required]
        public int AvailableCopies { get; set; }
    }
}
