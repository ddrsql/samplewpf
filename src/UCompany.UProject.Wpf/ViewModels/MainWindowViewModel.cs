
using Prism.Mvvm;
using Prism.Regions;
using Prism.Commands;
using System;
using System.Reflection.Metadata;
using UCompany.UProject.Wpf.Views;
using UCompany.UProject.People.Application.Contracts.Accounts;
using Prism.Ioc;
using UCompany.UProject.People.Module.Views;
using Abp.Dependency;
using Castle.Core.Logging;
using UCompany.UProject.Wpf.Infra.Common;
using UCompany.UProject.Wpf.Infra.Constants;
using Abp.UI;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;

namespace UCompany.UProject.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<ItemMenu> Modules { get; set; }

        private IRegion _paientListRegion;
        private PeopleList _patientListView;

        private readonly IRegionManager _regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            var item1 = new ItemMenu()
            {
                Name = "Register",
                UC = new UserControl1(),
                SubItems = new List<SubItem>
                {
                    new SubItem() { Name = "Customer" },
                    new SubItem() { Name = "Providers" }
                }
            };
            var item2 = new ItemMenu()
            {
                Name = "Appointments",
                UC = new MenuItemUC(),
                SubItems = new List<SubItem>
                {
                    new SubItem() { Name = "Services" },
                    new SubItem() { Name = "Meetings" }
                }
            };
            var item3 = new ItemMenu()
            {
                Name = "Expenses",
            };
            Modules = new ObservableCollection<ItemMenu>() {
                item1,
                item2,
                item3
            };
        }


        #region Properties
        private ViewModelBase _viewContent;
        public ViewModelBase ViewContent
        {
            get => _viewContent;
            set => SetProperty(ref _viewContent, value);
        }
        #endregion

        #region Commands

        private DelegateCommand _loadCommand;
        public DelegateCommand LoadCommand =>
            _loadCommand ?? (_loadCommand = new DelegateCommand(ExecuteLoadCommand));

        private DelegateCommand<object> _closeTabCommand;
        public DelegateCommand<object> CloseTabCommand =>
            _closeTabCommand ?? (_closeTabCommand = new DelegateCommand<object>(OnExecuteCloseCommand));

        #endregion

        #region  Excutes

        void OnExecuteCloseCommand(object tabItem)
        {
            Modules.RemoveAt(0);
        }

        async void ExecuteLoadCommand()
        {
            Logger.Info($"ExecuteLoadCommand");
            Console.WriteLine("Test");
            var item3 = new ItemMenu()
            {
                UC = ContainerLocator.Current.Resolve<MenuItemUC>(),
                Name = "测试"+DateTime.Now,
            };
            Modules.Add(item3);
            //Modules.RemoveAt(0);
            return;
            //throw new UserFriendlyException("异常");
            try
            {
                await Test();
                //TestTwo();
            }
            catch (Exception ex)
            {

            }
            _regionManager.RegisterViewWithRegion("TabRegion", typeof(UserControl1));
            var aa = _regionManager.Regions["TabRegion"];
            var view = ContainerLocator.Current.Resolve<MenuItemUC>();
            aa.Add(view);
            //aa.Activate(view);
            //var param = new NavigationParameters();
            //param.Add("Page", nameof(MainWindowViewModel));
            //_regionManager.RequestNavigate(RegionNames.TodoListRegion, "TodoListView", NavigationCompelted, param);


            //_paientListRegion = _regionManager.Regions["PeopleRegion"];
            //_patientListView = ContainerLocator.Current.Resolve<PeopleList>();
            //_paientListRegion.Add(_patientListView);
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

        /// <summary>
        /// 导航后的回调方法
        /// </summary>
        /// <param name="result"></param>
        private void NavigationCompelted(NavigationResult result)
        {
            if (result.Result == true)
            {
         
            }
            else
            {
         
            }
        }
    }
}