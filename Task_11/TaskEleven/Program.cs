using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskEleven
{
    class Program
    {
        //Проверка, на что нужно менять значение
        static char Checker(char element, char fisrtValue, char secondValue)
        {
            if (element == '1' || element == '0')
            {
                return fisrtValue;
            }
            else if (element == '.' || element == '-')
            {
                return secondValue;
            }

            return ' ';
        }
        //Метод шифрования последовательности
        static void Coding(string sequence, out string encryption)
        {
            encryption = sequence[0].ToString();
            for (int i = 1; i < sequence.Length; i++)
            {
                if (sequence[i] == sequence[i - 1])
                {
                    encryption += Checker(sequence[i], '1', '.');
                }
                else
                {
                    encryption += Checker(sequence[i], '0', '-');
                }
            }
        }
        //Метод расшифровки последовательности
        static void Decoding(string encryption, out string decryption)
        {
            decryption = encryption[0].ToString();
            for (int i = 1; i < encryption.Length; i++)
            {
                if (encryption[i] == decryption[i - 1])
                {
                    decryption += Checker(encryption[i], '1', '.');
                }
                else
                {
                    decryption += Checker(encryption[i], '0', '-');
                }
            }
        }
        //Метод для проверки правильности ввода
        static bool Input(out string value)
        {
            Console.WriteLine("Введите последовательность из 0 и 1 или из . и - для шифрования");
            value = Console.ReadLine();
            if(value == "")
            {
                Console.WriteLine("Проверьте правильность введенной последовательности и попробуйте снова");
                return true;
            }
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] != '1' && value[i] != '0' && value[i] != '-' && value[i] != '.')
                {
                    Console.WriteLine("Проверьте правильность введенной последовательности и попробуйте снова");
                    return true;
                }
            }
            return false;
        }

        static void Main(string[] args)
        {
            //Значение для шифрования, расшифровки
            string value, encryption, decription;

            //Проверка коррекнтости
            while(Input(out value)) { }

            Console.WriteLine("Шифрование....");
            Coding(value, out encryption);

            Console.WriteLine(encryption);

            Console.WriteLine("Расшифровка....");
            Decoding(encryption, out decription);

            Console.WriteLine(decription);

            Console.WriteLine("Для выхода нажмите любую клавишу...");
            Console.ReadKey();

        }
    }
}
