using Sneakers.DTO.RequestModel;
using Sneakers.Models;

namespace Sneakers.Services.Interface
{
    public interface IBrandService
    {
        void AddBrand(BrandVM model, ref int errorCode, ref string message, string traceId);
        void UpdateBrand(BrandVM model, int id, ref int errorCode, ref string message, string traceId);
        void DeleteBrand(int id, ref int errorCode, ref bool brandExists, ref string message, string traceId);


    }
}
