using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_5
{
    class Program
    {
        //Метод для ввода размера матрицы
        static void InputSize(out int input)
        {
            while (!int.TryParse(Console.ReadLine(), out input) || input < 3)
            {
                if(input < 3 && input != 0)
                {
                    Console.WriteLine("Размер такой матрицы не может быть меньше 3, чтобы получить выделенную область.");
                }
                else
                {
                    Console.WriteLine("Введенные данные некорректны, повторите ввод!");
                }
            }
        }
        //Метод для ввода значений матрицы
        static void Input(out int input)
        {
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Введенные данные некорректны, повторите ввод!");
            }
        }
        //Метод создания матрицы
        static int[,] CreateMatrix(int n)
        {
            int checker;

            int[,] matrix = new int[n, n];
            Console.WriteLine("Выберите способ генерации матрицы:\n" +
                              "1. Ввод значений с клавиатуры\n" +
                              "2. Заполнение случайными числами");
            while (!int.TryParse(Console.ReadLine(), out checker) || (checker > 2 && checker < 1))
            {
                Console.WriteLine("Введеннные данные некорректны, повторите ввод!");
            }

            switch (checker)
            {
                case 1:
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            Console.Clear();
                            Console.WriteLine($"Введите ({i+1}, {j+1}) элемент матрицы:");
                            Input(out matrix[i,j]);
                        }
                    }

                    Console.WriteLine("Матрица:");
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            Console.Write(matrix[i, j] + " ");
                        }
                        Console.WriteLine();
                    }


                    break;
                case 2:
                    Random random = new Random();

                    Console.WriteLine("Матрица:");
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            matrix[i, j] = random.Next(0, 100);
                            Console.Write(matrix[i, j] + " ");
                        }
                        Console.WriteLine();
                    }
                    break;
                default:
                    break;
            }
            return matrix;

        }

        static void Main(string[] args)
        {
            //Матрица
            int[,] matrix;
            //Размер матрицы
            int size;
            //Максимальное значение выделеной области
            int max;


            Console.WriteLine("Введите число n, для создания квадратной матрицы n x n");

            InputSize(out size);

            matrix = CreateMatrix(size);

            //Присваиваем максимальному значению первый элемент матрицы
            max = matrix[0, 0];

            Console.WriteLine();
            Console.WriteLine("Выделенная область матрицы:");
            //Определяем выделенную область в матрице
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    //Если значение принадлежит выделенной области, то проверяем его
                    //Идем до середины матрицы
                    if (j >= i && j< size-i)
                    {
                        Console.Write(matrix[i, j] + " ");
                        if (matrix[i, j] > max)
                        {
                            max = matrix[i, j];
                        }
                    }
                    //Идем с середины матрицы до ее конца
                    else if(j>=size-i-1 && j<=i && size - i <= size/2)
                    {
                        Console.Write(matrix[i, j] + " ");
                        if (matrix[i, j] > max)
                        {
                            max = matrix[i, j];
                        }
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                }
                Console.WriteLine();
            }



            Console.WriteLine("Самое большое значение выделенной области матрицы: {0}", max);

            Console.WriteLine("Для выхода нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
