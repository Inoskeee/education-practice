using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightTask
{
    class Program
    {
        //Метод для открытия файла с графом
        static Graph Input()
        {
            Console.WriteLine("Введите название текстового файла, в котором хранится матрица инцедентности");

            string name;
            Graph newGraph;

            do
            {
                name = Console.ReadLine();
            } while ((newGraph = Graph.OpenGraph(name)) == null);

            return newGraph;
        }

        static void Main(string[] args)
        {
            //Переменная для проверки значений ввода в меню
            byte checker;
            Console.WriteLine("Выберите способ задания матрицы инцидентности:\n" +
                "1. Загрузить из файла\n" +
                "2. Сгенерировать случайным образом");
            while(!byte.TryParse(Console.ReadLine(), out checker) || (checker > 2 || checker < 1))
            {
                Console.WriteLine("Введенные данные некорректны! Повторите ввод.");
            }

            switch (checker)
            {
                case 1://Получаем граф из файла
                    Console.Clear();
                    Graph graph = Input();
                    
                    Console.WriteLine(graph.ToString());
                    if (graph != null)
                    {
                        Console.WriteLine("\nВыполняем поиск блоков в графе...\n");
                        graph.DeepSearch(0, 0);
                    }
                    break;
                case 2://Генерируем граф по заданным пользователем параметрам
                    Console.Clear();
                    int vertex, edges;
                    Console.WriteLine("Введите количество вершин графа:");
                    while (!int.TryParse(Console.ReadLine(), out vertex))
                    {
                        Console.WriteLine("Введенные данные некорректны! Повторите ввод.");
                    }
                    Console.WriteLine("Введите количество ребер графа:");
                    //Проверка на то, чтобы ребер было не больше допустимого количества
                    int maxEdges = (vertex * (vertex - 1)) / 2;
                    while (!int.TryParse(Console.ReadLine(), out edges) || edges > maxEdges)
                    {
                        if(edges > maxEdges)
                        {
                            Console.WriteLine("В графе не может быть ребер больше чем n(n-1)/2");
                        }
                        else
                        {
                            Console.WriteLine("Введенные данные некорректны! Повторите ввод.");
                        }
                    }
                    //Создание нового графа и поиск блоков в нем
                    Graph new_graph = Graph.CreateGraph(vertex, edges);
                    Console.WriteLine(new_graph.ToString());
                    if (new_graph != null)
                    {
                        Console.WriteLine("\nВыполняем поиск блоков в графе...\n");
                        new_graph.DeepSearch(0, 0);
                    }

                    break;
            }

            Console.WriteLine("Для выхода нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
