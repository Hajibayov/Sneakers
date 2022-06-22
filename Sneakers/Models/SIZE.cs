using System.ComponentModel.DataAnnotations.Schema;

namespace Sneakers.Models
{
    public class SIZE
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("SIZE")]
        public string Size { get; set; }
    }
}
