using System.Threading.Tasks;
using UCompany.UProject.Models.TokenAuth;
using UCompany.UProject.Web.Controllers;
using Shouldly;
using Xunit;

namespace UCompany.UProject.Web.Tests.Controllers
{
    public class HomeController_Tests: UCompany.UProjectWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}