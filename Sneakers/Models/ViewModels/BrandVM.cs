using System.ComponentModel.DataAnnotations.Schema;

namespace Sneakers.Models.ViewModels
{
    public class BrandVM
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("BRAND")]
        public string Brand { get; set; }
    }
}
