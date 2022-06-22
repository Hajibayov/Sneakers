using System.ComponentModel.DataAnnotations.Schema;

namespace Sneakers.Models
{
    public class SIZE_SNEAKERS_CONNECTION
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("SNEAKERS_ID")]
        public int SneakersId { get; set; }

        [Column("SIZE_ID")]
        public int SizeId { get; set; }

        [Column("QUANTITY")]
        public int Quantity { get; set; }

        public virtual SNEAKERS Sneakers { get; set; }
        public virtual SIZE Size { get; set; }


    }
}
