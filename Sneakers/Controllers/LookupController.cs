using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sneakers.Services.Interface;
using System;

namespace Sneakers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILookupService _lookupService;

        public LookupController(IMapper mapper, ILookupService lookupService)
        {
            _mapper = mapper;
            _lookupService = lookupService;
        }

        [HttpGet, Route("brands")]
        public IActionResult Brand()
        {
            var currentUser = HttpContext.User;
            bool currentUserRole = Convert.ToBoolean(currentUser.FindFirst("UserRole").Value);

            if (!currentUserRole)
            {
                return Unauthorized();
            }

            return Ok(new { Result = _lookupService.GetBrands() });
        }

        [HttpGet, Route("types")]
        public IActionResult Type()
        {
            var currentUser = HttpContext.User;
            bool currentUserRole = Convert.ToBoolean(currentUser.FindFirst("UserRole").Value);

            if (!currentUserRole)
            {
                return Unauthorized();
            }

            return Ok(new { Result = _lookupService.GetTypes() });
        }

        [HttpGet, Route("sizes")]
        public IActionResult Size()
        {
            var currentUser = HttpContext.User;
            bool currentUserRole = Convert.ToBoolean(currentUser.FindFirst("UserRole").Value);

            if (!currentUserRole)
            {
                return Unauthorized();
            }

            return Ok(new { Result = _lookupService.GetSizes() });
        }

        [HttpGet, Route("models")]
        public IActionResult Model()
        {
            var currentUser = HttpContext.User;
            bool currentUserRole = Convert.ToBoolean(currentUser.FindFirst("UserRole").Value);

            if (!currentUserRole)
            {
                return Unauthorized();
            }

            return Ok(new { Result = _lookupService.GetModels() });
        }

    }
}
