using UCompany.UProject.TodoList.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Reflection;

namespace UCompany.UProject.TodoList.Module
{
    public class TodoListModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TodoListView>();
            containerRegistry.RegisterForNavigation<AddItemView>();
        }
    }
}