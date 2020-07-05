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
        //Матрица смежности
        public int[,] Matrix;
        //Массив вершин графа
        public int[] Values;
        //Размер графа
        public int Size;
        //Переменная, хранящая индекс вершины для стягивания
        public int vertexWithNumber;

        public Graph(int size, int[] val, int[,] matrix)
        {
            Size = size;
            Matrix = matrix;
            Values = val;
            vertexWithNumber = 0;
        }

        //Метод загрузки графа из файла
        public static Graph CreateGraph(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    //Считываем первую строку в массив вершин
                    string line = sr.ReadLine().Trim(' ');

                    int[] values = line.Split(' ').Select(item => int.Parse(item)).ToArray();

                    int[,] someMatrix = new int[values.Length, values.Length];

                    int count = 0;
                    //Считываем матрицу смежности без учета первого элемента, т.к он вершина
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

                    return new Graph(values.Length, values, someMatrix);

                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Выбранный файл не найден, повторите ввод.");
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        //Метод сохранения графа в файл
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //Метод проверки наличия вершин для стягивания
        public bool isVertex(int value)
        {
            //Проверяем, если такая вершина
            if (!Values.Contains(value))
            {
                Console.WriteLine("В графе нет вершины с указанным значением.");
                return false;
            }

            int vertexWithNumber = Array.IndexOf(Values, value);

            // Проходим по оставшимся вершинам графа
            for (int i = vertexWithNumber + 1; i < Size; i++)
            {
                if (Values[i] == value)
                {
                    return true;
                }
            }
            return false;
        }
        //Метод стягивания вершины
        public void TightVertexes(int value)
        {
            //Совершаем перебор графа
            for (int i = vertexWithNumber + 1; i < Size; i++)
            {
                //Если нашли значение для стягивания
                if (Values[i] == value)
                {
                    //Ищем другое такое значение в колонке
                    for (int col = 0; col < Size; col++)
                    {
                        //Как только нашли, переписываем в первую нужную вершину
                        if (Matrix[i, col] == 1 && col != vertexWithNumber)
                        {
                            Matrix[vertexWithNumber, col] = 1;
                        }
                            
                    }

                    //Ищем другое такое значение в строке
                    for (int row = 0; row < Size; row++)
                    {
                        //Как только нашли, переписываем в первую нужную вершину
                        if (Matrix[row, i] == 1 && row != vertexWithNumber)
                        {
                            Matrix[row, vertexWithNumber] = 1;
                        }
                            
                    }
                    //Удаляем вершину
                    RemoveVertex(i);
                }
            }
        }
        //Метод удаления вершины
        public void RemoveVertex(int index)
        {
            //Проходим по всем вершинам графа и переписываем из в новый массив без учета удаляемой
            int[] NewValues = new int[Size - 1];
            for(int i = 0; i < index; i++)
            {
                NewValues[i] = Values[i];
            }
            for (int i = index; i < NewValues.Length; i++)
            {
                NewValues[i] = Values[i];
            }

            Values = NewValues;

            //Проходим по матрице смежности и переписываем ее в новый массив без учета удаляемой вершины
            int[,] NewMatrix = new int[Size - 1, Size - 1];

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
            //Удаление строки
            for (int i = index; i < NewMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < NewMatrix.GetLength(1); j++)
                {
                    NewMatrix[i, j] = Matrix[i + 1, j];
                }
                   
            }
                
            Matrix = NewMatrix;

            Size--;

        }

        public override string ToString()
        {

            string result = "[";
            for (int i = 0; i < Values.Length; i++)
            {
                result += Values[i] + " ";
            }
            result += "]\n";
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    result += Matrix[i, j] + " ";
                }
                result += "\n";
            }

            return result;
        }

    }
}
