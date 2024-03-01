using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    internal class Program
    {
        delegate int[] Delegate(int[] array, int k);

        static void Main(string[] args)
        {
            Console.WriteLine("Введіть коефіцієнт:");
            int k = int.Parse(Console.ReadLine());
            Console.WriteLine("0 - автоматичне заповненя масиву\n1 - ввід масиву вручну");
            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine("Введіть кількість елементів масиву:");
            int n = int.Parse(Console.ReadLine());
            int[] array = new int[n];
            switch (choice) 
            {
                case 0: GenerateRandomArray(ref array);
                    PrintArray(array);
                    break;
                case 1: InputArray(ref array);
                    break;
                default: Console.WriteLine("Помилочка");
                    break;
            }
            Delegate coolMethodDelegate = CoolMethodWithEnumerable;
            Delegate ownMethodDelegate = OwnMethod;
            int[] res = coolMethodDelegate(array, k);
            PrintArray(res);
            int[] res2 = ownMethodDelegate(array, k);
            PrintArray(res2);
        }
        static void InputArray(ref int[] array)
        {
            Console.WriteLine("Введіть елементи масиву через пробіл:");
            string[] str = Console.ReadLine().Trim().Split();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = int.Parse(str[i]);
            }
        }
        static void GenerateRandomArray(ref int[] array)
        {
            Random random = new Random();   
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(0,10000);
            }
        }
        private static void PrintArray(int[] array)
        {
            foreach (int element in array)
            {
                Console.Write(element + " ");
            }
            Console.WriteLine(); 
        }
        private static int[] CoolMethodWithEnumerable(int[] originalArray, int k)
        { 
            var newArray = originalArray.Where(element => element % k == 0).ToArray();
            return newArray;
        }
        private static int[] OwnMethod(int[] originalArray, int k)
        {
            int temp = 0;
            foreach (int element in originalArray)
            {
                if (element % k == 0)
                {
                    temp++;
                }
            }
            int[] resultArray = new int[temp];
            temp = 0;
            foreach (int element in originalArray)
            {
                if (element % k == 0)
                {
                    resultArray[temp++] = element;
                }
            }
            return resultArray;
        }
    }
}
