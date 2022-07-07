using Sneakers.Infrastructure.Repository;
using Sneakers.Models;
using Sneakers.Services.Interface;
using System.Collections.Generic;

namespace Sneakers.Services.Implementation
{
    public class LookupService : ILookupService
    {
        private readonly IRepository<SNEAKERS_BRAND> _brands;
        private readonly IRepository<SNEAKERS_MODEL> _models;
        private readonly IRepository<SNEAKERS_TYPE> _types;
        private readonly IRepository<SIZE> _sizes;


        public LookupService(
          IRepository<SNEAKERS_BRAND> brands,
          IRepository<SNEAKERS_MODEL> models,
          IRepository<SNEAKERS_TYPE> types,
          IRepository<SIZE> sizes
          )
        {
            _brands = brands;
            _models = models;
            _types = types;
            _sizes = sizes;
        }
        public IEnumerable<SNEAKERS_BRAND> GetBrands()
        {
            return _brands.AllQuery;
        }

        public IEnumerable<SNEAKERS_MODEL> GetModels()
        {
            return _models.AllQuery;
        }

        public IEnumerable<SNEAKERS_TYPE> GetTypes()
        {
            return _types.AllQuery;
        }

        public IEnumerable<SIZE> GetSizes()
        {
            return _sizes.AllQuery;
        }
    }
}
