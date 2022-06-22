using System.ComponentModel.DataAnnotations.Schema;

namespace Sneakers.Models
{
    public class SNEAKERS_BRAND
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("BRAND")]
        public string Brand { get; set; }
    }
}
