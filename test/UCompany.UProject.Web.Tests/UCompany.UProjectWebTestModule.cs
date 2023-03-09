using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using UCompany.UProject.EntityFrameworkCore;
using UCompany.UProject.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace UCompany.UProject.Web.Tests
{
    [DependsOn(
        typeof(UCompany.UProjectWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class UCompany.UProjectWebTestModule : AbpModule
    {
        public UCompany.UProjectWebTestModule(UCompany.UProjectEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(UCompany.UProjectWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(UCompany.UProjectWebMvcModule).Assembly);
        }
    }
}