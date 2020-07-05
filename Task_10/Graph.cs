using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTen
{
    

    public class Graph
    {
        public int[,] Matrix;
        public int[] Values;

        public Graph(int[] val, int[,] matrix)
        {
            Matrix = matrix;
            Values = val;
        }


        public static Graph CreateGraph(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {

                    string line = sr.ReadLine().Trim(' ');

                    int[] values = line.Split(' ').Select(item => int.Parse(item)).ToArray();

                    int[,] someMatrix = new int[values.Length, values.Length];

                    int count = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        line = line.Substring(1).Trim(' ');
                        int[] array = line.Split(' ').Select(item => int.Parse(item)).ToArray();
                        for (int i = 0; i < array.Length; i++)
                        {
                            if (array[i] != 0 && array[i] != 1)
                            {
                                throw new Exception("Неверно задана матрица смежности");
                            }
                            someMatrix[count, i] = array[i];
                        }
                        count++;
                    }

                    return new Graph(values, someMatrix);

                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Указанный файл не найден, повторите ввод");
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public void SaveGraph(string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
                {
                    string text = "  ";
                    for(int i = 0; i< Values.Length; i++)
                    {
                        text += Values[i] + " ";
                    }
                    sw.WriteLine(text);
                    for(int i = 0; i<Matrix.GetLength(0); i++)
                    {
                        text = Values[i] + " ";
                        for(int j = 0; j < Matrix.GetLength(0); j++)
                        {
                            text += Matrix[i, j] + " ";
                        }
                        sw.WriteLine(text);
                    }
                }
                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    result += Matrix[i,j] + " ";
                }
                result += "\n";
            }

            return result;
        }


        public bool isVertex(int value)
        {
            if (!Values.Contains(value))
            {
                Console.WriteLine("В графе нет вершины с указанным значением.");
                return false;
            }
            int currentVertex = Array.IndexOf(Values, value);

            // Проходим по оставшимся вершинам графа
            for (int i = currentVertex + 1; i < Values.GetLength(0); i++)
            {
                if (Values[i] == value)
                {
                    return true;
                }
            }
            return false;
        }

        public void TightVertexes(int value)
        {
            int currentVertex = Array.IndexOf(Values, value);

            for (int i = currentVertex + 1; i < Values.Length; i++)
            {
                if (Values[i] == value)
                {
                    for (int col = 0; col < Values.GetLength(0); col++)
                    {
                        if (Matrix[i, col] == 1 && col != currentVertex)
                        {
                            Matrix[currentVertex, col] = 1;
                        }
                    }   

                    for (int row = 0; row < Values.GetLength(0); row++)
                    {
                        if (Matrix[row, i] == 1 && row != currentVertex)
                        {
                            Matrix[row, currentVertex] = 1;
                        }
                    }
                        
                    RemoveVertex(i);
                }
            }
        }

        public void RemoveVertex(int index)
        {
            int[] NewValues = new int[Values.Length - 1];
            for(int i = 0; i < index; i++)
            {
                NewValues[i] = Values[i];
            }
            for (int i = index; i < NewValues.Length; i++)
            {
                NewValues[i] = Values[i];
            }

            Values = NewValues;


            int[,] NewMatrix = new int[Values.Length - 1, Values.Length - 1];


            for (int i = 0; i < index; i++)
            {
                for (int j = 0; j < index; j++)
                {
                    NewMatrix[i, j] = Matrix[i, j];
                }
                    
            }    
            // Удаление столбца
            for (int i = 0; i < NewMatrix.GetLength(0); i++)
            {
                for (int j = index; j < NewMatrix.GetLength(1); j++)
                {
                    NewMatrix[i, j] = Matrix[i, j + 1];
                }
                    
            }   
            // Удаление строки
            for (int i = index; i < NewMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < NewMatrix.GetLength(1); j++)
                {
                    NewMatrix[i, j] = Matrix[i + 1, j];
                }
                   
            }    
            Matrix = NewMatrix;

        }

    }
}
