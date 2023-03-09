using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace UCompany.UProject.Tests.Sessions
{
    public class SessionAppService_Tests : UProjectTestBase
    {

        public SessionAppService_Tests()
        {
        }

        [MultiTenantFact]
        public async Task Should_Get_Current_User_When_Logged_In_As_Host()
        {
            // Arrange
        }

        [Fact]
        public async Task Should_Get_Current_User_And_Tenant_When_Logged_In_As_Tenant()
        {
            // Act

        }
    }
}
