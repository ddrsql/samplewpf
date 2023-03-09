using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace UCompany.Platform.EntityFrameworkCore
{
    public static class PlatformDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<PlatformDbContext> builder, string connectionString)
        {
            //builder.UseSqlServer(connectionString);
            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public static void Configure(DbContextOptionsBuilder<PlatformDbContext> builder, DbConnection connection)
        {
            //builder.UseSqlServer(connection);
            builder.UseMySql(connection, ServerVersion.AutoDetect(connection.ConnectionString));
        }
    }
}
