using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EightTask
{
    //Граф
    public class Graph
    {
        private int vertixes;
        private int edges;
        private int blocks;
        private int checkedCount;

        //Матрица инцедентности
        public List<int[]> Matrix;
        //Стек, хранящий вершины блоков
        public Stack<int> Block;
        //Массив, хранящий посещенные ребра и вершины
        public int[] CheckedEdges;
        public int[] ParentVerticies;

        //Количество вершин
        public int Verticies { get { return vertixes; } set { vertixes = value; } }
        //Количество ребер
        public int Edges { get { return edges; } set { edges = value; } }

        //Количество блоков
        public int BlockCount { get { return blocks; } set { blocks = value; } }
        //Количество посещенных вершин
        public int CheckedCount { get { return checkedCount; } set { checkedCount = value; } }


        public Graph(int vertix, int edges, List<int[]> matrix)
        {
            Matrix = matrix;

            Verticies = vertix;

            Edges = edges;

            BlockCount = 1;

            CheckedCount = 0;
            ParentVerticies = new int[vertix];
            CheckedEdges = new int[edges];
            //Заполняем оба массива нулевыми элементами
            //чтобы в последствии можно было совершить перебор
            for(int i = 0; i<vertix; i++)
            {
                ParentVerticies[i] = 0;
            }
            for (int i = 0; i < edges; i++)
            {
                CheckedEdges[i] = 0;
            }

            Block = new Stack<int>(edges);


        }

        //Метод получения графа из файла
        public static Graph OpenGraph(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    List<int[]> someMatrix = new List<int[]>();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Считываем каждую строку и записываем ее в матрицу
                        int[] array = line.Split(' ').Select(item => int.Parse(item)).ToArray();
                        for(int i = 0; i< array.Length; i++)
                        {
                            //Если значение не 0 и не 1, то матрица задана неверно
                            if(array[i] != 0 && array[i] != 1)
                            {
                                throw new Exception("Неверно задана матрица инцедентности");
                            }
                        }
                        someMatrix.Add(array);

                    }

                    //Проверяем, чтобы ребер в матрице было не больше максимально допустимого количества
                    if(someMatrix[0].Length > (someMatrix.Count * (someMatrix.Count - 1)) / 2)
                    {
                        throw new Exception("Число рёбер в графе больше чем (n*(n-1))/2");
                    }

                    return new Graph(someMatrix.Count, someMatrix[0].Length, someMatrix);

                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл, который вы указали не был найден, повторите ввод!");
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //Метод поиска в глубину в матрице инцедентций
        public int DeepSearch(int vertex, int Parent)
        {
            try
            {
                ParentVerticies[vertex] = CheckedCount++;
                // определяем начальную родительскую вершину для поиска точек сочленения
                int minimum = ParentVerticies[vertex];
                // Перебор всех ребер, исходящих из вершины
                for (int Edge = 0; Edge < Edges; Edge++)
                {
                    if (Matrix[vertex][Edge] == 1)
                    {
                        int NextVertix = 0;
                        //Поиск новой вершины
                        while (NextVertix < Edges && Matrix[NextVertix][Edge] == 0 || NextVertix == vertex)
                        {
                            NextVertix++;
                        }
                        if (NextVertix != Parent)
                        {
                            //Сохраняет родительскую вершину смежную со следующей
                            int toVert;

                            int currentSize = Block.Count;
                            // Если текущего ребра еще нет в стеке
                            if (Array.BinarySearch(CheckedEdges, Edge) < 0)
                            {
                                //Записываем номер ребра соединющего текущую и следующую вершины
                                CheckedEdges.Append(Edge);
                                Block.Push(Edge);
                            }

                            //Если вершина еще не посещена
                            if (ParentVerticies[NextVertix] == 0)
                            {
                                // Продолжаем обход из этой вершины, ищем точки сочленения
                                toVert = DeepSearch(NextVertix, vertex);
                                //Если нашли точку сочленения, то печатаем блок
                                if (toVert >= ParentVerticies[vertex])
                                {
                                    CreateBlock(currentSize);
                                }
                            }
                            else
                            {
                                toVert = ParentVerticies[NextVertix];
                            }
                            //Получаем младшее значение дочерних элементов с вершиной
                            minimum = Math.Min(minimum, toVert);
                        }
                    }
                }
                return minimum;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Не удалось выделить блоки из этого графа, попробуйте повторить попытку.");
                return -1;
            }
            
        }
        //Метод определения блоков в матрице
        private void CreateBlock(int currentSize)
        {
            Console.Write($"Блок {BlockCount++} состоит из ребер под номерами: ");
            while (Block.Count != currentSize)
            {
                Console.Write(" {0} ", Block.Pop());
            }
            Console.WriteLine();
        }
        //Метод создания матрицы инцедентций
        public static Graph CreateGraph(int vertex, int edges)
        {
            Random rand = new Random();
            //Cоздаем матрицу инцедентций с помощью генерации чисел
            List<int[]> someMatrix = new List<int[]>(vertex);
            for(int i = 0; i < vertex; i++)
            {
                someMatrix.Add(new int[edges]);
                for(int j = 0; j < edges; j++)
                {
                    someMatrix[i][j] = rand.Next(0, 2);
                }
            }
            return new Graph(vertex, edges, someMatrix);
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < Matrix.Count; i++)
            {
                for (int j = 0; j < Matrix[i].Length; j++)
                {
                    result += Matrix[i][j] + " ";
                }
                result += "\n";
            }

            return result;
        }

    }
}
