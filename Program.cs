using System;
//1
class Program
{
    static void Main()
    {
        Console.Write("Введите число от 1 до 100: ");

        if (int.TryParse(Console.ReadLine(), out int number))
        {
            if (number < 1 || number > 100)
            {
                Console.WriteLine("Ошибка: введенное число находится вне диапазона от 1 до 100.");
            }
            else
            {
                if (number % 3 == 0 && number % 5 == 0)
                    Console.WriteLine("Fizz Buzz");
                else if (number % 3 == 0)
                    Console.WriteLine("Fizz");
                else if (number % 5 == 0)
                    Console.WriteLine("Buzz");
                else
                    Console.WriteLine(number);
            }
        }
        else
        {
            Console.WriteLine("Ошибка: введено не число.");
        }
    }
}

//2
class Program
{
    static void Main()
    {
        Console.Write("Введите значение: ");
        double value = double.Parse(Console.ReadLine());

        Console.Write("Введите процент, который необходимо посчитать: ");
        double percent = double.Parse(Console.ReadLine());

        double result = (value * percent) / 100;
        Console.WriteLine($"{percent} процентов от {value} равно: {result}");
    }
}

//3
class Program
{
    static void Main()
    {
        Console.WriteLine("Введите четыре цифры (каждую с новой строки):");
        string resultStr = "";

        for (int i = 0; i < 4; i++)
        {
            Console.Write($"Цифра {i + 1}: ");
            resultStr += Console.ReadLine();
        }

        if (int.TryParse(resultStr, out int finalNumber))
        {
            Console.WriteLine($"Сформированное число: {finalNumber}");
        }
        else
        {
            Console.WriteLine("Ошибка ввода.");
        }
    }
}

//4
class Program
{
    static void Main()
    {
        Console.Write("Введите шестизначное число: ");
        string numberStr = Console.ReadLine();

        if (numberStr.Length != 6 || !int.TryParse(numberStr, out _))
        {
            Console.WriteLine("Ошибка: введено не шестизначное число.");
            return;
        }

        Console.Write("Введите номер первого разряда для обмена (1-6): ");
        int pos1 = int.Parse(Console.ReadLine()) - 1; // Индексация массива начинается с 0

        Console.Write("Введите номер второго разряда для обмена (1-6): ");
        int pos2 = int.Parse(Console.ReadLine()) - 1;

        char[] digits = numberStr.ToCharArray();

        // Меняем местами
        char temp = digits[pos1];
        digits[pos1] = digits[pos2];
        digits[pos2] = temp;

        string result = new string(digits);
        Console.WriteLine($"Результат: {result}");
    }
}

//5
class Program
{
    static void Main()
    {
        Console.Write("Введите дату (например, 22.12.2021): ");

        if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
        {
            string dayOfWeek = date.DayOfWeek.ToString();
            string season = "";

            int month = date.Month;
            if (month == 12 || month == 1 || month == 2)
                season = "Winter";
            else if (month >= 3 && month <= 5)
                season = "Spring";
            else if (month >= 6 && month <= 8)
                season = "Summer";
            else
                season = "Autumn";

            Console.WriteLine($"{season} {dayOfWeek}");
        }
        else
        {
            Console.WriteLine("Ошибка: неверный формат даты.");
        }
    }
}

//6
class Program
{
    static void Main()
    {
        Console.Write("Введите показания температуры: ");
        double temperature = double.Parse(Console.ReadLine());

        Console.WriteLine("Выберите направление перевода:");
        Console.WriteLine("1 - Из Фаренгейта в Цельсий");
        Console.WriteLine("2 - Из Цельсия в Фаренгейт");
        Console.Write("Ваш выбор (1 или 2): ");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            double celsius = (temperature - 32) * 5 / 9;
            Console.WriteLine($"Результат: {celsius:F2} °C");
        }
        else if (choice == "2")
        {
            double fahrenheit = (temperature * 9 / 5) + 32;
            Console.WriteLine($"Результат: {fahrenheit:F2} °F");
        }
        else
        {
            Console.WriteLine("Ошибка: неверный выбор.");
        }
    }
}

//7
class Program
{
    static void Main()
    {
        Console.Write("Введите начало диапазона: ");
        int start = int.Parse(Console.ReadLine());

        Console.Write("Введите конец диапазона: ");
        int end = int.Parse(Console.ReadLine());

        // Нормализация границ
        if (start > end)
        {
            int temp = start;
            start = end;
            end = temp;
        }

        Console.WriteLine($"Четные числа в диапазоне от {start} до {end}:");
        for (int i = start; i <= end; i++)
        {
            if (i % 2 == 0)
            {
                Console.Write(i + " ");
            }
        }
        Console.WriteLine();
    }
}