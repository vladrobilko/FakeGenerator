namespace FakeGenerator.Helpers;

public static class LocalLettersSetProvider
{
    public const string USA = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public const string Georgia = "აბგდევზთიკლმნოპჟრსტუფქღყშჩცძწჭხჯჰ0123456789";

    public const string Japan = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをん0123456789";

    public static string GetLettersSet(string region)
    {
        switch (region)
        {
            case "en_US":
                return USA;
            case "ge":
                return Georgia;
            case "ja":
                return Japan;
            default:
                throw new ArgumentException($"Unsupported region: {region}", nameof(region));
        }
    }
}
