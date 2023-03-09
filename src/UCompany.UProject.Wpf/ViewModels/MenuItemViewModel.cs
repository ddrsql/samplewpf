using Avalonia.Controls;
using UCompany.UProject.Wpf.Infra.Common;
using UCompany.UProject.Wpf.Infra.Constants;
using UCompany.UProject.Wpf.Views;
using Microsoft.AspNetCore.Routing;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UCompany.UProject.Wpf.ViewModels
{
    public class MenuItemViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;

        SubItem selectedItem;
        public SubItem SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }
        public List<ItemMenu> ItemMenus { get; set; }

        public MenuItemViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            GetItemMenu();
        }

        #region Commands

        private DelegateCommand<SubItem> _menuCommand;
        public DelegateCommand<SubItem> MenuCommand =>
            _menuCommand ?? (_menuCommand = new DelegateCommand<SubItem>(ExecuteMenuCommand));

 
        #endregion

        #region  Excutes

        void GetItemMenu()
        {
            var item1 = new ItemMenu()
            {
                Name = "Register",
                SubItems = new List<SubItem>
                {
                    new SubItem() { Name = "TabTest" ,Route="UserControl1"},
                    new SubItem() { Name = "TodoList" ,Route="TodoListView"}
                }
            };
            var item2 = new ItemMenu()
            {
                Name = "Appointments",
                SubItems = new List<SubItem>
                {
                    new SubItem() { Name = "Abp静态对象",Route="PeopleList" },
                    new SubItem() { Name = "Meetings" }
                }
            };
            var item3 = new ItemMenu()
            {
                Name = "Expenses",
            };
            ItemMenus = new List<ItemMenu>() {
                item1,
                item2,
                item3
            };
        }
 
        async void ExecuteMenuCommand(SubItem tabItem)
        {
            Logger.Info($"ExecuteMenuCommand");

            if (!string.IsNullOrEmpty(tabItem?.Route))
                _regionManager.RequestNavigate(RegionNames.ContentRegion, tabItem.Route);
        }

        public async Task Test()
        {
            await Task.CompletedTask;
            throw new Exception("Task Exception");
        }

        public async void TestTwo()
        {
            await Task.CompletedTask;
            throw new Exception("Task Exception");
        }

        #endregion

    }

    public class ItemMenu
    {
        public string Name { get; set; }
        public UserControl UC { get; set; }
        public List<SubItem> SubItems { get; set; }
    }

    public class SubItem
    {
        public string Name { get; set; }
        public string Route { get; set; }
    }
}
