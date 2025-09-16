using DB.Models.Vehicles.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DB.Models.Parking.Model;

public class TbParkingAssignments
{
  public int AssignmentId { get; set; }
  public int VehicleId { get; set; }
  public int ParkingLotId { get; set; }
  public DateTime CheckInTime { get; set; }
  public DateTime? CheckOutTime { get; set; }

}

public class TbParkingAssignmentsConfiguration : IEntityTypeConfiguration<TbParkingAssignments>
{
  public void Configure(EntityTypeBuilder<TbParkingAssignments> builder)
  {
    builder.ToTable("TbParkingAssignments");

    builder.HasKey(e => e.AssignmentId);
  }
}
