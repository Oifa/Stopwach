using System;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Lab3
{
    class Lab3
    {
        static int len = 0;
        static int[] matrix1, matrix2, matrix3, matrix;

        static void Main(string[] args)
        {
            Random rnd = new Random();
            //int size = 50000000;
            int size = 1000;
            int num = 2;
            int g = (int)size / 2;

            matrix1 = new int[g];
            matrix2 = new int[g];
            matrix3 = new int[g];

            matrix = new int[size];


            for (int i = 0; i < g; i++)
            {

                matrix1[i] = rnd.Next(1001);
                matrix2[i] = rnd.Next(1001);
                matrix3[i] = rnd.Next(1001);

            }
            for (int i = 0; i < size; i++)
            {

                matrix[i] = rnd.Next(1001);

            }

            Stopwatch time = new Stopwatch();
            time.Start();
            Thread threads = new Thread(work);
            Thread threads2 = new Thread(work);
            Thread threads3 = new Thread(work);

            threads.Start(matrix1);
            threads2.Start(matrix2);
            threads3.Start(matrix3);

            threads.Join();
            threads2.Join();
            threads3.Join();

            len = (int)Math.Ceiling(Convert.ToDouble(size) / Convert.ToDouble(num));
            time.Stop();

            long t = time.ElapsedMilliseconds;

            Console.WriteLine("work time with " + num + " threads ms: " + t);

            Stopwatch time2 = new Stopwatch();
            time2.Start();

            Thread mainTh = new Thread(work);
            mainTh.Start(matrix);
            mainTh.Join();

            time2.Stop();
            long t2 = time2.ElapsedMilliseconds;
            Console.WriteLine("work time with 1 thread: " + t2);

            Console.WriteLine("Accelerator: " + (double)t2 / (double)t);
            Console.ReadLine();
        }

        static void work(Object n)
        {

            int[] matrix3 = (int[])n;
            int[] Mass = new int[matrix3.Length];
            Mass[0] = matrix3[0];

            int sum = 0;
            for (int i = 0; i < matrix3.Length; i++)  // умножение
            {
                for (int j = 0; j < matrix3.Length; j++)
                {
                    for (int r = 0; r < matrix3.Length; r++)
                    {
                        sum = sum + arr1[i, matrix3.Length] * arr2[matrix3.Length, j];
                    }
                    Mass[i, j] = sum;
                    sum = 0;
                }
            }
            //int[] matrix3 = (int[])n;

            //int[][] summ = new int[matrix3.Length][];
            //summ[0][0] = matrix3[0][0];

            //for (int i = 1; i < matrix3.Length; i++)
            //{
            //    for (int j = 1; j < matrix3.Length; j++)
            //    {
            //        summ[i][j] = matrix2[i][j] + matrix1[i][j];
            //    }
            //}
            int t = 0;
        }
    }
}