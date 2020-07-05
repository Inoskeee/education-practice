using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NineTask
{
    class Program
    {
        //Рекурсивный метод подсчета элементов в листе
        static int CountRec(DuplexPoint<int> point, int count)
        {
            count++;

            if (point.Next == null)
            {
                return count;
            }

            return CountRec(point.Next, count);
        }
        //Обычный метод подсчета элементов в листе
        static int Count(DuplexPoint<int> point, int count)
        {
            count++;

            while (point.Next != null)
            {
                count++;
                point = point.Next;
            }
            return count;
        }
        //Метод создания Листа
        static LinkedList<int> CreateList()
        {
            Console.WriteLine("Введите сколько элементов хотите добавить в список:");
            //checker - Переменная для проверки меню
            //countEl - Переменная для ожидаемого количества элементов
            byte checker;
            int countEl;

            while(!int.TryParse(Console.ReadLine(), out countEl) || countEl <= 0)
            {
                if(countEl <= 0)
                {
                    Console.WriteLine("Количество элементов не может быть не числом, отрицательным или равным нулю");
                }
                else
                {
                    Console.WriteLine("Данные введены некорректно, повторите ввод");
                }
            }

            LinkedList<int> list = new LinkedList<int>();

            Console.WriteLine("Выберите способ добавления элементов\n" +
                              "1. Ввести элементы с клавиатуры\n" +
                              "2. Генерация случайных чисел");

            while (!byte.TryParse(Console.ReadLine(), out checker) || (checker > 2 && checker < 1))
            {
                Console.WriteLine("Данные введены некорректно, повторите ввод");
            }

            switch (checker)
            {
                case 1:
                    for (int i = 0; i < countEl; i++)
                    {
                        Console.WriteLine("Введите {0} элемент:", i + 1);
                        int element;
                        while (!int.TryParse(Console.ReadLine(), out element))
                        {
                            Console.WriteLine("Данные введены некорректно, повторите ввод");
                        }
                        list.Add(element);
                        Console.Clear();
                    }
                    break;
                case 2:
                    Random rand = new Random();
                    for (int i = 0; i < countEl; i++)
                    {
                        list.Add(rand.Next(0, 100));
                    }
                    break;
                default: 
                    break;
            }
            Console.Clear();
            return list;
        }
        static void Main(string[] args)
        {
            //Результаты рекурсивного и нерекурсивного метода
            int countRecurs = 0;
            int countGeneral = 0;

            LinkedList<int> list = CreateList();

            //Рекурсивный метод
            countRecurs = CountRec(list.Head, 0);

            //Обычный метод
            countGeneral = Count(list.Head, 0);

            Console.WriteLine("Список:");
            //Выводим наш список
            foreach(var item in list)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

            Console.WriteLine("В списке {0} элементов(рекурсивный метод)", countRecurs);
            Console.WriteLine("В списке {0} элементов(обычный метод)", countGeneral);

            Console.WriteLine("Для выхода нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
