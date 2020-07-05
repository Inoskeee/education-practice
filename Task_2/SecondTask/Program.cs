using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTask
{
    class Program
    {
        static void Main(string[] args)
        {
            //Добавить ограничения
            int number;
            //Переменная предпериода, поиска степени, и периода
            long prePer = 0, degree = 0, period;

            //number = int.Parse(File.ReadAllText("input.txt"));
            Console.WriteLine("Введите число n, чтобы посчитать период и предпериод числа 1/n:");
            while(!int.TryParse(Console.ReadLine(), out number) || number <2)
            {
                if (number < 2 && number > 0)
                {
                    Console.WriteLine("Число не может быть меньше 2, повторите попытку!");
                }
                else
                {
                    Console.WriteLine("Введенные данные некорректны! Повторите попытку.");
                }    
            }

            number = Math.Abs(number);

            if(number >= 2 && number <= 1000000)
            {
                //Ищем предпериод
                //Выделяем степень для 2
                while (number % 2 == 0)
                {
                    prePer++;
                    number = number / 2;
                }
                //Выделяем степень для 5
                while (number % 5 == 0)
                {
                    degree++;
                    number = number / 5;
                }
                //Берем большую степень за длину предпериода
                if (prePer < degree)
                {
                    prePer = degree;
                }

                //Период существует, если делители числа не только 2 и 5
                if (number > 1)
                {
                    //Первое число для деления
                    degree = 9;
                    //Длина периода
                    period = 1;

                    //Пока число вида 999... не разделится на введенное число 
                    while (degree % number > 0)
                    {
                        period++;
                        //остаток от деления на число * 10 + 9
                        degree = degree % number * 10 + 9;
                    }
                }
                else if(number == 1)
                {
                    //Если число раскладывается только на множители 2 и 5 то оно не периодично
                    period = 0;
                }
                else
                {
                    period = 1;
                }

                //File.WriteAllText("output.txt", $"{prePer} {period}");
                Console.WriteLine($"Длина предпериода: {prePer}\nДлина периода: {period}");
            }
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
