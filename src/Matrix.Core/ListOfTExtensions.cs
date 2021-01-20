namespace Matrix
{
    using System.Collections;
    using System.Linq;

    public static class ListOfTExtensions
    {
        public static bool Contains<T>(this IEnumerable source) where T : class
        {
            return source.OfType<T>().Any();
        }

        public static T Get<T>(this IEnumerable source) where T : class
        {
            return source.OfType<T>().FirstOrDefault();
        }
    }
}
