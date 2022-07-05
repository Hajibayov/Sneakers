using Sneakers.DTO.RequestModel;
using Sneakers.DTO.ResponseModel.Inner;
using System.Collections.Generic;

namespace Sneakers.Services.Interface
{
    public interface ITypeService
    {
        List<TYPE_VIEW_MODEL> GetTypes(int skip, int limit, ref decimal totalCount, bool isExport, ref int errorCode, ref string message, string traceId);
        TypeVM GetType(int id, ref int errorCode, ref string message, string traceId);

        void AddType(TypeVM type, ref int errorCode, ref string message, string traceId);
        void UpdateType(TypeVM type, int id, ref int errorCode, ref string message, string traceId);
        void DeleteType(int id, ref int errorCode, ref bool typeExists, ref string message, string traceId);
    }
}
