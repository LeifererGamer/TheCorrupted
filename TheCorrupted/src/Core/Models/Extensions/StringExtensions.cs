namespace TheCorrupted.TheCorrupted.src.Core.Models.Extensions;

//Mostly utilities to get asset paths.
public static class StringExtensions
{
    public static string CardImagePath(this string path)
    {
        return Path.Join("images", "packed", "card_portraits", "corrupted", path);
    }

    public static string CardImagePathCurses(this string path)
    {
        return Path.Join("images", "packed", "card_portraits", "curse", path);
    }

    public static string RelicImagePath(this string path)
    {
        return Path.Join("images", "relics", path);
    }

    public static string PotionImagePath(this string path)
    {
        return Path.Join("images", "potions", path);
    }

    public static string BigRelicImagePath(this string path)
    {
        return Path.Join("images", "relics", path);
    }

    public static string TresRelicImagePath(this string path)
    {
        return Path.Join("images", "atlases", "relic_atlas.sprites", path);
    }

}