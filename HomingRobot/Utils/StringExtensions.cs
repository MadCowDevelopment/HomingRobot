using System;
using System.Linq;

namespace HomingRobot.Utils
{
    public static class StringExtensions
    {

        public static string Repeat(this string s, int n)
        {
            return new String(Enumerable.Range(0, n).SelectMany(x => s).ToArray());
        }
    }
}
