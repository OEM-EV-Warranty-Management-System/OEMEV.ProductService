using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;

namespace OEMEV_ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetVehicles()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleDto>> GetVehicle(long id)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
                return Ok(vehicle);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("vin/{vin}")]
        public async Task<ActionResult<VehicleDto>> GetVehicleByVIN(string vin)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleByVINAsync(vin);
                return Ok(vehicle);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<VehicleDto>> CreateVehicle(CreateVehicleDto createVehicleDto)
        {
            try
            {
                var vehicle = await _vehicleService.CreateVehicleAsync(createVehicleDto);
                return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.Id }, vehicle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(long id, UpdateVehicleDto updateVehicleDto)
        {
            try
            {
                await _vehicleService.UpdateVehicleAsync(id, updateVehicleDto);
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
        public async Task<IActionResult> DeleteVehicle(long id)
        {
            try
            {
                var result = await _vehicleService.DeleteVehicleAsync(id);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetVehiclesByCustomer(Guid customerId)
        {
            var vehicles = await _vehicleService.GetVehiclesByCustomerAsync(customerId);
            return Ok(vehicles);
        }

        [HttpGet("manufacture/{manufactureId}")]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetVehiclesByManufacture(long manufactureId)
        {
            var vehicles = await _vehicleService.GetVehiclesByManufactureAsync(manufactureId);
            return Ok(vehicles);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(long id, [FromBody] string status)
        {
            try
            {
                await _vehicleService.UpdateVehicleStatusAsync(id, status);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}