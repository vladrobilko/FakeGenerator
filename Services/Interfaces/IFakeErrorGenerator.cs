using FakeGenerator.Models;

namespace FakeGenerator.Services.Interfaces;

public interface IFakeErrorGenerator
{
    List<User> GetFakeUsersWithErrors(FakeDataConfigurationViewModel configuration, int count);
}