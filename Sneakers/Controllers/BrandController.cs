using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sneakers.Models.ViewModels;
using Sneakers.Services;

namespace Sneakers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        public BrandService _brandService;
        public BrandController(BrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost ("add-brand")]
        public IActionResult AddBrand([FromBody] BrandVM brand)
        {
            _brandService.AddBrand(brand);
            return Ok();

        }
    }
}
