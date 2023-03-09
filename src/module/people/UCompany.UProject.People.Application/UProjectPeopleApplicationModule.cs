using Abp.Modules;
using System.Reflection;

namespace UCompany.UProject.People.Application
{
    public class UProjectPeopleApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}