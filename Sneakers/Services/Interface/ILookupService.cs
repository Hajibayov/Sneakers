using Sneakers.Models;
using System.Collections.Generic;

namespace Sneakers.Services.Interface
{
    public interface ILookupService
    {
        public IEnumerable<SNEAKERS_BRAND> GetBrands();

        public IEnumerable<SNEAKERS_TYPE> GetTypes();

        public IEnumerable<SNEAKERS_MODEL> GetModels();

        public IEnumerable<SIZE> GetSizes();


    }
}
