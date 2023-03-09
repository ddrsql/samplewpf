using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using UCompany.Platform.EntityFrameworkCore.Seed;
using Microsoft.Extensions.Logging;
using System;

namespace UCompany.Platform.EntityFrameworkCore
{
    [DependsOn(
        typeof(PlatformCoreModule)
        , typeof(AbpEntityFrameworkCoreModule)
        )]
    public class PlatformEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; } = true;

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<PlatformDbContext>(options =>
                {
#if DEBUG
                    var loggerFactory = IocManager.IocContainer.Resolve<ILoggerFactory>();
                        options.DbContextOptions.UseLoggerFactory(loggerFactory);
                        options.DbContextOptions.EnableSensitiveDataLogging(); // 记录SQL的参数值
#endif
                    if (options.ExistingConnection != null)
                    {
                        PlatformDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        PlatformDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PlatformEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
