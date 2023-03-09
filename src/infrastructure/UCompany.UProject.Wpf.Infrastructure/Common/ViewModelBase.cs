using Abp.Dependency;
using Castle.Core.Logging;
using Prism.Mvvm;

namespace UCompany.UProject.Wpf.Infra.Common
{
    public class ViewModelBase : BindableBase
    {
        public ILogger Logger = IocManager.Instance.IocContainer.Resolve<ILogger>();
    }
}