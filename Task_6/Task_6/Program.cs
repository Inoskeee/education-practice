using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Task_6
{
    class Program
    {
        //Метод для ввода элементов последовательности
        static void InputElements (out double[] elements, out int maxCount, out int restrictor, out int limitedElements)
        {
            Console.WriteLine("Введите количество элементов последовательности:");
            while (!int.TryParse(Console.ReadLine(), out maxCount) || maxCount<4)
            {
                if(maxCount < 4)
                {
                    Console.WriteLine("Количество элементов не может быть меньше 4");
                }
                else
                {
                    Console.WriteLine("Данные некорректны, повторите ввод.");
                }
            }

            Console.WriteLine("Введите ограничивающий элемент последовательности:");
            while (!int.TryParse(Console.ReadLine(), out restrictor))
            {
                Console.WriteLine("Данные некорректны! Повторите ввод.");
            }

            Console.WriteLine("Введите количество элементов последовательности больших ограничевающего:");
            while (!int.TryParse(Console.ReadLine(), out limitedElements) || limitedElements>maxCount)
            {

                if (limitedElements > maxCount)
                {
                    Console.WriteLine("Количество элементов не может быть больше общего числа элементов.");
                }
                else
                {
                    Console.WriteLine("Данные некорректны! Повторите ввод.");
                }
            }

            //Массив всех элементов последовательности
            elements = new double[maxCount];

            //Проверяем количество элементов которые нужно вычислить до ограничения
            if (limitedElements <= 0)
            {
                return;
            }
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Введите {0} элемент последовательности:", i + 1);
                while (!double.TryParse(Console.ReadLine(), out elements[i]))
                {
                    Console.WriteLine("Данные некорректны! Повторите ввод.");
                }
                if (elements[i] > restrictor)
                {
                    limitedElements--;
                }
                if(limitedElements <= 0)
                {
                    return;
                }
            }


        }
        //Создание последовательности, рекурсивный метод
        static void Sequence(double[] elements, int curentElement, int restrictor, int limitedElements)
        {
            //Если количество нулевое, значит вычислены элементы большие чем ограничитель
            if (limitedElements <= 0)
            {
                Console.WriteLine("Вычеслены элементы последовательности, которые больше чем {0}", restrictor);
                return;
            }
            else
            {
                //Вычисляем элемент последовательности и печатаем его
                elements[curentElement] = elements[curentElement - 3] * (((7 / 3) * elements[curentElement - 1] + elements[curentElement - 2]) / 2);
                Console.WriteLine("A{0} = {1}", curentElement + 1, elements[curentElement]);
                if (elements[curentElement] > restrictor)
                {
                    //Если элемент больше ограничителя уменьшаем счетчик
                    limitedElements--;
                }
                //Если это последний элемент последовательности выводим сообщение
                if (curentElement + 1 == elements.Length)
                {
                    Console.WriteLine("Вычеслено {0} элементов последовательности", elements.Length);
                    return;
                }

                Sequence(elements, curentElement + 1, restrictor, limitedElements);
            }
        }
        static void Main(string[] args)
        {
            //массив элементов последовательности
            double[] elements = new double[] { };
            //Максимальное количество элементов, ограничитель и количество элементов для ограничителя
            int maxCount = 0, restrictor = 0, limitedElements = 0;
            //Текущий элемент = 3, т.к. первые 3 вводятся пользователем 
            int currentElement = 3;

            InputElements(out elements, out maxCount, out restrictor, out limitedElements);

            Sequence(elements, currentElement, restrictor, limitedElements);

            Console.WriteLine("Для выхода нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
