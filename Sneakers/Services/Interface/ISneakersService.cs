using Sneakers.DTO.RequestModel;

namespace Sneakers.Services.Interface
{
    public interface ISneakersService
    {
        void AddSneakers(SneakersVM sneaker, ref int errorCode, ref string message, string traceId);
    }
}
