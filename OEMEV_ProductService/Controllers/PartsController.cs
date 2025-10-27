using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;

namespace OEMEV_ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartsController : ControllerBase
    {
        private readonly IPartService _partService;

        public PartsController(IPartService partService)
        {
            _partService = partService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartDto>>> GetParts()
        {
            var parts = await _partService.GetAllPartsAsync();
            return Ok(parts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PartDto>> GetPart(long id)
        {
            try
            {
                var part = await _partService.GetPartByIdAsync(id);
                return Ok(part);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<PartDto>> CreatePart(CreatePartDto createPartDto)
        {
            try
            {
                var part = await _partService.CreatePartAsync(createPartDto);
                return CreatedAtAction(nameof(GetPart), new { id = part.Id }, part);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePart(long id, UpdatePartDto updatePartDto)
        {
            try
            {
                await _partService.UpdatePartAsync(id, updatePartDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePart(long id)
        {
            try
            {
                var result = await _partService.DeletePartAsync(id);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("type/{type}")]
        public async Task<ActionResult<IEnumerable<PartDto>>> GetPartsByType(string type)
        {
            var parts = await _partService.GetPartsByTypeAsync(type);
            return Ok(parts);
        }

        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<PartDto>>> SearchParts(string name)
        {
            var parts = await _partService.SearchPartsByNameAsync(name);
            return Ok(parts);
        }
    }
}