using Xunit;

namespace CustomerManagementSystem.Services.Tests
{
    public class MernisValidationServiceTests
    {
        [Fact]
        public void ValidationReturnsTrueForCorrectInput()
        {
            var validationService = new MernisValidationService();
            var result = validationService.ValidateCustomer(51949329530, "Muhammet Selim", "Yeþilkaya", 1994);
            Assert.True(result);
        }

        [Fact]
        public void ValidationReturnsFalseForIncorrectInput()
        {
            var validationService = new MernisValidationService();
            var result = validationService.ValidateCustomer(51643329530, "Muhammet Selim", "Yeþilkaya", 1994);
            Assert.False(result);
        }
    }
}
