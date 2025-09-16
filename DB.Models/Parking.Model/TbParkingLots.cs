using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DB.Models.Parking.Model;

public class TbParkingLots
{
  public int ParkingLotId { get; set; }
  public int TicketTypeId { get; set; } 
  public bool IsOccupied { get; set; }

  //public ICollection<TbParkingAssignments> ParkingAssignments { get; set; } = new List<TbParkingAssignments>();
}

public class TbParkingLotsConfiguration : IEntityTypeConfiguration<TbParkingLots>
{
  public void Configure(EntityTypeBuilder<TbParkingLots> builder)
  {
    builder.ToTable("TbParkingLots");

    builder.HasKey(e => e.ParkingLotId);
  }
}
