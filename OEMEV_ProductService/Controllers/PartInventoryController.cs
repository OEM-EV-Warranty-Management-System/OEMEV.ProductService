using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;

namespace OEMEV_ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartInventoryController : ControllerBase
    {
        private readonly IPartInventoryService _partInventoryService;

        public PartInventoryController(IPartInventoryService partInventoryService)
        {
            _partInventoryService = partInventoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartInventoryDto>>> GetInventories()
        {
            var inventories = await _partInventoryService.GetAllInventoriesAsync();
            return Ok(inventories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PartInventoryDto>> GetInventory(long id)
        {
            try
            {
                var inventory = await _partInventoryService.GetInventoryByIdAsync(id);
                return Ok(inventory);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<PartInventoryDto>> CreateInventory(CreatePartInventoryDto createInventoryDto)
        {
            try
            {
                var inventory = await _partInventoryService.CreateInventoryAsync(createInventoryDto);
                return CreatedAtAction(nameof(GetInventory), new { id = inventory.Id }, inventory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventory(long id, UpdatePartInventoryDto updateInventoryDto)
        {
            try
            {
                await _partInventoryService.UpdateInventoryAsync(id, updateInventoryDto);
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
        public async Task<IActionResult> DeleteInventory(long id)
        {
            try
            {
                var result = await _partInventoryService.DeleteInventoryAsync(id);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("part/{partId}")]
        public async Task<ActionResult<IEnumerable<PartInventoryDto>>> GetInventoryByPart(long partId)
        {
            var inventories = await _partInventoryService.GetInventoryByPartIdAsync(partId);
            return Ok(inventories);
        }

        [HttpGet("service-center/{serviceCenterId}")]
        public async Task<ActionResult<IEnumerable<PartInventoryDto>>> GetInventoryByServiceCenter(long serviceCenterId)
        {
            var inventories = await _partInventoryService.GetInventoryByServiceCenterAsync(serviceCenterId);
            return Ok(inventories);
        }

        [HttpPatch("{id}/quantity")]
        public async Task<IActionResult> UpdateQuantity(long id, [FromBody] int quantity)
        {
            try
            {
                await _partInventoryService.UpdateInventoryQuantityAsync(id, quantity);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}