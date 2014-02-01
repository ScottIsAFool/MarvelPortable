using MarvelPortable.Attributes;

namespace MarvelPortable.Model
{
    public enum SeriesType
    {
        Collection,
        [Description("one shot")]
        OneShot,
        Limited,
        Ongoing
    }
}