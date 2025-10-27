using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;

namespace OEMEV_ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclePartsController : ControllerBase
    {
        private readonly IVehiclePartService _vehiclePartService;

        public VehiclePartsController(IVehiclePartService vehiclePartService)
        {
            _vehiclePartService = vehiclePartService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehiclePartDto>>> GetVehicleParts()
        {
            var vehicleParts = await _vehiclePartService.GetAllVehiclePartsAsync();
            return Ok(vehicleParts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehiclePartDto>> GetVehiclePart(long id)
        {
            try
            {
                var vehiclePart = await _vehiclePartService.GetVehiclePartByIdAsync(id);
                return Ok(vehiclePart);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<VehiclePartDto>> CreateVehiclePart(CreateVehiclePartDto createVehiclePartDto)
        {
            try
            {
                var vehiclePart = await _vehiclePartService.CreateVehiclePartAsync(createVehiclePartDto);
                return CreatedAtAction(nameof(GetVehiclePart), new { id = vehiclePart.Id }, vehiclePart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehiclePart(long id, UpdateVehiclePartDto updateVehiclePartDto)
        {
            try
            {
                await _vehiclePartService.UpdateVehiclePartAsync(id, updateVehiclePartDto);
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
        public async Task<IActionResult> DeleteVehiclePart(long id)
        {
            try
            {
                var result = await _vehiclePartService.DeleteVehiclePartAsync(id);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("vehicle/{vehicleId}")]
        public async Task<ActionResult<IEnumerable<VehiclePartDto>>> GetVehiclePartsByVehicle(long vehicleId)
        {
            var vehicleParts = await _vehiclePartService.GetVehiclePartsByVehicleIdAsync(vehicleId);
            return Ok(vehicleParts);
        }

        [HttpGet("part/{partId}")]
        public async Task<ActionResult<IEnumerable<VehiclePartDto>>> GetVehiclePartsByPart(long partId)
        {
            var vehicleParts = await _vehiclePartService.GetVehiclePartsByPartIdAsync(partId);
            return Ok(vehicleParts);
        }

        [HttpGet("serial/{serialNumber}")]
        public async Task<ActionResult<VehiclePartDto>> GetVehiclePartBySerial(string serialNumber)
        {
            try
            {
                var vehiclePart = await _vehiclePartService.GetVehiclePartBySerialNumberAsync(serialNumber);
                return Ok(vehiclePart);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}