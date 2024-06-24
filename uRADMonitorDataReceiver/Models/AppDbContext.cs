using Microsoft.EntityFrameworkCore;

namespace uRADMonitorDataReceiver.Models;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<DataReceiver> DataReceivers { get; set; }
}
