using Abp.Application.Services;
using Abp.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCompany.UProject.People.Application.Contracts.Accounts;

namespace UCompany.UProject.People.Application.Accounts
{
    [DisableAuditing]
    public class PeopleAppService : ApplicationService, IPeopleAppService
    {
        private int _count;

        public virtual int UpdateCount()
        {
            Logger.Info($"UpdateCount：{_count}");
            _count++;
            Console.WriteLine("count is " + _count);
            return _count;
        }
    }
}
