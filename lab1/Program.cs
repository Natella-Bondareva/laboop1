using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab1
{
    internal class Program
    {
        internal class Timer
        {
            public Timer(Action action, int t)
            {
                Thread thread = new Thread(() =>
                {
                    while (true)
                    {
                        action.Invoke();
                        Thread.Sleep(TimeSpan.FromSeconds(t));
                    }
                });
                thread.Start();
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть кількість секунд(періодичність роботи таймеру):");
            int t = int.Parse(Console.ReadLine());
            Timer timer = new Timer(SomeMethod, t);
        }
        static void SomeMethod()
        {
            Console.WriteLine("Привіт!");
        }
    }
}
