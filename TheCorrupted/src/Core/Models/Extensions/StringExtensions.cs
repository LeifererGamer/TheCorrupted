namespace TheCorrupted.TheCorrupted.src.Core.Models.Extensions;

//Mostly utilities to get asset paths.
public static class StringExtensions
{
    public static string CardImagePath(this string path)
    {
        return Path.Join("images", "packed", "card_portraits", "corrupted", path);
    }

}