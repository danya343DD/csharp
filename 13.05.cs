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
                Console.WriteLine("-ГЛАВНОЕ МЕНЮ-");
                Console.WriteLine("1 - Задание 1 (Сумма, произведение, min/max элементов)");
                Console.WriteLine("2 - Задание 2 (Четные, нечетные, уникальные элементы)");
                Console.WriteLine("3 - Задание 3 (Поиск последовательности из 3-х чисел)");
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
                else if (choice == "0")
                {
                    Console.WriteLine("Программа завершена.");
                    return;
                }
                else
                {
                    Console.WriteLine("Неверный ввод! Выберите от 0 до 3.");
                }

                Console.WriteLine("\nНажмите Enter для возврата в меню.");
                Console.ReadLine();
            }
        }

        // Задание 1: Сумма, произведение, минимум и максимум
        static void Task1()
        {
            Console.WriteLine("Задание 1");
            int[] array = new int[5];
            Random rand = new Random();

            Console.Write("Массив: ");
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(1, 10); // Заполнение числами от 1 до 9
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();

            int sum = 0;
            long product = 1; 
            int min = array[0];
            int max = array[0];

            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];        
                product *= array[i];    

                if (array[i] < min) min = array[i]; 
                if (array[i] > max) max = array[i]; 
            }

            Console.WriteLine("Сумма элементов: " + sum);
            Console.WriteLine("Произведение элементов: " + product);
            Console.WriteLine("Минимальный элемент: " + min);
            Console.WriteLine("Максимальный элемент: " + max);
        }

        // Задание 2: Четные, нечетные и уникальные элементы
        static void Task2()
        {
            Console.WriteLine("Задание 2");
            int[] numbers = new int[10];
            Random rand = new Random();

            Console.Write("Массив: ");
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = rand.Next(1, 10);
                Console.Write(numbers[i] + " ");
            }
            Console.WriteLine();

            int evenCount = 0;
            int oddCount = 0;
            int uniqueCount = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] % 2 == 0)
                {
                    evenCount++;
                }
                else
                {
                    oddCount++;
                }

                // Проверка на уникальность (встречается ли число в массиве ровно ОДИН раз)
                bool isUnique = true;
                for (int j = 0; j < numbers.Length; j++)
                {
                    if (i != j && numbers[i] == numbers[j])
                    {
                        isUnique = false;
                        break; 
                    }
                }

                if (isUnique)
                {
                    uniqueCount++;
                }
            }

            Console.WriteLine("Количество четных элементов: " + evenCount);
            Console.WriteLine("Количество нечетных элементов: " + oddCount);
            Console.WriteLine("Количество уникальных элементов: " + uniqueCount);
        }

        // Задание 3: Поиск последовательности из 3-х чисел
        static void Task3()
        {
            Console.WriteLine("Задание 3");

            // Задаем тестовый массив из примера в задании
            int[] mainArray = { 7, 6, 5, 3, 4, 7, 6, 5, 8, 7, 6, 5 };

            Console.Write("Исходный массив: ");
            for (int i = 0; i < mainArray.Length; i++)
            {
                Console.Write(mainArray[i] + " ");
            }
            Console.WriteLine();

            Console.WriteLine("\nВведите искомую последовательность из 3 чисел:");
            Console.Write("Число 1: ");
            int num1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Число 2: ");
            int num2 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Число 3: ");
            int num3 = Convert.ToInt32(Console.ReadLine());

            int matchCount = 0;

            for (int i = 0; i <= mainArray.Length - 3; i++)
            {
                if (mainArray[i] == num1 && mainArray[i + 1] == num2 && mainArray[i + 2] == num3)
                {
                    matchCount++;
                }
            }

            Console.WriteLine("\nКоличество повторений последовательности: " + matchCount);
        }
    }
}