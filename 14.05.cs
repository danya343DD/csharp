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
                Console.WriteLine("-ГЛАВНОЕ МЕНЮ ДОМАШНЕГО ЗАДАНИЯ-");
                Console.WriteLine("1 - Задание 1 (Сумма, произведение, min/max элементов)");
                Console.WriteLine("2 - Задание 2 (Переворот строк / Смена знака элементов)");
                Console.WriteLine("3 - Задание 3 (Сортировка строк матрицы по возрастанию)");
                Console.WriteLine("4 - Задание 4 (Сложение и умножение двух матриц)");
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

                Console.WriteLine("\nНажмите Enter, чтобы вернуться в меню...");
                Console.ReadLine();
            }
        }

        // Задание 1: Подсчет суммы, произведения, поиск min и max в одномерном массиве
        static void Task1()
        {
            Console.WriteLine("Задание 1 (Анализ одномерного массива)");
            int[] array = new int[5];
            Random rand = new Random();

            Console.Write("Сгенерированный массив: ");
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(-10, 11);
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

        // Задание 2: Изменение знака элементов одномерного массива на противоположный
        static void Task2()
        {
            Console.WriteLine("Задание 2 (Смена знака элементов)");
            int[] array = new int[6];
            Random rand = new Random();

            Console.Write("Исходный массив:  ");
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(-20, 21);
                Console.Write(array[i] + "\t");
            }
            Console.WriteLine();

            // Инвертируем знак каждого элемента умножением на -1
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = array[i] * -1;
            }

            Console.Write("Измененный массив: ");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + "\t");
            }
            Console.WriteLine();
        }

        // Задание 3: Сортировка каждой строки двумерного массива по возрастанию
        static void Task3()
        {
            Console.WriteLine("Задание 3 (Сортировка строк матрицы)");
            int[,] matrix = new int[3, 4];
            Random rand = new Random();

            Console.WriteLine("Исходная матрица:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = rand.Next(10, 100);
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }

            // Метод пузырьковой сортировки для каждой строки в отдельности
            for (int i = 0; i < matrix.GetLength(0); i++) 
            {
                for (int step = 0; step < matrix.GetLength(1) - 1; step++) 
                {
                    for (int j = 0; j < matrix.GetLength(1) - 1 - step; j++)
                    {
                        if (matrix[i, j] > matrix[i, j + 1])
                        {
                            // Меняем элементы местами внутри строки
                            int temp = matrix[i, j];
                            matrix[i, j] = matrix[i, j + 1];
                            matrix[i, j + 1] = temp;
                        }
                    }
                }
            }

            Console.WriteLine("\nМатрица после сортировки строк по возрастанию:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        // Задание 4: Арифметика матриц (Сложение и умножение двух матриц)
        static void Task4()
        {
            Console.WriteLine("Задание 4 (Сложение и умножение матриц 2х2)");

            // Зададим две фиксированные квадратные матрицы размера 2х2 для наглядности расчетов
            int[,] matrixA = { { 1, 2 }, { 3, 4 } };
            int[,] matrixB = { { 5, 6 }, { 7, 8 } };

            Console.WriteLine("Матрица A:");
            PrintMatrix(matrixA);
            Console.WriteLine("Матрица B:");
            PrintMatrix(matrixB);

            int rows = matrixA.GetLength(0);
            int cols = matrixA.GetLength(1);

            int[,] sumMatrix = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sumMatrix[i, j] = matrixA[i, j] + matrixB[i, j];
                }
            }
            Console.WriteLine("Результат сложения (A + B):");
            PrintMatrix(sumMatrix);

            // 2. Умножение матриц (по правилу «строка на столбец»)
            int[,] productMatrix = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    productMatrix[i, j] = 0;
                    for (int k = 0; k < cols; k++)
                    {
                        productMatrix[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            }
            Console.WriteLine("Результат умножения (A * B):");
            PrintMatrix(productMatrix);
        }

        static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}