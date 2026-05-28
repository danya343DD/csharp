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
                Console.WriteLine("-ГЛАВНОЕ МЕНЮ ПРАКТИЧЕСКИХ ЗАДАНИЙ-");
                Console.WriteLine("1 - Задание 1 (Заполнение матрицы по спирали)");
                Console.WriteLine("2 - Задание 2 (Умножение матрицы на число)");
                Console.WriteLine("3 - Задание 3 (Сложение двух матриц)");
                Console.WriteLine("4 - Задание 4 (Произведение двух матриц)");
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

        // Задание 1: Заполнение квадратной матрицы по спирали
        static void Task1()
        {
            Console.WriteLine("Задание 1 (Спиральная матрица)");
            Console.Write("Введите размер квадратной матрицы (например, 4): ");
            int n = Convert.ToInt32(Console.ReadLine());

            int[,] matrix = new int[n, n];

            int value = 1;       
            int top = 0;         
            int bottom = n - 1;  
            int left = 0;        
            int right = n - 1;   

            // Пошаговый классический алгоритм движения по спирали
            while (value <= n * n)
            {
                for (int i = left; i <= right; i++)
                {
                    matrix[top, i] = value++;
                }
                top++; // Сдвигаем верхнюю границу вниз

                for (int i = top; i <= bottom; i++)
                {
                    matrix[i, right] = value++;
                }
                right--; 

                if (top <= bottom)
                {
                    for (int i = right; i >= left; i--)
                    {
                        matrix[bottom, i] = value++;
                    }
                    bottom--; 
                }

                // Движение снизу - вверх по левому столбцу
                if (left <= right)
                {
                    for (int i = bottom; i >= top; i--)
                    {
                        matrix[i, left] = value++;
                    }
                    left++; // Сдвигаем левую границу вправо
                }
            }

            Console.WriteLine("\nРезультат заполнения по спирали:");
            PrintMatrix(matrix);
        }

        // Задание 2: Умножение двумерного массива (матрицы) на число
        static void Task2()
        {
            Console.WriteLine("Задание 2 (Умножение матрицы на число)"); 
            int[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 } };

            Console.WriteLine("Исходная матрица:");
            PrintMatrix(matrix);

            Console.Write("Введите число (множитель): ");
            int multiplier = Convert.ToInt32(Console.ReadLine());

            // Умножаем каждый элемент на число по отдельности
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = matrix[i, j] * multiplier;
                }
            }

            Console.WriteLine("\nМатрица после умножения:");
            PrintMatrix(matrix);
        }

        // Задание 3: Сложение двух матриц одинакового размера
        static void Task3()
        {
            Console.WriteLine("Задание 3 (Сложение двух матриц)");
            int[,] matrixA = { { 10, 20 }, { 30, 40 } };
            int[,] matrixB = { { 1, 2 }, { 3, 4 } };

            Console.WriteLine("Матрица A:");
            PrintMatrix(matrixA);
            Console.WriteLine("Матрица B:");
            PrintMatrix(matrixB);

            int rows = matrixA.GetLength(0);
            int cols = matrixA.GetLength(1);
            int[,] resultMatrix = new int[rows, cols];

            // Сложение соответствующих элементов матриц
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    resultMatrix[i, j] = matrixA[i, j] + matrixB[i, j];
                }
            }

            Console.WriteLine("Результат сложения (A + B):");
            PrintMatrix(resultMatrix);
        }

        // Задание 4: Произведение (умножение) двух матриц
        static void Task4()
        {
            Console.WriteLine("Задание 4 (Произведение двух матриц)");

            // Матрица A размера 2x3
            int[,] matrixA = { { 1, 2, 3 },
                               { 4, 5, 6 } };

            // Матрица B размера 3x2 
            int[,] matrixB = { { 7, 8 },
                               { 9, 1 },
                               { 2, 3 } };

            Console.WriteLine("Матрица A (2x3):");
            PrintMatrix(matrixA);
            Console.WriteLine("Матрица B (3x2):");
            PrintMatrix(matrixB);

            int rowsA = matrixA.GetLength(0);
            int colsA = matrixA.GetLength(1);
            int colsB = matrixB.GetLength(1);

            int[,] resultMatrix = new int[rowsA, colsB];

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    resultMatrix[i, j] = 0;
                    for (int k = 0; k < colsA; k++)
                    {
                        resultMatrix[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            }

            Console.WriteLine("Результат матричного произведения (A * B):");
            PrintMatrix(resultMatrix);
        }

        // Вспомогательный метод для построчного вывода матриц на экран с табуляцией
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