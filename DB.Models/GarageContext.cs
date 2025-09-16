using DB.Models.Aggregators;
using DB.Models.Parking.Model;
using DB.Models.Vehicles.Model;
using Microsoft.EntityFrameworkCore;

namespace DB.Models;

public partial class GarageContext : DbContext
{
  public GarageContext(DbContextOptions<GarageContext> options) : base(options) { }

  public virtual DbSet<TbParkingLots> TbParkingLots { get; set; } = null!;
  public virtual DbSet<TbParkingAssignments> TbParkingAssignments { get; set; } = null!;
  public virtual DbSet<TbVehicles> TbVehicles { get; set; } = null!;
  protected virtual DbSet<VehicleByTicketAggregator> VehicleByTicketAggregators { get; set; } = null!;


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    new TbParkingLotsConfiguration().Configure(modelBuilder.Entity<TbParkingLots>());
    new TbParkingAssignmentsConfiguration().Configure(modelBuilder.Entity<TbParkingAssignments>());
    new TbVehiclesConfiguration().Configure(modelBuilder.Entity<TbVehicles>());
    modelBuilder.Entity<VehicleByTicketAggregator>().HasNoKey();

  }
  public List<VehicleByTicketAggregator> GetVehicleByTicketAggregators(int? TicketTypeId)
  {
    var query = @"EXEC ParkingGarage.dbo.GetVehiclesByTicketType @TicketTypeId={0}";

    return VehicleByTicketAggregators
        .FromSqlRaw(query,
         TicketTypeId ?? (object)DBNull.Value)
        .AsNoTracking().ToList();
  }
}

