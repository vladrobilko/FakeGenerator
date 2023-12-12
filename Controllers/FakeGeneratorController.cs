using FakeGenerator.Helpers;
using FakeGenerator.Models;
using FakeGenerator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace FakeGenerator.Controllers
{
    public class FakeGeneratorController : Controller
    {
        private readonly IFakeErrorGenerator _fakeErrorGenerator;

        public FakeGeneratorController(IFakeErrorGenerator fakeErrorGenerator) => _fakeErrorGenerator = fakeErrorGenerator;

        [HttpGet]
        public IActionResult Index()
        {
            var users = _fakeErrorGenerator.GetFakeUsersWithErrors(FakeDataConfigurationHelper.DefaultConfiguration, FakeDataConfigurationHelper.DefaultUserCount);
            return View(FakeDataConfigurationHelper.GetDefaultViewModel(users));
        }

        [HttpPost]
        public IActionResult Index(FakeDataConfigurationViewModel configuration)
        {
            configuration.Users = _fakeErrorGenerator.GetFakeUsersWithErrors(configuration, FakeDataConfigurationHelper.DefaultUserCount);
            return View(configuration);
        }

        [HttpGet]
        public IActionResult LoadMoreData(int page, string region, int errorCount, int seed)
        {
            var users = _fakeErrorGenerator.GetFakeUsersWithErrors(FakeDataConfigurationHelper.GetConfiguration(region, errorCount, seed), page.CalculateNextUserCount());
            var model = FakeDataConfigurationHelper.GetConfiguration(users, region, errorCount, seed);
            return PartialView("_UserDataTablePartial", model);
        }

        [HttpPost]
        public IActionResult Export(FakeDataConfigurationViewModel configuration, string usersCount)
        {
            var users = _fakeErrorGenerator.GetFakeUsersWithErrors(configuration, int.Parse(usersCount));
            return File(Encoding.UTF8.GetBytes(users.ToCsv()), "text/csv", "users.csv");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
