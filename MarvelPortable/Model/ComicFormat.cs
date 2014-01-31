using MarvelPortable.Attributes;

namespace MarvelPortable.Model
{
    public enum ComicFormat
    {
        Comic,
        Magazine,
        [Description("trade paperback")]
        TradePaperback,
        Hardcover,
        Digest,
        [Description("graphic novel")]
        GraphicNovel,
        [Description("digital comic")]
        DigitalComic,
        [Description("infinite comic")]
        InfiniteComic
    }
}