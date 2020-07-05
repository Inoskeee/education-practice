using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Задача_3
{
    class Program
    {
        //Метод для проверки правильности ввода значения
        static void Input(out double input)
        {
            while (!double.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Введенные данные некорректны. Повторите ввод!");
            }
        }
        static void Main(string[] args)
        {
            //Проверка на нажатие нужной клавиши
            char checker = 'y';

            while (checker == 'y' || checker == 'Y' || checker == 'Н' || checker == 'н')
            {
                Console.Clear();
                Console.WriteLine("Введите координаты точки(х,у), чтобы проверить их принадлежность выделенной области:");
                //Действительные числа, координаты точки (х,у)
                double x;
                double y;


                Console.WriteLine("Введите координаты Х:");
                Input(out x);
                Console.WriteLine("Введите координаты Y:");
                Input(out y);

                //Переменная хранит результат TRUE/FALSE принадлежности к выделенной области
                bool result = ((x >= -1 && x <= 1) && (y <= 0 && y >= -2)) || ((x >= -1 && x < 0) && (y >= x)) || ((x <= 1 && x > 0) && (y <= x));

                if (result)
                {
                    Console.WriteLine($"Точка ({x};{y}) принадлежит выделенной области");
                }
                else
                {
                    Console.WriteLine($"Точка ({x};{y}) не принадлежит выделенной области");
                }
                //Можем продолжать проверять принадлежность разных точек
                Console.WriteLine("Для продолжения введите \"Y\", для выхода нажмите любую другую клавишу...");
                checker = Console.ReadKey().KeyChar;
            }

        }
    }
}
