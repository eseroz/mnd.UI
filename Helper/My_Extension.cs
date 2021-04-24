using mnd.Logic.Model.Uretim;
using mnd.Logic.Services._DTOs;
using System;
using System.Collections.Generic;

namespace mnd.UI.Helper
{
    public static class My_Extension
    {
        public static KaliteSertifikaDto ToKaliteSertifikaDto(this UretimEmri source)
        {
            var k = new KaliteSertifikaDto();
            k.Bobinler = new List<Bobin>();

            k.Bobinler.AddRange(source.UretimBobinler);

            return k;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
        {
            if (dictionary == null) { throw new ArgumentNullException(nameof(dictionary)); } // using C# 6
            if (key == null) { throw new ArgumentNullException(nameof(key)); } //  using C# 6

            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }
    }
}