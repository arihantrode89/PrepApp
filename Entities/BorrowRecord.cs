using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class BorrowRecord
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("BookId")]
        public int BookId { get; set; }
        public Book Book { get; set; }
        [ForeignKey("MemberId")]
        public int MemberId { get; set; }
        public Member Member { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set;}
    }
}
