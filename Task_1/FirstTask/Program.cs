using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;

namespace FirstTask
{

	class Program
	{
		static void Main(string[] args)
		{
			//Получаем данные о текущем количестве команд и команд для финала в первой строке
			Console.WriteLine("Введите данные об университетах и командах для олимпиады:");
			int teamsCount, finishTeams, limitUniversity;
			string[] row = Console.ReadLine().Split(' ');
			try
			{
				//Количество команд
				teamsCount = Convert.ToInt32(row[0]);
				//Количество допустимых команд в финале
				finishTeams = Convert.ToInt32(row[1]);
				//Количество допустимых команд от одного университета
				limitUniversity = Convert.ToInt32(row[2]);
			}
			catch (FormatException)
			{
				Console.WriteLine("Неверный формат входных данных, перезапустите программу. Нажмите любую клавишу...");
				Console.ReadKey();
				return;
			}
			catch (IndexOutOfRangeException)
			{
				Console.WriteLine("Нужно ввести три числа через пробел, перезапустите программу. Нажмите любую клавишу...");
				Console.ReadKey();
				return;
			}
			//Проверяем входные данные на соответствия ограничениям задачи
			if (finishTeams <= teamsCount && limitUniversity <= teamsCount && teamsCount <= 100000)
			{
				
				//Массив, хранящий данные о командах в первой строе и их номерах во второй строке
				string[,] teams = new string[2, teamsCount];

				for (int i = 0; i < teamsCount; i++)
				{
					//Заполняем команды
					teams[0, i] = Console.ReadLine();
				}
				//Получение последней строки входных данных
				row = Console.ReadLine().Split(' ');
				try
				{
					for (int i = 0; i < teamsCount; i++)
					{
						//Заполняем номера команд
						teams[1, i] = row[i];
					}
				}
				catch (IndexOutOfRangeException)
				{
					Console.WriteLine("Последняя строка должна содержать номера каждой команды через пробел. Перезапустите программу.");
					Console.ReadKey();
					return;
				}

				//Счетчик для проверки количества команд в финале
				int invitations = 0;
				//Словарь для хранения уникальных значений университетов
				SortedDictionary<string, int> universities = new SortedDictionary<string, int>();

				Console.WriteLine("\nКоманды-финалисты:");
				for (int i = 0; i < teamsCount; i++)
				{
					if (invitations >= finishTeams)
					{
						break;
					}
					//Проверяем наличие текущей команды в словаре
					if (!universities.ContainsKey(teams[0, i]))
					{
						universities.Add(teams[0, i], 1);
					}
					else
					{
						/*Увеличиваем число команд от одного университета
						и проверяем, чтобы число не превосходило ограничение*/
						universities[teams[0, i]]++;
						if (universities[teams[0, i]] > limitUniversity)
						{
							continue;
						}
					}
					//Вывод команды в финал и увеличение счетчика
					Console.WriteLine(teams[0, i] + " #" + teams[1, i]);
					invitations++;
				}
				
			}
			else
			{
				Console.WriteLine("При вводе данных были нарушены ограничения, перезапустите программу и попробуйте снова.");
			}

			Console.WriteLine("Для выхода нажмите любую клавишу...");
			Console.ReadKey();
		}
	}

}

