using UCompany.UProject.People.Module.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Reflection;

namespace UCompany.UProject.People.Module
{
    public class PeopleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PeopleList>();
        }
    }
}