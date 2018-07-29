using System;

namespace vsx.Extensions
{
    public static class StringExtensions
    {
        public static int EvaluateToId(this string input) => Int32.Parse(input);

        public static Guid EvaluateToGuid(this string input) => new Guid(input);
    }
}
