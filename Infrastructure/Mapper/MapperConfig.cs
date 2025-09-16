using AutoMapper;
using DB.Models.Parking.Model;
using Models.Parking.Model;

namespace Infrastructure;

public class MapperConfig : Profile
{
  public MapperConfig()
  {
    CreateMap<TbParkingLots, ParkingLots>();
    CreateMap<ParkingLots, TbParkingLots>();
  }
}
