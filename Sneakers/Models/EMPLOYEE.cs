using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sneakers.Models
{
    public class EMPLOYEE
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("SURNAME")]
        public string Surname { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("PHONE_NUMBER")]
        public string PhoneNumber { get; set; } 

        [Column("WORK_ENTER")]
        public DateTime WorkEnter { get; set; }
    }
}
