using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    internal class Program
    {
        static double CalculateOfSeries (int accuracy, Func<int, double> func )
        {
            double sum = 0;
            double previousSum = 0;

            for (int i = 0; true; i++)
            {
                double currentTerm = func(i);
                sum += currentTerm;

                if (Math.Abs(sum - previousSum) < Math.Pow(10, -accuracy))
                {
                    break;
                }

                previousSum = sum;
            }

            return sum;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть точність(кількість знаків після коми): ");
            int accuracy =  int.Parse(Console.ReadLine());

            Console.WriteLine("1 + 1/2 + 1/4 + 1/8 + 1/16 + ...: ");
            Console.WriteLine(CalculateOfSeries(accuracy, CurrElem => 1.0/Math.Pow(2, CurrElem)));

            Console.WriteLine("1 + 1/2! + 1/3! + 1/4! + 1/5! + ...: ");
            Console.WriteLine(CalculateOfSeries(accuracy, CurrElem => 
            {
                double factorial = 1;

                for (int i = 2; i <= CurrElem+1; i++)
                {
                    factorial *= i;
                }

                return 1.0/ factorial;
            }));
            Console.WriteLine("–1 + 1/2 – 1/4 + 1/8 – 1/16 + ...: ");
            Console.WriteLine(CalculateOfSeries(accuracy, CurrElem => -1.0 / Math.Pow(-2, CurrElem)));
        }
    }
}
