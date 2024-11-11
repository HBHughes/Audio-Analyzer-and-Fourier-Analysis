using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Audio_Analyzer_and_Fourier_Analysis
{
    internal class FourierFuncs()
    {
        public static Complex[] DFT(Complex[] input)
        {
            int N = input.Length;
            Complex[] output = new Complex[N];
            double val = -2.0 * Math.PI / (double)N;
            for (int n = 0; n < N; n++)
            {
                output[n] = new Complex(0,0);
                for (int k = 0; k < N; k++)
                    output[n] += input[k] * Complex.FromPolarCoordinates(1, val * (double)n * (double)k);
            }
            return output;
        }
    }
}
