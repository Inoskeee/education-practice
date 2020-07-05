using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTwelve
{
    class Program
    {
        //Сортировка Шелла
        public static void ShellSort(int[] arr, out int count, out int compares)
        {
            //Количество сравнений
            compares = 0;
            //Количество перестановок
            count = 0;
            int j;
            //Определение расстояния шага
            int step = arr.Length / 2;
            while (step > 0)
            {
                //ищем элементы на расстоянии step и в случае необходимости меняем их местами
                for (int i = 0; i < (arr.Length - step); i++)
                {
                    j = i;
                    while ((j >= 0) && (arr[j] > arr[j + step]))
                    {
                        int tmp = arr[j];
                        arr[j] = arr[j + step];
                        arr[j + step] = tmp;
                        j -= step;
                        count++;
                        compares++;
                    }
                    
                }
                //Шаг сокращается
                step = step / 2;
                compares++;
            }
        }

        //Быстрая сортировка
        public static void QuickSort(int[] array, int start, int end, ref int count, ref int compares)
        {
            if (start > end)
            {
                return;
            }
            // Опорный элемент
            int pivot = array[(end - start) / 2];
            // Вспомогательные переменные для прохода по массиву
            int i = start, j = end;
            // Проход по массиву
            while (i < j)
            {
                // Поиск опорного элемента
                while (array[i] < pivot)
                {
                    compares++;
                    i++;
                }
                while (array[j] > pivot)
                {
                    compares++;
                    j--;
                }

                // Если не дошли до опорного элемента
                if (i < j)
                {
                    // Вспомогательная переменная для перестановки элементов
                    int Temp = array[i];
                    array[i++] = array[j];
                    array[j--] = Temp;
                    count++;
                }

                // Сортировка каждой части массива
                QuickSort(array, start, j, ref count, ref compares);
                QuickSort(array, i + 1, end, ref count, ref compares);
            }
        }

        //Дополнительные методы

        //Упорядочивание массива по убыванию
        public static int[] SortedDown(int[] array)
        {
            int[] new_array = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                new_array[i] = array[i];
            }
            Array.Reverse(new_array);
            return new_array;
        }
        //Упорядочивание массива по возрастанию
        public static int[] SortedUp(int[] array)
        {
            int[] new_array = new int[array.Length];
            for(int i = 0; i < array.Length; i++)
            {
                new_array[i] = array[i];
            }
            Array.Sort(new_array);
            return new_array;
        }
        //Ввод массива
        public static void Input(out int[] array_1, out int[] array_2)
        {
            //Переменная размера массива и проверки ввода меню
            int size, check;

            Console.WriteLine("Введите размер массива: ");
            while(!int.TryParse(Console.ReadLine(), out size) || size < 0)
            {
                Console.WriteLine("Введенные данные некорректны, повторите ввод!");
            }
            //Объявляем 2 массива для проверки двух сортировок
            array_1 = new int[size];
            array_2 = new int[size];
            Console.WriteLine("Выберите способ заполнения массива:\n" +
                "1. Ввести элементы с клавиатуры\n" +
                "2. Генерация случайных чисел");

            while (!int.TryParse(Console.ReadLine(), out check) || (check > 2 && check < 1))
            {
                Console.WriteLine("Введенные данные некорректны, повторите ввод!");
            }

            switch (check)
            {
                case 1: //Ввод элементов вручную
                    for(int i = 0; i < array_1.Length; i++)
                    {
                        Console.WriteLine("Введите {0} элемент массива:", i + 1);
                        int element;
                        while (!int.TryParse(Console.ReadLine(), out element))
                        {
                            Console.WriteLine("Введенные данные некорректны, повторите ввод!");
                        }
                        array_1[i] = element;
                        array_2[i] = element;
                    }
                    break;
                case 2: //Генерация элементов с клавиатуры
                    Random rand = new Random();
                    for (int i = 0; i < array_1.Length; i++)
                    {
                        int element = rand.Next(0, 101);
                        array_1[i] = element;
                        array_2[i] = element;
                    }
                    break;
                default: 
                    break;
            }

            Console.Clear();
        }
        //Печать массива
        public static void Print(int[] array)
        {
            for(int i = 0; i<array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            //Массивы
            int[] shellArray, quickArray, sortedUp, sortedDown;
            //Количество перестановок
            int count;
            //Количество сравнений
            int compares;

            //Заполняем оба массива
            Input(out shellArray, out quickArray);

            //Массивы упорядоченные по возрастанию и убыванию
            sortedUp = SortedUp(shellArray);
            sortedDown = SortedDown(sortedUp);

            //Печать изначальных массивов
            Console.WriteLine("Неупорядоченный массив:");
            Print(shellArray);
            Console.WriteLine("Упорядоченный по возрастанию массив:");
            Print(sortedUp);
            Console.WriteLine("Упорядоченный по убыванию массив:");
            Print(sortedDown);

            //Применяем сортировку Шелла для всех массивов
            Console.WriteLine("\nВыполняем сортировку Шелла каждого массива:\n");

            Console.WriteLine("Неупорядоченный массив:");
            ShellSort(shellArray, out count, out compares);
            Print(shellArray);
            Console.WriteLine("Количество перестановок: {0}", count);
            Console.WriteLine("Количество сравнений: {0}", compares);

            Console.WriteLine("Упорядоченный по возрастанию массив:");
            ShellSort(sortedUp, out count, out compares);
            Print(sortedUp);
            Console.WriteLine("Количество перестановок: {0}", count);
            Console.WriteLine("Количество сравнений: {0}", compares);

            Console.WriteLine("Упорядоченный по убыванию массив:");
            ShellSort(sortedDown, out count, out compares);
            Print(sortedDown);
            Console.WriteLine("Количество перестановок: {0}", count);
            Console.WriteLine("Количество сравнений: {0}", compares);

            //Возвращаем значения
            sortedUp = SortedUp(quickArray);
            sortedDown = SortedDown(sortedUp);
            //Обнуляем значения
            count = 0;
            compares = 0;

            //Применяем быструю сортировку для всех массивов
            Console.WriteLine("\nВыполняем быструю сортировку каждого массива:\n");

            Console.WriteLine("Неупорядоченный массив:");
            QuickSort(quickArray, 0, quickArray.Length-1, ref count, ref compares);
            Print(quickArray);
            Console.WriteLine("Количество перестановок: {0}", count);
            Console.WriteLine("Количество сравнений: {0}", compares);

            count = 0;
            compares = 0;

            Console.WriteLine("Упорядоченный по возрастанию массив:");
            QuickSort(sortedUp, 0, sortedUp.Length-1, ref count, ref compares);
            Print(sortedUp);
            Console.WriteLine("Количество перестановок: {0}", count);
            Console.WriteLine("Количество сравнений: {0}", compares);

            count = 0;
            compares = 0;

            Console.WriteLine("Упорядоченный по убыванию массив:");
            QuickSort(sortedDown, 0, sortedDown.Length-1, ref count, ref compares);
            Print(sortedDown);
            Console.WriteLine("Количество перестановок: {0}", count);
            Console.WriteLine("Количество сравнений: {0}", compares);


            Console.WriteLine("\nДля выхода нажмите любую клавишу...");
            Console.ReadKey();
        }

    }
}
