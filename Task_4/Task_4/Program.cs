using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4
{
    class Program
    {
        //Метод для подсчета многочлена методом Горнера
        static Complex Gorner(Complex[] Coefs)
        {
            //Коэффициенты х и у
            Complex X = Coefs[0];
            //Хранит в себе результат в виде комплексного числа
            Complex Result = Coefs[Coefs.Length - 1];
            //Считаем многочлен
            for (int i = Coefs.Length - 1; i > 1; i--)
            {
                Result = Result * X + Coefs[i - 1];
            }

            return Result;
        }

        //Метод для ввода коэффициентов последовательности
        static Complex[] Input()
        {
            //Степень последовательности
            int degree;
            //Коэффициенты Х и У
            double x, y;
            //Коэффициенты А и В
            double a, b;

            Console.WriteLine("Введите степень многочлена");
            while(!int.TryParse(Console.ReadLine(), out degree) || degree <= 0)
            {
                if (degree <= 0)
                {
                    Console.WriteLine("Степень должна быть положительной. Повторите ввод");
                }
                else
                {
                    Console.WriteLine("Введенные данные некорректны, повторите ввод.");
                }
            }

            Complex[] numbers = new Complex[degree+1];

            Console.WriteLine("Введите число x:");
            DoubleInput(out x);

            Console.WriteLine("Введите число y:");
            DoubleInput(out y);

            numbers[0] = new Complex(x, y);

            byte check;
            Console.WriteLine("Выберите способ получения коэффициентов a и b\n" +
                "1. Ввод значений с клавиатуры\n" +
                "2. Генерация случайных чисел");
            while(!byte.TryParse(Console.ReadLine(), out check) || (check > 2 && check < 1))
            {
                Console.WriteLine("Введенные данные некорректны, повторите ввод.");
            }

            switch (check)
            {
                case 1:
                    for(int i = 1; i < numbers.Length; i++)
                    {
                        Console.WriteLine("Введите a{0}:", i);
                        DoubleInput(out a);
                        Console.WriteLine("Введите b{0}:", i);
                        DoubleInput(out b);
                        numbers[i] = new Complex(a, b);
                    }
                    break;
                case 2:
                    for(int i = 1; i < numbers.Length; i++)
                    {
                        numbers[i] = new Complex();
                    }
                    break;
                default:
                    break;
            }

            Console.Clear();

            return numbers;
        }

        //Метод выполняющий печать всей последовательности
        public static void Print(Complex[] Coefs, int degree)
        {
            string X = $"({Coefs[0].Real} + {Coefs[0].Imaginary}i)";
            string Result = $"({Coefs[1].Real} + {Coefs[1].Imaginary}i)*" + X + $"^{degree}";
            Console.WriteLine("Итоговая последовательность:");

            for(int i = 2; i < Coefs.Length; i++)
            {
                degree--;
                Result = Result + "*" + X + $"^{degree}" + "+" + $"({Coefs[i].Real} + {Coefs[i].Imaginary}i)";
            }
            Console.WriteLine(Result);

            Console.WriteLine();
            
        }

        //Метод для проверки ввода коэффициентов последовательности
        static void DoubleInput(out double a)
        {
            while (!double.TryParse(Console.ReadLine(), out a))
            {
                Console.WriteLine("Введенные данные некорректны, повторите ввод.");
            }
        }
        static void Main(string[] args)
        {
            //Массив комплексных чисел всей последовательности
            Complex[] numbers = Input();

            //Печать последовательности
            Print(numbers, numbers.Length-1);
            //Вычисление значения многочлена
            Console.WriteLine("Значение многочлена по правилу Горнера равно : {0}", Gorner(numbers));

            Console.WriteLine("Для выхода нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
