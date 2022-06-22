using System.ComponentModel.DataAnnotations.Schema;

namespace Sneakers.Models
{
    public class SNEAKERS_TYPE
    {
        
        [Column("ID")]
        public int Id { get; set; }

        [Column("TYPE")]
        public string Type { get; set; }
    }
}
