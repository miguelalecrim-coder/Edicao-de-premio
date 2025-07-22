using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;


public class AbsanteeContext : DbContext
{
    public virtual DbSet<EdicaoDataModel> Edicao { get; set; }

    public virtual DbSet<EdicaoTemporaryDataModel> EdicaoTemporary { get; set; }

    public DbSet<UserDataModel> ValidUserIds { get; set; }


    public AbsanteeContext(DbContextOptions<AbsanteeContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        base.OnModelCreating(modelBuilder);
    }

}