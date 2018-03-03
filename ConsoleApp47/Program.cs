using System;
using System.Diagnostics;
using System.Runtime.Intrinsics;
namespace ConsoleApp46
{
    using System.Runtime.Intrinsics.X86;
    unsafe class Program
    {

        unsafe static void Main(string[] args)
        {
            Atan.Init();

            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < 10000000; i++)
            {
                i++;
                i--;
            }
            double ans = 0;
            for (int c = 0; c < 10; c++)
            {
                sw.Start();

                for (int i = 0; i < 10000000; i++)
                {
                       ans=      Math.Atan(i);

                    ans = Atan.Compute(i);
                }

                Console.WriteLine(sw.ElapsedMilliseconds);
                Console.WriteLine(ans);
                Console.WriteLine();
                sw.Reset();
            }
        }
    }


    static unsafe class Atan
    {
        static float* po;
        static public void Init()
        {
            var p = stackalloc float[4];
            po = p;
        }

        static public float Compute(float i)
        {

            if (Math.Abs(i - 1) <= 1)
            {
                return (float)Math.Atan(i);
            }
            Vector128<float> a = Sse.SetVector128(-1,0.333333333f, -0.2f, +0.142857142857f);



            // var a = 
            var p = i * i;

            var j = p * i;
            var k = j * p;
            var l = k * p;
            var m = l * p;

            var b = Sse.SetVector128(i,j, k, l);


            var ans = Sse.Divide(a, b);

            Sse.Store(po, ans);

            float an = 1.57079633f ;
            an += po[0] + po[1] + po[2] + po[3];



            return an;
        }
    }

}
