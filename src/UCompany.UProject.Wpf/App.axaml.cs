using Abp;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using UCompany.UProject.Wpf.ViewModels;
using UCompany.UProject.Wpf.Views;
using Microsoft.Extensions.Configuration;
using Prism.DryIoc;
using Prism.Ioc;
using System.Diagnostics;
using System.IO;
using System;
using Abp.PlugIns;
using Prism.Modularity;
using UCompany.UProject.People.Module;
using Castle.Facilities.Logging;
using Serilog;
using System.Reflection;
using Microsoft.Extensions.Options;
using Serilog.Events;
using Serilog.Core;
using Abp.Dependency;
using Abp.Domain.Uow;
using Prism.Regions;
using Avalonia.Controls;

namespace UCompany.UProject.Wpf
{
    public partial class App : PrismApplication //PrismApplication Application
    {
        public IConfiguration Configuration { get; private set; }
        private readonly AbpBootstrapper _bootstrapper = AbpBootstrapper.Create<UProjectWpfModule>();

        public override void Initialize()
        {
            OnStartup();
            AvaloniaXamlLoader.Load(this);
            base.Initialize();              // <-- Prism Required
        }

        //public override void OnFrameworkInitializationCompleted()
        //{
        //    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        //    {
        //        desktop.MainWindow = new MainWindow
        //        {
        //            //DataContext = new MainWindowViewModel(),
        //        };
        //    }

        //    base.OnFrameworkInitializationCompleted();
        //}
        #region prism

        protected override AvaloniaObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MenuItemUC, MenuItemViewModel>();
            containerRegistry.RegisterForNavigation<UserControl1>();
        }

        /// <summary>
        /// 代码注册模块
        /// </summary>
        /// <param name="moduleCatalog"></param>
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<PeopleModule>();
            //moduleCatalog.AddModule<TodoListModule>();
            base.ConfigureModuleCatalog(moduleCatalog);
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            //获取该路径下的文件夹的模块目录
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Modules\TodoList");
            return new DirectoryModuleCatalog() { ModulePath = path };

            ////加载配置文件模块目录 App.config
            //return new ConfigurationModuleCatalog();
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("TabRegion", typeof(MenuItemUC));
        }
        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(TabControl), Container.Resolve<TabControlAdapter>());
        }

        #endregion

        protected void OnStartup()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"PlugIns\People");
            Console.WriteLine("path is " + path);
            _bootstrapper.PlugInSources.AddFolder(path);
            _bootstrapper.Initialize();
            watch.Stop();  //停止监视
            TimeSpan timeSpan = watch.Elapsed;
            Console.WriteLine("执行时间：{0}(毫秒)", timeSpan.TotalMilliseconds);
        }
    }
}