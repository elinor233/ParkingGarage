
using Models.Base.Models;
using Models.Parking.Model;
using Models.Ticket.Model;
namespace Models;

public class CheckOutResultResponse : ResponseBase<ResponseEnum, int > { }
public class CheckInResultResponse : ResponseBase<ResponseEnum, CheckInResult> { }
public class GarageStateResponse : ResponseBase<ResponseEnum, List<ParkingLots>> { }
public class ParkingLotStateResponse : ResponseBase<ResponseEnum, ParkingLots> { }
public class VehicleByTicketListResponse : ResponseBase<ResponseEnum, List<VehicleByTicket>> { }


