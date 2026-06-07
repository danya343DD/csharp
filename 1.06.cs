using System;

namespace nagaev
{
    // ПОЛЬЗОВАТЕЛЬСКИЕ ИСКЛЮЧЕНИЯ ДЛЯ ЗАДАНИЯ 2
    public class InvalidAmountException : Exception
    {
        public InvalidAmountException(string message) : base(message) { }
    }

    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException(string message) : base(message) { }
    }

    // КЛАСС ДЛЯ РАБОТЫ С БАНКОВСКИМ СЧЕТОМ (ЗАДАНИЕ 2)
    public class BankAccount
    {
        public decimal Balance { get; private set; }

        public BankAccount()
        {
            Balance = 0;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new InvalidAmountException("Сумма пополнения должна быть положительной.");
            }
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new InvalidAmountException("Сумма снятия должна быть положительной.");
            }
            if (amount > Balance)
            {
                throw new InsufficientFundsException("Недостаточно средств на счете.");
            }
            Balance -= amount;
        }
    }

    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("ГЛАВНОЕ МЕНЮ (ОБРАБОТКА ИСКЛЮЧЕНИЙ)");
                Console.WriteLine("1 - Задание 1 (Безопасное деление и массив)");
                Console.WriteLine("2 - Задание 2 (Банковский счет и кастомные исключения)");
                Console.WriteLine("0 - Выход");
                Console.Write("Выберите пункт: ");

                string choice = Console.ReadLine();
                Console.Clear();

                if (choice == "1") Task1();
                else if (choice == "2") Task2();
                else if (choice == "0") return;
                else Console.WriteLine("Неверный ввод!");

                Console.WriteLine("\nНажмите Enter для возврата в меню...");
                Console.ReadLine();
            }
        }

        // ЗАДАНИЕ 1: Безопасное деление и работа с массивами
        static void Task1()
        {
            Console.WriteLine("Задание 1 (Работа с исключениями)\n");
            int[] results = new int[3];
            int currentIndex = 0;

            while (true)
            {
                try
                {
                    Console.Write("Введите первое целое число: ");
                    int num1 = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Введите второе целое число: ");
                    int num2 = Convert.ToInt32(Console.ReadLine());

                    int divisionResult = num1 / num2;

                    if (currentIndex >= results.Length)
                    {
                        throw new IndexOutOfRangeException();
                    }

                    results[currentIndex] = divisionResult;
                    currentIndex++;
                    Console.WriteLine("Результат успешно сохранен в массив.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: введите целое число.");
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine("Ошибка: деление на ноль невозможно.");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Массив результатов заполнен, дальнейшие вычисления невозможны.");
                    break; // Завершаем программу (выходим из цикла) при переполнении
                }
                finally
                {
                    Console.WriteLine("Попытка выполнения операции завершена.");
                }

                Console.Write("\nХотите продолжить? (да/нет): ");
                string answer = Console.ReadLine().ToLower();
                if (answer != "да" && answer != "y")
                {
                    break;
                }
                Console.WriteLine();
            }
        }

        // ЗАДАНИЕ 2: Работа с банковским счетом
        static void Task2()
        {
            Console.WriteLine("Задание 2 (Банковский счет)\n");
            BankAccount account = new BankAccount();

            while (true)
            {
                Console.WriteLine("\nДоступные действия:");
                Console.WriteLine("1 - Пополнить баланс");
                Console.WriteLine("2 - Снять средства");
                Console.WriteLine("3 - Показать текущий баланс");
                Console.WriteLine("4 - Вернуться в главное меню");
                Console.Write("Выберите действие: ");

                string action = Console.ReadLine();
                if (action == "4") break;

                try
                {
                    if (action == "1")
                    {
                        Console.Write("Введите сумму для пополнения: ");
                        decimal amount = Convert.ToDecimal(Console.ReadLine());
                        account.Deposit(amount);
                        Console.WriteLine("Операция успешна.");
                    }
                    else if (action == "2")
                    {
                        Console.Write("Введите сумму для снятия: ");
                        decimal amount = Convert.ToDecimal(Console.ReadLine());
                        account.Withdraw(amount);
                        Console.WriteLine("Операция успешна.");
                    }
                    else if (action == "3")
                    {
                        // Просто выводим баланс, исключения тут маловероятны
                    }
                    else
                    {
                        Console.WriteLine("Неверный выбор действия.");
                        continue;
                    }
                }
                catch (InvalidAmountException ex)
                {
                    Console.WriteLine("Ошибка суммы: " + ex.Message);
                }
                catch (InsufficientFundsException ex)
                {
                    Console.WriteLine("Ошибка средств: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла непредвиденная ошибка: " + ex.Message);
                }

                // Вывод баланса с точностью до двух знаков после каждой операции
                Console.WriteLine($"Текущий баланс: {account.Balance:F2}");
            }
        }
    }
}