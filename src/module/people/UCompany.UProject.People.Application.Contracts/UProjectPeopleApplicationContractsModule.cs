using Abp.Modules;
using System.Reflection;

namespace UCompany.UProject.People.Application.Contracts
{
    public class UProjectPeopleApplicationContractsModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}