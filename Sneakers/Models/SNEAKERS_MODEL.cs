using System.ComponentModel.DataAnnotations.Schema;

namespace Sneakers.Models
{
    public class SNEAKERS_MODEL
    {

        [Column("ID")]
        public int Id { get; set; }

        [Column("MODEL")]
        public string Model { get; set; }
    }
}
