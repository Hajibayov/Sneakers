using Sneakers.DTO.RequestModel;
using Sneakers.DTO.ResponseModel.Inner;
using System.Collections.Generic;

namespace Sneakers.Services.Interface
{
    public interface ISizeService
    {
        List<SIZE_VIEW_MODEL> GetSizes(int skip, int limit, ref decimal totalCount, bool isExport, ref int errorCode, ref string message, string traceId);
        SizeVM GetSize(int id, ref int errorCode, ref string message, string traceId);

        void AddSize(SizeVM size, ref int errorCode, ref string message, string traceId);
        void UpdateSize(SizeVM size, int id, ref int errorCode, ref string message, string traceId);

        void DeleteSize(int id, ref int errorCode, ref bool brandExists, ref string message, string traceId);
    }
}
