using System;

namespace nagaev
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("- ГЛАВНОЕ МЕНЮ ДОМАШНЕГО ЗАДАНИЯ -");
                Console.WriteLine("1 - Задание 1 (FizzBuzz от 1 до 100)");
                Console.WriteLine("2 - Задание 2 (Расчет процента от числа)");
                Console.WriteLine("3 - Задание 3 (Число из 4-х цифр)");
                Console.WriteLine("4 - Задание 4 (Обмен разрядов в 6-значном числе)");
                Console.WriteLine("5 - Задание 5 (Сезон и день недели по дате)");
                Console.WriteLine("6 - Задание 6 (Конвертер температур)");
                Console.WriteLine("0 - Выход из программы");
                Console.WriteLine("___");
                Console.Write("Выберите номер задания: ");

                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        Task1();
                        break;
                    case "2":
                        Task2();
                        break;
                    case "3":
                        Task3();
                        break;
                    case "4":
                        Task4();
                        break;
                    case "5":
                        Task5();
                        break;
                    case "6":
                        Task6();
                        break;
                    case "0":
                        Console.WriteLine("Программа завершена. До свидания!");
                        return;
                    default:
                        Console.WriteLine("Неверный ввод! Пожалуйста, выберите пункт от 0 до 6.");
                        break;
                }

                Console.WriteLine("\nНажмите Enter, чтобы вернуться в меню.");
                Console.ReadLine();
            }
        }

        // Задание 1: FizzBuzz с валидацией диапазона
        static void Task1()
        {
            Console.WriteLine("Задание 1 (FizzBuzz)");
            Console.Write("Введите число от 1 до 100: ");
            int num = Convert.ToInt32(Console.ReadLine());

            if (num < 1 || num > 100)
                Console.WriteLine("Ошибка: число вне диапазона от 1 до 100.");
            else if (num % 3 == 0 && num % 5 == 0)
                Console.WriteLine("Fizz Buzz");
            else if (num % 3 == 0)
                Console.WriteLine("Fizz");
            else if (num % 5 == 0)
                Console.WriteLine("Buzz");
            else
                Console.WriteLine(num);
        }

        // Задание 2: Расчет процента от числа
        static void Task2()
        {
            Console.WriteLine("Задание 2 (Расчет процента)");
            Console.Write("Введите число: ");
            double value = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите процент: ");
            double percent = Convert.ToDouble(Console.ReadLine());

            double result = value * (percent / 100.0);
            Console.WriteLine($"Результат: {result}");
        }

        // Задание 3: Формирование числа из четырех цифр
        static void Task3()
        {
            Console.WriteLine("Задание 3 (Число из 4-х цифр)");
            int result = 0;
            for (int i = 0; i < 4; i++)
            {
                Console.Write($"Введите цифру {i + 1}: ");
                int digit = Convert.ToInt32(Console.ReadLine());
                result = result * 10 + digit;
            }
            Console.WriteLine($"Сформированное число: {result}");
        }

        // Задание 4: Обмен разрядов в шестизначном числе
        static void Task4()
        {
            Console.WriteLine("Задание 4 (Обмен разрядов)");
            Console.Write("Введите шестизначное число: ");
            string input = Console.ReadLine(); if (input.Length != 6 || !int.TryParse(input, out _))
            {
                Console.WriteLine("Ошибка: введено не шестизначное число.");
                return;
            }

            char[] digits = input.ToCharArray();

            Console.Write("Введите первый номер разряда для обмена (1-6): ");
            int p1 = Convert.ToInt32(Console.ReadLine()) - 1;
            Console.Write("Введите второй номер разряда для обмена (1-6): ");
            int p2 = Convert.ToInt32(Console.ReadLine()) - 1;

            char temp = digits[p1];
            digits[p1] = digits[p2];
            digits[p2] = temp;

            Console.WriteLine("Результат: " + new string(digits));
        }

        // Задание 5: Определение сезона и дня недели по дате
        static void Task5()
        {
            Console.WriteLine("Задание 5 (Сезон и день недели)");
            Console.Write("Введите дату (например, 22.12.2021): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            string season = date.Month switch
            {
                12 or 1 or 2 => "Winter",
                3 or 4 or 5 => "Spring",
                6 or 7 or 8 => "Summer",
                _ => "Autumn"
            };

            Console.WriteLine($"{season} {date.DayOfWeek}");
        }

        // Задание 6: Конвертер температуры (Цельсий <-> Фаренгейт)
        static void Task6()
        {
            Console.WriteLine("Задание 6 (Конвертер температуры)");
            Console.Write("Введите показания температуры: ");
            double temp = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Выберите тип конвертации:\n1 - Из Цельсия в Фаренгейт\n2 - Из Фаренгейта в Цельсий");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
                Console.WriteLine($"Результат: {temp * 9 / 5 + 32} °F");
            else if (choice == 2)
                Console.WriteLine($"Результат: {(temp - 32) * 5 / 9} °C");
            else
                Console.WriteLine("Неверный выбор.");
        }
    }
}