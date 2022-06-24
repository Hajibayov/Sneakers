using Sneakers.DTO.RequestModel;
using Sneakers.Models;

namespace Sneakers.Services.Interface
{
    public interface IBrandService
    {
        void AddBrand(BrandVM model, ref int errorCode, ref string message, string traceId);
    }
}
