using System;
using Sneakers.DTO.HelperModels.Jwt;

namespace Sneakers.Services.Interface
{
    public interface IJwtHandler
    {
        JwtResponse CreateToken(JwtCustomClaims claims);
    }
}
