using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystem.Core.Tests
{
    internal class MockContext : DbContext
    {
        public MockContext(DbContextOptions<MockContext> options) : base(options) { }
    }
}
