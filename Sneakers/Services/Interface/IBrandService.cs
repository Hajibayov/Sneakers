using Sneakers.DTO.RequestModel;
using Sneakers.Models;
using System.Collections.Generic;
using TeamControlV2.DTO.ResponseModels.Inner;

namespace Sneakers.Services.Interface
{
    public interface IBrandService
    {
        List<BRAND_VIEW_MODEL> GetBrands(int skip, int limit, ref decimal totalCount, bool isExport, ref int errorCode, ref string message, string traceId);
        BrandVM GetBrand(int id, ref int errorCode, ref string message, string traceId);

        void AddBrand(BrandVM model, ref int errorCode, ref string message, string traceId);
        void UpdateBrand(BrandVM model, int id, ref int errorCode, ref string message, string traceId);
        void DeleteBrand(int id, ref int errorCode, ref bool brandExists, ref string message, string traceId);


    }
}
