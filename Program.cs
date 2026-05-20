using System;
//1
namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Задача 1: Размещение квадратов";

            Console.Write("Введите сторону прямоугольника A: ");
            int a = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите сторону прямоугольника B: ");
            int b = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите сторону квадрата C: ");
            int c = Convert.ToInt32(Console.ReadLine());

            // Проверка: можно ли разместить хотя бы один квадрат
            if (c > a || c > b)
            {
                Console.WriteLine("Служебное сообщение: Разместить ни одного квадрата нельзя, так как сторона C больше сторон прямоугольника.");
            }
            else
            {
                
                int countByA = a / c;
                int countByB = b / c;

                
                int totalSquares = countByA * countByB;

                
                long areaRectangle = (long)a * b;
                long areaOccupied = (long)totalSquares * (c * c);
                long areaFree = areaRectangle - areaOccupied;

                Console.WriteLine($"\nКоличество размещенных квадратов: {totalSquares} шт.");
                Console.WriteLine($"Площадь незанятой части прямоугольника: {areaFree}");
            }

            Console.ReadLine();
        }
    }
}

//2
namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Задача 2: Расчет банковского вклада";

            Console.Write("Введите ежемесячный процент P (0 < P < 25): ");
            double p = Convert.ToDouble(Console.ReadLine());

            // Проверяем корректность ввода согласно условию
            if (p <= 0 || p >= 25)
            {
                Console.WriteLine("Ошибка: процент должен быть больше 0 и меньше 25.");
            }
            else
            {
                double sum = 10000.0; 
                int months = 0;       

                
                while (sum <= 11000.0)
                {
                    sum += sum * (p / 100.0);
                    months++;
                }

                // Округляем итоговую сумму для красивого вывода
                sum = Math.Round(sum, 2);

                Console.WriteLine($"\nКоличество месяцев K = {months}");
                Console.WriteLine($"Итоговый размер вклада S = {sum} руб.");
            }

            Console.ReadLine();
        }
    }
}

//3
namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Задача 3: Вывод чисел";

            Console.Write("Введите целое положительное число A: ");
            int a = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите целое положительное число B (должно быть > A): ");
            int b = Convert.ToInt32(Console.ReadLine());

            if (a >= b)
            {
                Console.WriteLine("Ошибка: должно выполняться условие A < B.");
            }
            else
            {
                Console.WriteLine("\nРезультат вывода:");

                // Внешний цикл перебирает числа от A до B включительно
                for (int i = a; i <= b; i++)
                {
                    // Внутренний цикл выводит текущее число 'i' ровно 'i' раз
                    for (int j = 0; j < i; j++)
                    {
                        Console.Write(i + " ");
                    }
                    Console.WriteLine(); 
                }
            }

            Console.ReadLine();
        }
    }
}

//4
namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Задача 4: Переворот числа";

            Console.Write("Введите целое положительное число N: ");
            int n = Convert.ToInt32(Console.ReadLine());

            if (n <= 0)
            {
                Console.WriteLine("Ошибка: число должно быть больше 0.");
            }
            else
            {
                int originalNumber = n;
                int reversedNumber = 0;

                // Математический алгоритм переворота числа
                while (n > 0)
                {
                    int remainder = n % 10;                     
                    reversedNumber = (reversedNumber * 10) + remainder; 
                    n /= 10;                                    // Отсекаем последнюю цифру
                }

                Console.WriteLine($"\nИсходное число: {originalNumber}");
                Console.WriteLine($"Число справа налево: {reversedNumber}");
            }

            Console.ReadLine();
        }
    }
}