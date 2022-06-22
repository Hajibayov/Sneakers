using System.ComponentModel.DataAnnotations.Schema;

namespace Sneakers.Models
{
    public class WAREHOUSE
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("LOCATION")]
        public string Location { get; set; }

        [Column("CAPACITY")]
        public string Capacity { get; set; }

        [Column("ZIP")]
        public string Zip { get; set; }
    }
}
