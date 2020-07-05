using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenTask
{
    //16. Сгенерировать все сочетания из N элементов по K без повторений и выписать их в лексикографическом порядке.
    class Program
    {
        //Статическая переменная для подсчета количества сочетаний
        static int Count = 1;

        //Метод для генерации следующего сочетания
        static bool NextSet(int[] comb, int n, int k)
        {
            //Максимальное количество элементов = К
            int maxValue = k;

            for(int i = maxValue-1; i >=0; i--)
            {
                if(comb[i] < n - maxValue + 1 + i)
                {
                    comb[i]++;
                    for(int j = i + 1; j < maxValue; j++)
                    {
                        comb[j] = comb[j - 1] + 1;
                    }
                    //Если сочетание сгенерировано
                    return true;
                }
            }
            //Если сочетаний больше нет
            return false;
        }
        //Метод печати каждой комбинации
        static void Print(int[] comb, int K)
        {
            Console.Write(Count + ": (");
            for(int i = 0; i < K; i++)
            {
                Console.Write(comb[i] + " ");
            }
            Console.Write(")\n");
            Count++;
        }
        //Метод проверки ввода значений
        static void Input(out int input)
        {
            while(!int.TryParse(Console.ReadLine(), out input) || input <=0)
            {
                if(input <= 0)
                {
                    Console.WriteLine("Количество элементов не может быть нулевым или отрицательным!");
                }
                else
                {
                    Console.WriteLine("Вы ввели некорректные данные, повторите ввод.");
                }
            }
        }

        static void Main(string[] args)
        {
            //N - количество элементов всего
            //K - количество элементов в сочетании
            int N, K;
            Console.WriteLine("Введите число различных элементов:");
            Input(out N);
            Console.WriteLine("Введите максимальное количество элементов в сочетании:");
            while (!int.TryParse(Console.ReadLine(), out K) || K > N)
            {
                if (K > N)
                {
                    Console.WriteLine("Количество элементов в сочетании не может быть больше количества элементов");
                }
                else
                {
                    Console.WriteLine("Вы ввели некорректные данные, повторите ввод.");
                }
               
            }

            Console.WriteLine("Сочетания из N по K без повторений:");
            //Массив, содержащий в себе комбинацию чисел
            int[] combinations = new int[K];
            //Генерация самой первой комбинации
            for(int i = 0; i<combinations.Length; i++)
            {
                combinations[i] = i + 1;
            }
            Print(combinations, K);

            if (N >= K)
            {
                //Генерация комбинаций, пока они существуют
                while(NextSet(combinations, N, K))
                {
                    Print(combinations, K);
                }
            }

            Console.WriteLine("Для выхода нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
