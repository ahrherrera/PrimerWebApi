using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Omega.Common.Helpers.Extensions
{
    public static class Extension
    {
        public static bool IsNotNullAndHasAny<T>(this IEnumerable<T> enumerable)
        {
            bool result = false;

            if (enumerable != null && enumerable.Any())
            {
                result = true;
            }
            return result;
        }

        public static bool IsNullOrNotHasAny<T>(this IEnumerable<T> enumerable)
        {
            bool result = false;

            if (enumerable == null) result = true;

            if (enumerable != null && enumerable.Count() == 0) result = true;

            return result;
        }

        public static bool IsNotNullAndIsEmpty<T>(this IEnumerable<T> enumerable)
        {
            bool result = false;

            if (enumerable != null && enumerable.Count() == 0) result = true;

            return result;
        }

        public static IEnumerable<U> FindDuplicates<T, U>(this IEnumerable<T> list, Func<T, U> keySelector)
        {
            IEnumerable<U> result = null;

            result = list.GroupBy(keySelector)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key).ToList();

            if (result.IsNotNullAndHasAny())
            {
                result = result.Where(a => a != null).AsEnumerable();
            }

            return result;
        }

        public static T Clone<T>(this T source)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
        }

        public static U ForceCast<T, U>(this T source)
        {
            return JsonConvert.DeserializeObject<U>(JsonConvert.SerializeObject(source));
        }

        public static String GetEnumMemberValue<T>(this T value) where T : struct, IConvertible
        {
            return typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString())
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;
        }

        public static bool CompareTo(this object value, object reference)
        {
            return JsonConvert.SerializeObject(value).Equals(JsonConvert.SerializeObject(reference));
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
