using System.Drawing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Parking.Model;
using Services.Services;

namespace ParkingGarage.Controllers;

[Route("[controller]")]
[ApiController]
public class ParkingController : ControllerBase
{
  private readonly ParkingService _service;

  public ParkingController(ParkingService service)
  {
    _service = service;
  }

  [HttpPost("checkIn")]
  public async Task<CheckInResultResponse> CheckIn(CheckInRequest checkInRequest) =>
    await _service.CheckInAsync(checkInRequest);

  [HttpPost("checkOut")]
  public async Task<CheckOutResultResponse> CheckOut( string licensePlate)=>
    await _service.CheckOutAsync(licensePlate);

  [HttpGet("GetLotState")]
  public async Task<ParkingLotStateResponse> GetLotState( int parkingLotId)=>
    await _service.GetParkingLotStateAsync(parkingLotId);

  [HttpGet("GetGarageState")]
  public async Task<GarageStateResponse> GetGarageState()=>
    await _service.GetGarageStateAsync();

  [HttpGet("vehiclesByTicket")]
  public VehicleByTicketListResponse GetVehiclesByTicket(int? ticketTypeId)=>
     _service.GetVehicleByTicket(ticketTypeId);

  [HttpPost("RandomCheckIn")]
  public async Task<List<CheckInResultResponse>> RandomCheckIn() =>
     await _service.CheckInRandomVehiclesAsync();

}
