using System.Threading.Tasks;
using UCompany.Platform.Configuration.Dto;

namespace UCompany.Platform.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
