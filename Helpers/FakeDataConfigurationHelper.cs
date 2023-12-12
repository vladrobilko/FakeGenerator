using FakeGenerator.Models;

namespace FakeGenerator.Helpers
{
    public static class FakeDataConfigurationHelper
    {
        public const int DefaultUserCount = 20;

        public const int UsersToAddPerUpdate = 10;

        public static FakeDataConfigurationViewModel DefaultConfiguration { get; } = new()
        {
            Region = "en_US",
            ErrorCount = 0,
            Seed = 0
        };

        public static int CalculateNextUserCount(this int currentPage) => DefaultUserCount + (currentPage - 1) * UsersToAddPerUpdate;

        public static FakeDataConfigurationViewModel GetDefaultViewModel(List<User> users)
        {
            return new()
            {
                Region = "en_US",
                ErrorCount = 0,
                Seed = 0,
                Users = users
            };
        }

        public static FakeDataConfigurationViewModel GetConfiguration(List<User> users, string region, int errorCount, int seed)
        {
            return new FakeDataConfigurationViewModel()
            {
                Region = region,
                ErrorCount = errorCount,
                Seed = seed,
                Users = users.TakeLast(10).ToList()
            };
        }

        public static FakeDataConfigurationViewModel GetConfiguration(string region, int errorCount, int seed)
        {
            return new FakeDataConfigurationViewModel()
            {
                Region = region,
                ErrorCount = errorCount,
                Seed = seed
            };
        }
    }
}
