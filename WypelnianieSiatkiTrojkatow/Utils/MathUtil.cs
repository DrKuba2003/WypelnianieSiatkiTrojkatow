using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypelnianieSiatkiTrojkatow.Utils
{
    public static class MathUtil
    {

        public static double B(int i, int n, float t)
        {
            return NewtonBionimal(n, i) * Math.Pow(t, i) * Math.Pow(1 - t, n - i);
        }

        public static double NewtonBionimal(int n, int k)
        {
            double result = 1;
            for (int i = 1; i <= k; i++)
            {
                result *= n - (k - i);
                result /= i;
            }
            return result;
        }

        public static double ToRadians(this double deg)
        {
            return (Math.PI / 180) * deg;
        }
    }
}
