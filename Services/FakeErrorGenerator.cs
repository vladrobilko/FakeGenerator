using Bogus;
using FakeGenerator.Models;
using FakeGenerator.Services.Interfaces;

namespace FakeGenerator.Services
{
    public class FakeErrorGenerator : IFakeErrorGenerator
    {
        private readonly ErrorGenerator _generator;

        public FakeErrorGenerator() => _generator = new ErrorGenerator();

        public List<User> GetFakeUsersWithErrors(FakeDataConfigurationViewModel configuration, int count)
        {
            var users = GenerateUsersWithSeed(configuration, count);
            if (configuration.ErrorCount != 0)
                _generator.ApplyErrors(users, configuration);
            return users;
        }

        private static List<User> GenerateUsersWithSeed(FakeDataConfigurationViewModel configuration, int count)
        {
            var users = new List<User>();
            var faker = new Faker(configuration.Region);
            Randomizer.Seed = new Random(configuration.Seed);
            for (int i = 0; i < count; i++)
                users.Add(CreateNewUser(faker, i + 1));
            return users;
        }

        private static User CreateNewUser(Faker faker, int number)
        {
            return new User
            {
                Number = number,
                Id = Guid.NewGuid().ToString(),
                FullName = faker.Name.FullName(),
                Address = faker.Address.FullAddress(),
                Phone = faker.Phone.PhoneNumberFormat()
            };
        }
    }
}
