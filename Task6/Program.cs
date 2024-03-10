using System.Diagnostics;

namespace Task6
{
    internal class Program
    {
        delegate int[] SortingAlgorithm(int[] array);

        static void Main(string[] args)
        {
            SortingAlgorithm standardSelectionSort = StandardSelectionSort;
            SortingAlgorithm studentSelectionSort = StudentSelectionSort;

            SortingAlgorithm standardShakerSort = StandardShakerSort;
            SortingAlgorithm studentShakerSort = StudentShakerSort;

            CompareStandardWithStudent(standardSelectionSort, studentSelectionSort);
            CompareStandardWithStudent(standardShakerSort, studentShakerSort);
        }
        static void CompareStandardWithStudent(SortingAlgorithm standard, SortingAlgorithm student)
        {
            var arrayToSort = GenerateRandomArray(10000);

            double standardTime, studentTime;
            int[] arrForStandard = new int[arrayToSort.Length], arrForStudent = new int[arrayToSort.Length];

            using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));

            arrayToSort.CopyTo(arrForStandard, 0);
            arrayToSort.CopyTo(arrForStudent, 0);
            int[] referenceArray;
            try
            {
                var st = Stopwatch.StartNew();
                referenceArray = standard(arrForStandard);
                st.Stop();
                standardTime = st.Elapsed.TotalMilliseconds;
                Console.WriteLine($"Час виконання стандартного алгоритму: {standardTime} мс");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Помилка у стандартному методі: {e.Message}");
                return;
            }

            try
            {
                var st = Stopwatch.StartNew();

                var studentResult = Task.Run(() => student(arrForStudent), cancellationTokenSource.Token);
                studentResult.Wait(cancellationTokenSource.Token);

                st.Stop();
                studentTime = st.Elapsed.TotalMilliseconds;
                Console.WriteLine($"Час виконання студентського алгоритму: {studentTime} мс");

                if (!ArraysEqual(referenceArray, studentResult.Result))
                {
                    Console.WriteLine("Студентський алгоритм не відсортував масив правильно");
                    return;
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Час виконання студентського алгоритму перевищив 5 секунд");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Помилка у студентському методі: {e.Message}");
                return;
            }

            if (standardTime - 200 <= studentTime && studentTime <= standardTime + 200)
            {
                Console.WriteLine("Алгоритми мають однаковий час виконання");
            }
            else
            {
                Console.WriteLine("Алгоритми мають різний час виконання");
            }
        }

        static int[] StandardSelectionSort(int[] array)
        {
            var len = array.Length;
            for (var i = 0; i < len - 1; i++)
            {
                var minIndex = i;
                for (var j = i + 1; j < len; j++)
                {
                    if (array[j] < array[minIndex])
                    {
                        minIndex = j;
                    }
                }
                if (minIndex != i)
                {
                    (array[i], array[minIndex]) = (array[minIndex], array[i]);
                }
            }
            return array;

            //int n = array.Length;

            //for (int i = 0; i < n - 1; i++)
            //{
            //    for (int j = 0; j < n - i - 1; j++)
            //    {
            //        if (array[j] > array[j + 1])
            //        {

            //            int temp = array[j];
            //            array[j] = array[j + 1];
            //            array[j + 1] = temp;
            //        }
            //    }
            //}

            return array;
        }

        static int[] StandardShakerSort(int[] array)
        {
            var len = array.Length;
            bool swapped;
            do
            {
                swapped = false;
                for (var i = 0; i < len - 1; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        (array[i], array[i + 1]) = (array[i + 1], array[i]);
                        swapped = true;
                    }
                }
                if (!swapped)
                    break;
                swapped = false;
                for (var i = len - 2; i >= 0; i--)
                {
                    if (array[i] > array[i + 1])
                    {
                        (array[i], array[i + 1]) = (array[i + 1], array[i]);
                        swapped = true;
                    }
                }
            } while (swapped);
            return array;
        }

        static int[] StudentSelectionSort(int[] array)
        {
            while (true)
            {

            }
            return array;
        }

        static int[] StudentShakerSort(int[] array)
        {
            int left = 0;
            int right = array.Length - 1;
            int temp;
            bool swapped = true;
            while (left < right && swapped)
            {
                swapped = false;
                for (int i = left; i < right; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swapped = true;
                    }
                }
                right--;
                swapped = false;
                for (int j = right; j > left; j--)
                {
                    if (array[j - 1] > array[j])
                    {
                        temp = array[j];
                        array[j] = array[j - 1];
                        array[j - 1] = temp;
                        swapped = true;

                    }
                }
                left++;
            }
            return array;
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

        static bool ArraysEqual(int[] referenceArray, int[] studentArray)
        {
            if (referenceArray.Length != studentArray.Length)
                return false;

            for (int i = 0; i < referenceArray.Length; i++)
            {
                if (referenceArray[i] != studentArray[i])
                    return false;
            }

            return true;
        }
    }
}
