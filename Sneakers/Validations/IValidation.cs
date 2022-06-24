using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sneakers.Validations
{
    public interface IValidation
    {
        int CheckErrorCode(int error);
    }
}
