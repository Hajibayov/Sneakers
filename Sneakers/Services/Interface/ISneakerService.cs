using Sneakers.DTO.RequestModel;
using Sneakers.DTO.ResponseModel.Inner;
using System.Collections.Generic;

namespace Sneakers.Services.Interface
{
    public interface ISneakerService
    {
        void AddSneakers(SneakersVM sneaker, ref int errorCode, ref string message, string traceId);
        List<SNEAKER_VIEW_MODEL> GetSneakers(SNEAKER_FILTER_VIEW_MODEL model, int skip, int limit, ref decimal totalCount, bool isExport, ref int errorCode, ref string message, string TraceId);
        SneakersVM GetSneaker(int id, ref int errorCode, ref string message, string traceId);
        void UpdateSneaker(SneakersVM sneaker, int id, ref int errorCode, ref string message, string traceId);
        void DeleteSneaker(int id, ref int errorCode, ref string message, string traceId);
    }
}
