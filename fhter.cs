using System;
using System.Linq;
//1
namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Задание 1: Анализ элементов массива";

            
            int[] numbers = new int[12];
            Random rand = new Random();
            Console.Write("Исходный массив: ");
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
                // 1. Проверка на чётность/нечётность
                if (numbers[i] % 2 == 0)
                    evenCount++;
                else
                    oddCount++;

                // 2. Проверка на уникальность (встречается ли элемент где-то еще)
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

            Console.WriteLine($"\nКоличество чётных элементов: {evenCount}");
            Console.WriteLine($"Количество нечётных элементов: {oddCount}");
            Console.WriteLine($"Количество уникальных элементов: {uniqueCount}");

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
            Console.Title = "Задание 2: Элементы меньше заданного";

            int[] array = new int[10];
            Random rand = new Random();
            Console.Write("Исходный массив: ");
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(1, 15); 
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();

            Console.Write("\nВведите пороговое значение: ");
            int threshold = Convert.ToInt32(Console.ReadLine());

            int countLess = 0;
            foreach (int item in array)
            {
                if (item < threshold)
                {
                    countLess++;
                }
            }

            Console.WriteLine($"Количество значений в массиве меньших, чем {threshold}: {countLess}");

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
            Console.Title = "Задание 3: Поиск последовательности";

            
            int[] mainArray = { 7, 6, 5, 3, 4, 7, 6, 5, 8, 7, 6, 5 };

            Console.Write("Массив: ");
            foreach (int num in mainArray) Console.Write(num + " ");
            Console.WriteLine();

            
            Console.WriteLine("\nВведите последовательность из трех чисел через Enter:");
            int[] searchPattern = new int[3];
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"Число {i + 1}: ");
                searchPattern[i] = Convert.ToInt32(Console.ReadLine());
            }

            int matchCount = 0;

            // Длина перебора уменьшена на (длина шаблона - 1), чтобы не выйти за границы массива
            for (int i = 0; i <= mainArray.Length - searchPattern.Length; i++)
            {
                
                if (mainArray[i] == searchPattern[0] &&
                    mainArray[i + 1] == searchPattern[1] &&
                    mainArray[i + 2] == searchPattern[2])
                {
                    matchCount++;
                }
            }

            Console.WriteLine($"\nКоличество повторений последовательности: {matchCount}");

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
            Console.Title = "Задание 4: Общие элементы двух массивов";

            // Инициализация двух массивов разной размерности (M и N)
            int[] array1 = { 5, 2, 7, 2, 9, 3, 5 }; 
            int[] array2 = { 7, 3, 1, 5, 7, 8 };    

            Console.Write("Первый массив: ");
            foreach (int num in array1) Console.Write(num + " ");
            Console.WriteLine();

            Console.Write("Второй массив: ");
            foreach (int num in array2) Console.Write(num + " ");
            Console.WriteLine();

            // Временный массив для хранения общих элементов (максимум равен длине наименьшего массива)
            int maxPossibleSize = Math.Min(array1.Length, array2.Length);
            int[] tempCommon = new int[maxPossibleSize];
            int commonCount = 0;

            
            for (int i = 0; i < array1.Length; i++)
            {
                for (int j = 0; j < array2.Length; j++)
                {
                    
                    if (array1[i] == array2[j])
                    {
                        // Проверяем, не записали ли мы его уже во временный массив ранее
                        bool alreadyExists = false;
                        for (int k = 0; k < commonCount; k++)
                        {
                            if (tempCommon[k] == array1[i])
                            {
                                alreadyExists = true;
                                break;
                            }
                        }

                        
                        if (!alreadyExists)
                        {
                            tempCommon[commonCount] = array1[i];
                            commonCount++;
                        }
                    }
                }
            }

            // Переносим результат в финальный третий массив точного размера
            int[] resultTemplate = new int[commonCount];
            for (int i = 0; i < commonCount; i++)
            {
                resultTemplate[i] = tempCommon[i];
            }

            
            Console.Write("\nТретий массив (общие элементы без повторений): ");
            foreach (int num in resultTemplate)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}

//5
namespace Task5
{
    class Program
    {
        static void Main()
        {
            int[,] matrix = new int[3, 4];
            Random rand = new Random();
            int min = 100, max = 0;

            Console.WriteLine("Массив:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = rand.Next(10, 100);
                    Console.Write(matrix[i, j] + " ");

                    if (matrix[i, j] < min) min = matrix[i, j];
                    if (matrix[i, j] > max) max = matrix[i, j];
                }
                Console.WriteLine();
            }
            Console.WriteLine($"\nМинимум: {min}\nМаксимум: {max}");
        }
    }
}

//6
namespace Task6
{
    class Program
    {
        static void Main()
        {
            Console.Write("Введите предложение: ");
            string input = Console.ReadLine();

            string[] words = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine($"Количество слов: {words.Length}");
        }
    }
}

//7
namespace Task7
{
    class Program
    {
        static void Main()
        {
            Console.Write("Введите предложение: ");
            string[] words = Console.ReadLine().Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                char[] chars = words[i].ToCharArray();
                Array.Reverse(chars);
                words[i] = new string(chars);
            }

            Console.WriteLine("Результат: " + string.Join(" ", words));
        }
    }
}

//8
namespace Task8
{
    class Program
    {
        static void Main()
        {
            Console.Write("Введите предложение: ");
            string input = Console.ReadLine();
            string vowels = "аеёиоуыэюяАЕЁИОУЫЭЮЯaeiouyAEIOUY";
            int count = 0;

            foreach (char c in input)
            {
                if (vowels.Contains(c)) count++;
            }

            Console.WriteLine($"Количество гласных букв: {count}");
        }
    }
}