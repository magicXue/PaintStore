using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PaintStore.DataAccess;

public class DesignTimePaintProductDbContextFactory : IDesignTimeDbContextFactory<PaintStoreDbContext>
{
    public PaintStoreDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<PaintStoreDbContext>();

        var connectionString = configuration.GetConnectionString("PaintStoreDb");

        builder.UseSqlServer(connectionString);

        return new PaintStoreDbContext(builder.Options);
    }
}
