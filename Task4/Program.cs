using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    internal class Program
    {
        delegate void Operation(double x);
        static void Main(string[] args)
        {
            Console.WriteLine("Вводьте рядки послідовно один за одним.\r\n" +
                "Рядки повинні мати вигляд 0 x чи 1 x чи 2 x\r\n" +
                "(тобто, цифра від 0 до 2, а після неї запис конкретного дійсного числа),\r\n" +
                "програма обчислюватиме одну з трьох функцій\r\n        " +
                "0 -- sin(x) (приймає в градусах)\r\n        " +
                "1 -- exp(x^2)\r\n        " +
                "2 -- ln(abs(x + 4))\r\n" +
                "(згідно цифри на початку) і виводитиме результат.\r\n\r\n" +
                "Якщо вхідний рядок не задовольнятиме цей формат, програма завершить роботу.");

            Operation[] operations = new Operation[]
            {
            (x) => Console.WriteLine(Math.Sin(x * Math.PI / 180)),
            (x) => Console.WriteLine(Math.Exp(Math.Pow(x,2))),
            (x) => Console.WriteLine(Math.Log(Math.Abs(x+4))),
            };

            while (true)
            {
                try
                {
                    string[] str = Console.ReadLine().Trim().Split();
                    int choice = int.Parse(str[0]);
                    double operant = double.Parse(str[1]);
                    operations[choice](operant);
                }
                catch (Exception) {
                    Console.WriteLine("Некоректний вибір операції. Програма завершує роботу. Дякую за співпрацю!");
                }
            }

        }
    }
}
