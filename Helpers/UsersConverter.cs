using CsvHelper;
using FakeGenerator.Models;
using System.Globalization;

namespace FakeGenerator.Helpers
{
    public static class UsersConverter
    {
        public static string ToCsv(this List<User> users)
        {
            using (var writer = new StringWriter())
            {
                new CsvWriter(writer, CultureInfo.InvariantCulture).WriteRecords(users);
                return writer.ToString();
            }
        }
    }
}
