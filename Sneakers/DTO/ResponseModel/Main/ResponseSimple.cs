using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sneakers.DTO.HelperModels;

namespace Sneakers.DTO.ResponseModels.Main
{
    public class ResponseSimple
    {
        public Status Status { get; set; }

        public string TraceID { get; set; }
    }
}
