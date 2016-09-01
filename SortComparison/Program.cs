using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortComparison
{
    class Program
    {
        //Code taken from exchangecore.com
        public delegate void SortMethod(int[] list);

        static void ShowSortingTimes(String methodName, SortMethod method, int[] list)
        {
            double sortTime;
            Console.WriteLine("{0} of {1} items:", methodName, list.Length);
            FillRandom(list, 10000);
            sortTime = GetSortingTime(method, list);
            Console.WriteLine("\t{0} miliseconds", sortTime);           
        }

        static double GetSortingTime(SortMethod method, int[] list)
        {
            int startTime, stopTime;
            startTime = Environment.TickCount;
            method(list);
            stopTime = Environment.TickCount;
            return (stopTime - startTime);
        }
        
        static Random rnd = new Random();
        static void FillRandom(int[] arr, int max)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = rnd.Next(max + 1);
        }

        static int FindMax(int[] arr, int last)
        {
            // find the index of the largest number in the array.
            int maxIndex = 0;
            for (int i = 1; i <= last; i++)
            {
                // if the number in location i is bigger than the largest #
                // we have seen before, remember i.
                if (arr[i] > arr[maxIndex])
                    maxIndex = i;
            }
            return maxIndex;
        }

        static void swap(int[] arr, int m, int n)
        {
            int tmp = arr[m];
            arr[m] = arr[n];
            arr[n] = tmp;
        }

        static void SelectionSort(int[] list)
        {
            int last = list.Length - 1;
            do
            {
                int biggest = FindMax(list, last);
                swap(list, biggest, last);
                last--;
            } while (last > 0);
            return;
        }

        static void InsertionSort(int[] list)
        {
            for (int i = 1; i < list.Length; i++)
            {
                if (list[i] < list[i - 1])
                {
                    int temp = list[i];
                    int j;
                    for (j = i; j > 0 && list[j - 1] > temp; j--)
                        list[j] = list[j - 1];
                    list[j] = temp;
                }
            }
        }

        static void BubbleSort(int[] list)
        {
            for (int i = list.Length - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (list[j] > list[j + 1])
                        swap(list, j, j + 1);
                }
            }

        }

        static void QuickSort(int[] a)
        {
            QuickSortRecursive(a, 0, a.Length);
        }

        static void QuickSortRecursive(int[] a, int low, int high)
        {
            if (high - low <= 1) return;
            int pivot = a[high - 1];
            int split = low;
            for (int i = low; i < high - 1; i++)
                if (a[i] < pivot)
                    swap(a, i, split++);
            swap(a, high - 1, split);
            QuickSortRecursive(a, low, split);
            QuickSortRecursive(a, split + 1, high);
            return;
        }

        static int Partition(int[] arr, int x)
        {
            int lowMark = 0, highMark = arr.Length - 1;

            while (true)
            {
                // find the first item out of place from the start
                while (lowMark < arr.Length && arr[lowMark] <= x)
                    lowMark++;
                // first the first out of place from the end
                while (highMark >= 0 && arr[highMark] > x)
                    highMark--;
                if (lowMark > highMark)
                    return highMark;
                // swap those two items
                swap(arr, lowMark, highMark);
            }
        }

        
        static void MergeSort(int[] inputArray)
        {
            
            int left = 0;
            int right = inputArray.Length - 1;
            InternalMergeSort(inputArray, left, right);
            
        }

        static void InternalMergeSort(int[] inputArray, int left, int right)
        {
            int mid = 0;

            if (left < right)
            {
                mid = (left + right) / 2;
                InternalMergeSort(inputArray, left, mid);
                InternalMergeSort(inputArray, (mid + 1), right);

                MergeSortedArray(inputArray, left, mid, right);
               
            }
        }
        static void MergeSortedArray(int[] inputArray, int left, int mid, int right)
        {
            int index = 0;
            int total_elements = right - left + 1; //BODMAS rule
            int right_start = mid + 1;
            int temp_location = left;
            int[] tempArray = new int[total_elements];

            while ((left <= mid) && right_start <= right)
            {
                if (inputArray[left] <= inputArray[right_start])
                {
                    tempArray[index++] = inputArray[left++];
                }
                else
                {
                    tempArray[index++] = inputArray[right_start++];
                }
            }

            if (left > mid)
            {
                for (int j = right_start; j <= right; j++)
                    tempArray[index++] = inputArray[right_start++];
            }
            else
            {
                for (int j = left; j <= mid; j++)
                    tempArray[index++] = inputArray[left++];
            }

            //Array.Copy(tempArray, 0, inputArray, temp_location, total_elements); // just another way of accomplishing things (in-built copy)
            for (int i = 0, j = temp_location; i < total_elements; i++, j++)
            {
                inputArray[j] = tempArray[i];
            }
        }

        static void SwapWithTemp(ref int valOne, ref int valTwo)
        {
            int temp = valOne;
            valOne = valTwo;
            valTwo = temp;
        }

        static void HeapSort(int[] inputArray)
        {
            
            for (int index = (inputArray.Length / 2) - 1; index >= 0; index--)
                Heapify(inputArray, index, inputArray.Length);

            for (int index = inputArray.Length - 1; index >= 1; index--)
            {
                SwapWithTemp(ref inputArray[0], ref inputArray[index]);
                Heapify(inputArray, 0, index - 1);
            }
            
        }


        static void Heapify(int[] inputArray, int root, int bottom)
        {
            bool completed = false;
            int maxChild;

            while ((root * 2 <= bottom) && (!completed))
            {
                if (root * 2 == bottom)
                    maxChild = root * 2;
                else if (inputArray[root * 2] > inputArray[root * 2 + 1])
                    maxChild = root * 2;
                else
                    maxChild = root * 2 + 1;

                if (inputArray[root] < inputArray[maxChild])
                {
                    Swap(ref inputArray[root], ref inputArray[maxChild]);
                    root = maxChild;
                    
                }
                else
                {
                    completed = true;
                }
            }
        }

        static void Swap(ref int valOne, ref int valTwo)
        {
            valOne = valOne + valTwo;
            valTwo = valOne - valTwo;
            valOne = valOne - valTwo;
        }

       static void RadixSort(int[] data)
        {
            int i, j;
            int[] temp = new int[data.Length];

            for (int shift = 31; shift > -1; --shift)
            {
                j = 0;

                for (i = 0; i < data.Length; ++i)
                {
                    bool move = (data[i] << shift) >= 0;

                    if (shift == 0 ? !move : move)
                        data[i - j] = data[i];
                    else
                        temp[j++] = data[i];
                }

                Array.Copy(temp, 0, data, data.Length - j, j);
            }
        }

        static void Main(string[] args)
        {
            int x = 100000; // list size variable
            int[] list = new int[x];
            int select; // case selection variable
            do
            {
                Console.WriteLine("Make a Selection:");
                Console.WriteLine("\t1: Insertion Sort");
                Console.WriteLine("\t2: Selection Sort");
                Console.WriteLine("\t3: Bubble Sort");
                Console.WriteLine("\t4: Quick Sort");
                Console.WriteLine("\t5: Merge Sort");
                Console.WriteLine("\t6: Heap Sort");
                Console.WriteLine("\t7: Radix Sort");
                Console.WriteLine("\t8: Change Array Size. Currently {0}", x);
                Console.WriteLine("\t0: Quit");
                Console.Write("Selection: "); select = int.Parse(Console.ReadLine());


                FillRandom(list, x);
                switch (select)
                {
                    case 1:
                        for (int i = 0; i < 10; i++)
                        {
                            ShowSortingTimes("Insertion Sort", InsertionSort, list);
                        }
                        break;

                    case 2:
                        for (int i = 0; i < 10; i++)
                        {
                            ShowSortingTimes("Selection Sort", SelectionSort, list);
                        }
                        break;

                    case 3:
                        for (int i = 0; i < 10; i++)
                        {
                            ShowSortingTimes("Bubble Sort", BubbleSort, list);
                        }
                        break;

                    case 4:
                        for (int i = 0; i < 10; i++)
                        {
                            ShowSortingTimes("Quick Sort", QuickSort, list);
                        }
                        break;

                    case 5:

                        for (int i = 0; i < 10; i++)
                        {
                            ShowSortingTimes("MergeSort", MergeSort, list);
                        }
                        break;
                    case 6:

                        for (int i = 0; i < 10; i++)
                        {
                            ShowSortingTimes("HeapSort", HeapSort, list);
                        }
                        break;

                    case 7:
                        for (int i = 0; i < 10; i++)
                        {
                            ShowSortingTimes("RadixSort", RadixSort, list);
                        }
                        break;
                    case 8:
                        do
                        {
                            Console.WriteLine("New Array Size: ");
                            x = int.Parse(Console.ReadLine());
                        } while (x < 0);
                        list = new int[x];
                        break;
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            } while (select != 0);
        }
    }
}