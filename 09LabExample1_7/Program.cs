using System;
using System.Threading;
using System.Diagnostics;

namespace _09LabExample1_7
{
    class Program
    {
        static int len = 0;
        static int[] m, m1;
        static object dr = new object();
        static int desired_num = 1;
        static int count = 0;

        static void Main(string[] args)
        {
            Random rnd = new Random();
            int size = 1000000;
            int num = 4;


            m = new int[size];
            m1 = new int[size];

            Console.WriteLine("Geniration of random array's numbers & write to txt file. Please, wait.");

            System.IO.StreamWriter textFile = new System.IO.StreamWriter(@"C:\Users\linfli\source\repos\09LabExample1_7\09LabExample1_7\text.txt");
            for (int i = 0; i < size; i++)
            {
                int c = rnd.Next(10);
                textFile.WriteLine(c);
            }
            textFile.Close();

            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\linfli\source\repos\09LabExample1_7\09LabExample1_7\text.txt");

            for (int i = 0; i < lines.Length; i++)
            {
                m[i] = Convert.ToInt32(lines[i]);
                m1[i] = Convert.ToInt32(lines[i]);
            }

            Console.WriteLine("Size: " + size);
            Console.WriteLine("Threads: " + num);


            Stopwatch time = new Stopwatch();
            Thread[] threads = new Thread[num];

            len = (int)Math.Ceiling(Convert.ToDouble(size) / Convert.ToDouble(num));

            for (int i = 0; i < num; i++)
            {
                threads[i] = new Thread(work);
            }
            time.Start();
            for (int i = 0; i < num; i++)
            {
                threads[i].Start(i);
            }
            for (int i = 0; i < num; i++)
            {
                threads[i].Join();
            }
            time.Stop();

            long t = time.ElapsedMilliseconds;

            Console.WriteLine("work time with " + num + " threads ms - " + t);

            count = 0;

            time.Reset();
            time.Start();

            for (int i = 0; i < size; i++)
            {
                if (m1[i] == desired_num) {
                    count += 1;
                }
            }

            time.Stop();

            Console.WriteLine("Desire number:" + desired_num);
            Console.WriteLine("Min count of desired value: - " + count);

            Console.WriteLine("Time to work with 1 thread - " + time.ElapsedMilliseconds);
            Console.WriteLine("Accelerator: " + (double)time.ElapsedMilliseconds / (double)t);
            


            Console.ReadLine();
        }

        static void work(Object n)
        {
            int num = (int)n;
            int countt = 0;
            int des = 1;
            for (int i = len * num; i < (len * num) + len && i < m.Length; i++)
            {
                if (des > m[i])
                {
                    des = m[i];
                    countt += 1;
                } 
            }
            lock (dr) { count = countt; }
        }
    }
}