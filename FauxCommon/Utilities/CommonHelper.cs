namespace LeFauxMods.Common.Utilities;

/// <summary>Common helper methods.</summary>
internal static class CommonHelper
{
    private const string AlphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public static string GetTemporaryId(string prefix)
    {
        var id = prefix + RandomString();
        while (Game1.player.team.globalInventories.ContainsKey(id)
               || Game1.player.team.globalInventoryMutexes.ContainsKey(id))
        {
            id = prefix + RandomString();
        }

        return id;
    }

    private static string RandomString(int length = 16)
    {
        var stringChars = new char[length];

        for (var i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = AlphaNumeric[Game1.random.Next(AlphaNumeric.Length)];
        }

        return new string(stringChars);
    }
}
