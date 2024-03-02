using System.Diagnostics;

namespace Task6
{
    internal class Program
    {
        delegate void SortingAlgorithm(int[] array);

        static void Main(string[] args)
        {
            TestSortingAlgorithm("Selection Sort", SelectionSort);
            TestSortingAlgorithm("Shaker Sort", ShakerSort);

            Console.ReadLine();
        }
        static void TestSortingAlgorithm(string algorithmName, SortingAlgorithm sortingAlgorithm)
        {
            // Генеруємо масив для сортування
            int[] arrayToSort = GenerateRandomArray(10000);

            // Копіюємо масив для еталонного сортування
            int[] referenceArray = new int[arrayToSort.Length];
            Array.Copy(arrayToSort, referenceArray, arrayToSort.Length);

            // Запускаємо еталонне сортування та вимірюємо час
            Stopwatch stopwatchReference = Stopwatch.StartNew();
            Array.Sort(referenceArray);
            stopwatchReference.Stop();

            // Запускаємо тестове сортування та вимірюємо час
            Stopwatch stopwatchTest = Stopwatch.StartNew();
            sortingAlgorithm(arrayToSort);
            stopwatchTest.Stop();

            // Порівнюємо результати та виводимо інформацію
            bool sortingCorrect = ArraysEqual(referenceArray, arrayToSort);
            string result = sortingCorrect ? "Passed" : "Failed";
            Console.WriteLine($"{algorithmName}: {result}");
            Console.WriteLine($"Reference Sort Time: {stopwatchReference.ElapsedMilliseconds} ms");
            Console.WriteLine($"Test Sort Time: {stopwatchTest.ElapsedMilliseconds} ms");
            Console.WriteLine();
        }
        static void SelectionSort(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[minIndex])
                        minIndex = j;
                }
                if (minIndex != i)
                {
                    int temp = array[i];
                    array[i] = array[minIndex];
                    array[minIndex] = temp;
                }
            }
        }

        static void ShakerSort(int[] array)
        {
            bool swapped;
            do
            {
                swapped = false;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        Swap(array, i, i + 1);
                        swapped = true;
                    }
                }
                if (!swapped)
                    break;
                swapped = false;
                for (int i = array.Length - 2; i >= 0; i--)
                {
                    if (array[i] > array[i + 1])
                    {
                        Swap(array, i, i + 1);
                        swapped = true;
                    }
                }
            } while (swapped);
        }

        static int[] GenerateRandomArray(int size)
        {
            Random random = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(1000);
            }
            return array;
        }

        static void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        static bool ArraysEqual(int[] array1, int[] array2)
        {
            if (array1.Length != array2.Length)
                return false;

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }

            return true;
        }
    }
}
