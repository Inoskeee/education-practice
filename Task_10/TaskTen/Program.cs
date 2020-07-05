using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTen
{
    class Program
    {
        //Метод получения графа из файла
        static Graph Input()
        {
            Console.WriteLine("Введите название текстового файла, в котором хранится матрица смежности");

            string name;
            Graph newGraph;

            do
            {
                name = Console.ReadLine();
            } while ((newGraph = Graph.CreateGraph(name))==null);

            return newGraph;
        }
        static void Main(string[] args)
        {
            //Переменные для проверки меню и правильности ввода значений
            byte menu;
            bool check = true;

            Graph graph = Input();


            while (check)
            {
                Console.Clear();
                Console.WriteLine("Выберите, что хотите сделать с графом:\n" +
                "1. Напечатать граф\n" +
                "2. Стянуть вершины\n" +
                "3. Сохранить граф в файл\n" +
                "0. Выйти из программы");
                while (!byte.TryParse(Console.ReadLine(), out menu) || (menu > 3 && menu < 0))
                {
                    Console.WriteLine("Введенные данные некорректны, повторите ввод!");
                }

                switch (menu)
                {
                    case 0:
                        check = false;
                        return;
                    case 1://Печатаем граф
                        Console.Clear();
                        Console.WriteLine(graph.ToString());
                        Console.WriteLine("Нажмите любую клавишу");
                        Console.ReadKey();
                        break;
                    case 2://Стягиваем вершину
                        int number;
                        Console.Clear();
                        Console.WriteLine("Введите номер вершины которую хотите стянуть:");
                        while(!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("Введенные данные некорректны, повторите ввод.");
                        }

                        while (graph.isVertex(number) == true)
                        {
                            graph.TightVertexes(number);
                        }
                        Console.WriteLine("Вершины успешно стянуты, нажмите любую клавишу!");
                        Console.ReadKey();
                        break;
                    case 3://Сохраняем в файл
                        Console.WriteLine("Введите название текстового файла, куда хотите сохранить граф:");
                        string name = "";
                        while (String.IsNullOrEmpty(name))
                        {
                            name = Console.ReadLine();
                        }
                        graph.SaveGraph(name + ".txt");
                        Console.WriteLine("Граф успешно сохранен, нажмите любую клавишу!");
                        Console.ReadKey();
                        break;
                }

            }
            
            Console.ReadLine();
        }
    }
}
