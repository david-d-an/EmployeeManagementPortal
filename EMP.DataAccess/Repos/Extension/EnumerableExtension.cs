
using System.Collections.Generic;

namespace EMP.DataAccess.Repos.Extension {

    public static partial class EnumerableExtension
    {
        public static IEnumerable<T> ToEnumerable<T>(this IEnumerable<T> source)
        {
            foreach (var item in source)
                yield return item;
        }
    }
}