using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4
{
    public class Complex
    {
        static Random rand = new Random();
        //переменные хранящие действительное и мнимое значения комплексного числа
        private double real;
        private double imaginary;

        public double Real { get { return real; } set { real = value; } }
        public double Imaginary { get { return imaginary; } set { imaginary = value; } }

        //Генерация случайного комплексного числа
        public Complex() 
        {
            Real = rand.Next(-100, 101);
            Imaginary = rand.Next(-100, 101);
        }
        public Complex(double real, double mnim)
        {
            Real = real;
            Imaginary = mnim;
        }

        //Перегружаем опрераторы для работы с комплексными числами
        public static Complex operator +(Complex A, Complex B)
        {
            return new Complex(A.Real + B.Real, A.Imaginary + B.Imaginary);
        }
        public static Complex operator -(Complex A, Complex B)
        {
            return new Complex(A.Real - B.Real, A.Imaginary - B.Imaginary);
        }
        public static Complex operator *(Complex A, Complex B)
        {
            Complex AB = new Complex();
            AB.Real = (A.Real * B.Real - A.Imaginary * B.Imaginary);
            AB.Imaginary = (B.Real * A.Imaginary + A.Real * B.Imaginary);
            return AB;
        }
        public static Complex operator /(Complex A, Complex B)
        {
            Complex AB = new Complex();
            AB.Real = (A.Real * B.Real + A.Imaginary * B.Imaginary) / (B.Real * B.Real + B.Imaginary * B.Imaginary);
            AB.Imaginary = (B.Real * A.Imaginary - B.Imaginary * A.Real) / (B.Real * B.Real + B.Imaginary * B.Imaginary);
            return AB;
        }

        //Для удобного вывода перегружаем метод ToString()
        public override string ToString()
        {
            if (Imaginary >= 0)
            {
                return Real + " + " + Imaginary + "i";
            }  
            return Real + "" + Imaginary + "i";
        }
    }
}
