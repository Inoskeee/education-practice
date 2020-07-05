using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineTask
{
    //Элемент двунаправленного списка
    public class DuplexPoint<T>
    {
        //Информационное поле
        public T Data { get; set; }
        //Следующий элемент
        public DuplexPoint<T> Next { get; set; }
        //Предыдущий элемент
        public DuplexPoint<T> Previous { get; set; }

        public DuplexPoint()
        {
            Next = null;
            Previous = null;
            Data = default(T);
        }
        public DuplexPoint(T data)
        {
            Data = data;
            Next = null;
            Previous = null;
        }

        public override string ToString()
        {
            return Data.ToString() + " ";
        }

    }
    //Двунаправленный список
    public class LinkedList<T> : IEnumerable<T>
    {
        //Поля хранящие первый и последний элемент списка и общее количество элементов
        public DuplexPoint<T> Head { get; private set; }
        public DuplexPoint<T> Tail { get; private set; }
        public int Count { get; private set; }

        public LinkedList()
        {
            Clear();
        }
        public LinkedList(T data)
        {
            DuplexPoint<T> item = new DuplexPoint<T>(data);
            SetHeadAndTail(item);

        }
        //Метод добавления элемента в список
        public void Add(T data)
        {
            DuplexPoint<T> item = new DuplexPoint<T>(data);
            if (Count == 0) SetHeadAndTail(item);
            else
            {
                Tail.Next = item;
                item.Previous = Tail;
                Tail = item;
                Count++;
            }
        }
        //Метод удаления элемента из списка
        public void Delete(T data)
        {
            DuplexPoint<T> current = Head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    if (Head.Previous != null)
                    {
                        current.Previous.Next = current.Next;
                        current.Next.Previous = current.Previous;
                        Count--;
                        return;
                    }
                    else
                    {
                        Head = current.Next;
                        Head.Previous = null;
                        Count--;
                        return;
                    }
                }

                current = current.Next;
            }

            Console.WriteLine("Элемент не найден!");
        }
        //Метод очистки списка
        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        private void SetHeadAndTail(DuplexPoint<T> item)
        {
            Head = item;
            Tail = item;
            Count = 1;
        }
        //Нумератор для перебора списка через foreach
        public IEnumerator GetEnumerator()
        {
            var current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>)GetEnumerator();
        }
    }
}
