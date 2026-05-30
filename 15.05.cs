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
                Console.WriteLine("ГЛАВНОЕ МЕНЮ ДОМАШНЕГО ЗАДАНИЯ");
                Console.WriteLine("1 - Задание 1 (Метод рисования квадрата из символов)");
                Console.WriteLine("2 - Задание 2 (Метод проверки числа на палиндром)");
                Console.WriteLine("3 - Задание 3 (Метод фильтрации оригинального массива)");
                Console.WriteLine("4 - Задание 4 (Работа с классом \"Веб-сайт\")");
                Console.WriteLine("0 - Выход");
                Console.WriteLine("___");
                Console.Write("Выберите номер задания: ");

                string choice = Console.ReadLine();
                Console.Clear();

                if (choice == "1")
                {
                    Task1();
                }
                else if (choice == "2")
                {
                    Task2();
                }
                else if (choice == "3")
                {
                    Task3();
                }
                else if (choice == "4")
                {
                    Task4();
                }
                else if (choice == "0")
                {
                    Console.WriteLine("Программа завершена.");
                    return;
                }
                else
                {
                    Console.WriteLine("Неверный ввод! Пожалуйста, выберите пункт от 0 до 4.");
                }

                Console.WriteLine("\nНажмите Enter, чтобы вернуться в меню.");
                Console.ReadLine();
            }
        }

        // ЗАДАНИЕ 1: Метод, отображающий квадрат из заданного символа
        static void DrawSquare(int size, char symbol)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(symbol + " ");
                }
                Console.WriteLine();
            }
        }

        static void Task1()
        {
            Console.WriteLine("Задание 1 (Квадрат из символов)");
            Console.Write("Введите длину стороны квадрата: ");
            int size = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите символ для рисования: ");
            char symbol = Convert.ToChar(Console.ReadLine());

            Console.WriteLine("\nРезультат работы метода:");
            DrawSquare(size, symbol);
        }

        // ЗАДАНИЕ 2: Метод проверки числа на палиндром
        static bool IsPalindrome(int number)
        {
            int original = number;
            int reversed = 0;
            int temp = number;

            //задом наперед
            while (temp > 0)
            {
                int remainder = temp % 10;
                reversed = (reversed * 10) + remainder;
                temp /= 10;
            }

            //палиндром
            if (original == reversed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void Task2()
        {
            Console.WriteLine("Задание 2 (Число-палиндром)");
            Console.Write("Введите целое положительное число: ");
            int num = Convert.ToInt32(Console.ReadLine());

            bool result = IsPalindrome(num);
            Console.WriteLine("Результат метода: " + result);

            if (result == true)
            {
                Console.WriteLine($"Число {num} является палиндромом.");
            }
            else
            {
                Console.WriteLine($"Число {num} НЕ является палиндромом.");
            }
        }

        // ЗАДАНИЕ 3: Метод фильтрации оригинального массива
        static int[] FilterArray(int[] originalArray, int[] filterArray)
        {
            int targetSize = 0;
            for (int i = 0; i < originalArray.Length; i++)
            {
                bool foundInFilter = false;
                for (int j = 0; j < filterArray.Length; j++)
                {
                    if (originalArray[i] == filterArray[j])
                    {
                        foundInFilter = true;
                        break;
                    }
                }

                //увеличиваем счетчик размера
                if (foundInFilter == false)
                {
                    targetSize++;
                }
            }

            //результирующий массив точного размера
            int[] result = new int[targetSize];
            int index = 0;

            for (int i = 0; i < originalArray.Length; i++)
            {
                bool foundInFilter = false;
                for (int j = 0; j < filterArray.Length; j++)
                {
                    if (originalArray[i] == filterArray[j])
                    {
                        foundInFilter = true;
                        break;
                    }
                }

                if (foundInFilter == false)
                {
                    result[index] = originalArray[i];
                    index++;
                }
            }

            return result;
        }

        static void Task3()
        {
            Console.WriteLine("Задание 3 (Фильтрация массива)");

            //массивы на основе примера из задания
            int[] original = { 1, 2, 6, -1, 88, 7, 6 };
            int[] filter = { 6, 88, 7 };

            Console.Write("Оригинальный массив:     ");
            for (int i = 0; i < original.Length; i++) Console.Write(original[i] + " ");
            Console.WriteLine();

            Console.Write("Массив для фильтрации:   ");
            for (int i = 0; i < filter.Length; i++) Console.Write(filter[i] + " ");
            Console.WriteLine();

            int[] filteredResult = FilterArray(original, filter);

            Console.Write("Результат работы метода: ");
            for (int i = 0; i < filteredResult.Length; i++) Console.Write(filteredResult[i] + " ");
            Console.WriteLine();
        }

        //ЗАДАНИЕ 4: Класс «Веб-сайт» и методы работы с ним
        static void Task4()
        {
            Console.WriteLine("--- Задание 4 (Класс \"Веб-сайт\") ---");

            Website site = new Website();

            site.InputData();

            Console.WriteLine("\nОтображение данных сайта:");
            site.PrintData();

            Console.WriteLine("Изменим IP-адрес сайта вручную через метод класса...");
            site.SetIpAddress("192.168.1.1");
            Console.WriteLine("Новый IP-адрес, полученный через метод Get: " + site.GetIpAddress());
        }
    }

    //описание класса «Веб-сайт» для Задания 4
    class Website
    {
        //(инкапсуляция)
        private string siteName;
        private string sitePath;
        private string description;
        private string ipAddress;

        //метод для ручного ввода данных с клавиатуры
        public void InputData()
        {
            Console.Write("Введите название сайта: ");
            siteName = Console.ReadLine();

            Console.Write("Введите путь к сайту (URL): ");
            sitePath = Console.ReadLine();

            Console.Write("Введите описание сайта: ");
            description = Console.ReadLine();

            Console.Write("Введите IP-адрес сайта: ");
            ipAddress = Console.ReadLine();
        }

        public void PrintData()
        {
            Console.WriteLine("Название сайта: " + siteName);
            Console.WriteLine("Путь к сайту: " + sitePath);
            Console.WriteLine("Описание сайта: " + description);
            Console.WriteLine("IP-адрес сайта: " + ipAddress);
        }

        public string GetSiteName() { return siteName; }
        public void SetSiteName(string value) { siteName = value; }

        public string GetSitePath() { return sitePath; }
        public void SetSitePath(string value) { sitePath = value; }

        public string GetDescription() { return description; }
        public void SetDescription(string value) { description = value; }

        public string GetIpAddress() { return ipAddress; }
        public void SetIpAddress(string value) { ipAddress = value; }
    }
}