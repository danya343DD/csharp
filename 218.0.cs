using System;
using System.Collections.Generic;

namespace nagaev
{
    // ДЕЛЕГАТЫ И КЛАССЫ ДЛЯ ЛАБОРАТОРНОЙ РАБОТЫ №8
    public delegate void AccountHandler(string message);

    public class Account
    {
        private double sum;
        private double percentage;

        // События для уведомления о действиях со счетом
        public event AccountHandler OnOpened;
        public event AccountHandler OnClosed;
        public event AccountHandler OnPut;
        public event AccountHandler OnWithdrawn;
        public event AccountHandler OnPercentageCalculated;

        public Account(double initialSum, double percent)
        {
            sum = initialSum;
            percentage = percent;
        }

        public void Open()
        {
            if (OnOpened != null) OnOpened($"Счет открыт. Начальная сумма: {sum} грн.");
        }

        public void Close()
        {
            if (OnClosed != null) OnClosed($"Счет закрыт. Итоговая сумма на момент закрытия: {sum} грн.");
        }

        public void Put(double amount)
        {
            sum += amount;
            if (OnPut != null) OnPut($"На счет зачислено: {amount} грн. Текущий баланс: {sum} грн.");
        }

        public void Withdraw(double amount)
        {
            if (sum >= amount)
            {
                sum -= amount;
                if (OnWithdrawn != null) OnWithdrawn($"Со счета снято: {amount} грн. Текущий баланс: {sum} грн.");
            }
            else
            {
                if (OnWithdrawn != null) OnWithdrawn($"Ошибка снятия: Недостаточно средств. Баланс: {sum} грн.");
            }
        }

        public void CalculatePercentage()
        {
            double profit = sum * (percentage / 100.0);
            sum += profit;
            if (OnPercentageCalculated != null) OnPercentageCalculated($"Начислены проценты (+{percentage}%): {profit} грн. Новый баланс: {sum} грн.");
        }
    }

    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("ГЛАВНОЕ МЕНЮ ЛАБОРАТОРНЫХ РАБОТ");
                Console.WriteLine("1 - Лабораторная №7 (Статистика слов - Generic Dictionary)");
                Console.WriteLine("2 - Лабораторная №8 (Управление счетом - Делегаты и События)");
                Console.WriteLine("0 - Выход");
                Console.Write("Выберите пункт меню: ");

                string choice = Console.ReadLine();
                Console.Clear();

                if (choice == "1") Lab7();
                else if (choice == "2") Lab8();
                else if (choice == "0") return;
                else Console.WriteLine("Неверный ввод!");

                Console.WriteLine("\nНажмите Enter для возврата в меню");
                Console.ReadLine();
            }
        }

        // ЛАБОРАТОРНАЯ РАБОТА №7: Программа «Статистика» (Dictionary)
        static void Lab7()
        {
            Console.WriteLine("Лабораторная работа №7 (Статистика слов)\n");

            string text = "Вот дом, Который построил Джек. А это пшеница, Которая в темном чулане хранится В доме, Который построил Джек. А это веселая птица-синица, Которая часто ворует пшеницу, Которая в темном чулане хранится В доме, Который построил Джек.";

            // Символы для очистки текста от знаков препинания
            char[] separators = { ' ', ',', '.', '-', '!', '?' };
            string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> statistics = new Dictionary<string, int>();
            int totalWords = 0; foreach (string rawWord in words)
            {
                // Приводим к одному регистру для точного подсчета
                string word = rawWord.ToLower();
                totalWords++;

                if (statistics.ContainsKey(word))
                {
                    statistics[word]++;
                }
                else
                {
                    statistics[word] = 1;
                }
            }

            // Вывод таблицы результатов
            Console.WriteLine("{0,-20} | {1,-10}", "Слово", "Кол-во");
            foreach (KeyValuePair<string, int> pair in statistics)
            {
                Console.WriteLine("{0,-20} | {1,-10}", pair.Key, pair.Value);
            }

            Console.WriteLine($"Всего слов: {totalWords} | Из них уникальных: {statistics.Count}");
        }

        // ЛАБОРАТОРНАЯ РАБОТА №8: Делегаты и события (Банковский счет)
        static void Lab8()
        {
            Console.WriteLine("Лабораторная работа №8 (Делегаты и События)\n");

            // Создаем банковский счет: 1000 грн начальный баланс, 4.5% годовых/месячных
            Account myAccount = new Account(1000.0, 4.5);

            // Подписываем один и тот же метод (обработчик) на все события счета
            myAccount.OnOpened += ShowMessage;
            myAccount.OnClosed += ShowMessage;
            myAccount.OnPut += ShowMessage;
            myAccount.OnWithdrawn += ShowMessage;
            myAccount.OnPercentageCalculated += ShowMessage;

            // Демонстрация логики работы со счетом и вызова событий
            myAccount.Open();
            myAccount.Put(500.0);
            myAccount.Withdraw(200.0);
            myAccount.CalculatePercentage();
            myAccount.Withdraw(2000.0); // Симуляция ошибки нехватки средств
            myAccount.Close();
        }

        // Метод-обработчик событий, соответствующий делегату AccountHandler
        static void ShowMessage(string message)
        {
            Console.WriteLine("[УВЕДОМЛЕНИЕ]: " + message);
        }
    }
}