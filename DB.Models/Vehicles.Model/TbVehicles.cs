using DB.Models.Parking.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DB.Models.Vehicles.Model;

public class TbVehicles
{
  public int VehicleId { get; set; }
  public string Name { get; set; } = null!;
  public string LicensePlate { get; set; } = null!;
  public string Phone { get; set; } = null!;
  public int VehicleTypeId { get; set; } 
  public int Height { get; set; }
  public int Width { get; set; }
  public int Length { get; set; }
  public int TicketTypeId { get; set; } 
  public DateTime CheckInTime { get; set; }

  //public ICollection<TbParkingAssignments> ParkingAssignments { get; set; } = new List<TbParkingAssignments>();

}
public class TbVehiclesConfiguration : IEntityTypeConfiguration<TbVehicles>
{
  public void Configure(EntityTypeBuilder<TbVehicles> builder)
  {
    builder.ToTable("TbVehicles");

    builder.HasKey(e => e.VehicleId);

    builder.Property(e => e.Name).HasMaxLength(100);
    builder.Property(e => e.LicensePlate).HasMaxLength(100);
    builder.Property(e => e.Phone).HasMaxLength(20);
  }
}
