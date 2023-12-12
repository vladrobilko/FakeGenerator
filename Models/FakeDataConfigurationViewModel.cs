namespace FakeGenerator.Models
{
    public class FakeDataConfigurationViewModel
    {
        public string? Region { get; set; }

        public double ErrorCount { get; set; }

        public int Seed { get; set; }

        public List<User>? Users { get; set; }
    }
}
