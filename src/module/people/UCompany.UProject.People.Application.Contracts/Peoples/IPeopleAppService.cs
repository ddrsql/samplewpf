using Abp.Application.Services;
using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCompany.UProject.People.Application.Contracts.Accounts
{
    public interface IPeopleAppService : IApplicationService, ISingletonDependency
    {
        int UpdateCount();
    }
}
