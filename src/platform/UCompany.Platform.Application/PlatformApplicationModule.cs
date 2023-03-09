using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using UCompany.Platform.TodoItems.Dtos;
using UCompany.Platform.TodoItems.Vtos;

namespace UCompany.Platform
{
    [DependsOn(
        typeof(PlatformCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class PlatformApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<GetTodoItemOutput, TodoItemVM>();
            });
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(PlatformApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
