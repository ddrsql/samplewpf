using Abp.Modules;
using Castle.Facilities.Logging;
using Castle.Services.Logging.SerilogIntegration;
using Castle.Windsor.MsDependencyInjection;
using UCompany.UProject.People.Application.Contracts;
using UCompany.Platform;
using UCompany.Platform.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;

namespace UCompany.UProject.Wpf
{
    [Abp.Modules.DependsOn(typeof(PlatformEntityFrameworkModule), typeof(PlatformApplicationModule)
        ,typeof(UProjectPeopleApplicationContractsModule))]
    public class UProjectWpfModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;
        
        

        public override void PreInitialize()  //预初始化
        {
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.Auditing.IsEnabled = false;
            //AppDomain.CurrentDomain.BaseDirectory;
            //Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets(typeof(App).Assembly);

            Configuration.DefaultNameOrConnectionString = builder.Build().GetConnectionString("Default");

            var services = new ServiceCollection();
            services.CreateSeriLog(builder.Build())
                .UseSeriLog(SerilogCreatorExtensions.logger);
            IocManager.IocContainer.AddFacility<LoggingFacility>(f => f.LogUsing(new SerilogFactory(SerilogCreatorExtensions.logger)));

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
