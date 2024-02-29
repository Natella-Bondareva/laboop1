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
        class Timer
        {
            public delegate void TimerDelegate();

            private TimerDelegate timerDelegate;
            private int interval;
            private bool isRunning;

            public Timer(TimerDelegate method, int intervalInSeconds)
            {
                timerDelegate = method;
                interval = intervalInSeconds * 1000; // перетворюємо секунди на мілісекунди
                isRunning = false;
            }

            public void Start()
            {
                if (isRunning)
                {
                    Console.WriteLine("Timer is already running.");
                    return;
                }

                isRunning = true;

                Thread timerThread = new Thread(() =>
                {
                    while (isRunning)
                    {
                        timerDelegate.Invoke(); // викликаємо делегат

                        Thread.Sleep(interval);
                    }
                });

                timerThread.Start();
            }

            public void Stop()
            {
                isRunning = false;
            }
        }

        static void Main(string[] args)
        {
            Timer.TimerDelegate myDelegate = () =>
            {
                Console.WriteLine("Method executed at: " + DateTime.Now);
            };

            Timer myTimer = new Timer(myDelegate, 5); // викликати метод кожні 5 секунд

            myTimer.Start();

            // Дайте таймеру працювати протягом якогось часу
            Thread.Sleep(20000);

            myTimer.Stop(); // Зупиняємо таймер
        }
    }
}
