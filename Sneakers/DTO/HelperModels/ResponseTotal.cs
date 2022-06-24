using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Sneakers.DTO.HelperModels
{
    public class ResponseTotal<T>
    {
        public List<T> Data { get; set; }

        public decimal Total { get; set; }


    }
}
