using System;

namespace AdventOfCode2020.Utilities
{
    public static class ArrayExtensions
    {
        public static int IndexOf<T>(this T[] arr, T val)
            where T : IEquatable<T>
        {
            for(int i = 0; i < arr.Length; ++i)
            {
                if (val.Equals(val))
                    return i;
            }

            return -1;
        }
    }
}
