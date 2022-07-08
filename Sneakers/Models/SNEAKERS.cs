using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sneakers.Models
{
    public class SNEAKERS
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("TYPE_ID")]
        public int TypeId { get; set; }

        [Column("BRAND_ID")]
        public int BrandId { get; set; }

        [Column("MODEL_ID")]
        public int ModelId { get; set; }

        [Column("PRICE")]
        public int Price { get; set; }

        [Column("CREATED_AT")]
        public DateTime CreatedAt { get; set; }

        [Column("UPDATED_AT")]
        public DateTime? UpdatedAt { get; set; }

        [Column("CREATED_BY")]
        public int CreatedBy { get; set; }

        [Column("UPDATED_BY")]
        public int? UpdatedBy { get; set; }

        public virtual SNEAKERS_BRAND Brand { get; set; }
        public virtual SNEAKERS_MODEL Model{ get; set; }
        public virtual SNEAKERS_TYPE Type { get; set; }

    }
}
