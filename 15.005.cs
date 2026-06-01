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
                Console.WriteLine("МЕНЮ ДОМАШНЕГО ЗАДАНИЯ");
                Console.WriteLine("1 - Задание 1 (Структура Article)");
                Console.WriteLine("2 - Задание 2 (Структура Client)");
                Console.WriteLine("3 - Задание 3 (Массив структур Article)");
                Console.WriteLine("4 - Задание 4 (Структура Request)");
                Console.WriteLine("0 - Выход");
                Console.WriteLine("___");
                Console.Write("Выберите пункт: ");

                string choice = Console.ReadLine();
                Console.Clear();

                if (choice == "1") Task1();
                else if (choice == "2") Task2();
                else if (choice == "3") Task3();
                else if (choice == "4") Task4();
                else if (choice == "0") return;
                else Console.WriteLine("Неверный ввод!");

                Console.WriteLine("\nНажмите Enter для возврата в меню...");
                Console.ReadLine();
            }
        }

        // ЗАДАНИЕ 1: Описание структуры Article и перечисления ArticleType
        enum ArticleType { Food, Clothing, Electronics, Books }

        struct Article
        {
            public int Code;
            public string Name;
            public double Price;
            public ArticleType Type; 

            public void Print()
            {
                Console.WriteLine($"Код: {Code} | Товар: {Name} | Цена: {Price} грн | Тип: {Type}");
            }
        }

        static void Task1()
        {
            Console.WriteLine("Задание 1 (Товар)");
            Article item;
            item.Code = 101;
            item.Name = "Ноутбук";
            item.Price = 25000.50;
            item.Type = ArticleType.Electronics;

            item.Print();
        }
        // ЗАДАНИЕ 2: Описание структуры Client и перечисления ClientType
        enum ClientType { Common, VIP, Corporate }

        struct Client
        {
            public int ClientCode;
            public string FullName;
            public string Address;
            public string Phone;
            public int OrderCount;
            public double TotalOrderSum;
            public ClientType Type;

            public void Print()
            {
                Console.WriteLine($"Код: {ClientCode} | ФИО: {FullName}");
                Console.WriteLine($"Адрес: {Address} | Тел: {Phone}");
                Console.WriteLine($"Заказов: {OrderCount} | Сумма: {TotalOrderSum} грн | Статус: {Type}");
            }
        }

        static void Task2()
        {
            Console.WriteLine("Задание 2 (Клиент)");
            Client customer;
            customer.ClientCode = 777;
            customer.FullName = "Иванов Иван Иванович";
            customer.Address = "ул. Главная, д. 10";
            customer.Phone = "+380501234567";
            customer.OrderCount = 5;
            customer.TotalOrderSum = 12500.00;
            customer.Type = ClientType.VIP;

            customer.Print();
        }

        // ЗАДАНИЕ 3: Массив структур Article
        static void Task3()
        {
            Console.WriteLine("Задание 3 (Массив товаров)");
            Article[] shopStock = new Article[3];

            shopStock[0].Code = 201; shopStock[0].Name = "Хлеб"; shopStock[0].Price = 15.20; shopStock[0].Type = ArticleType.Food;
            shopStock[1].Code = 202; shopStock[1].Name = "Джинсы"; shopStock[1].Price = 1200.00; shopStock[1].Type = ArticleType.Clothing;
            shopStock[2].Code = 203; shopStock[2].Name = "Смартфон"; shopStock[2].Price = 8500.90; shopStock[2].Type = ArticleType.Electronics;

            Console.WriteLine("Список товаров на складе:");
            for (int i = 0; i < shopStock.Length; i++)
            {
                shopStock[i].Print();
            }
        }

        // ЗАДАНИЕ 4: Описание структуры Request и перечисления PayType
        enum PayType { Cash, Card, BankTransfer }

        struct Request
        {
            public int RequestCode;
            public Client Customer;
            public string OrderDate;
            public Article[] OrderedItems;
            public PayType Payment;

            // Вычисляемое свойство для подсчета суммы заказа
            public double TotalSum
            {
                get
                {
                    double total = 0;
                    if (OrderedItems != null)
                    {
                        for (int i = 0; i < OrderedItems.Length; i++)
                        {
                            total += OrderedItems[i].Price;
                        }
                    }
                    return total;
                }
            }

            public void PrintRequest()
            {
                Console.WriteLine($"Заказ №: {RequestCode} | Дата: {OrderDate} | Форма оплаты: {Payment}");
                Console.WriteLine($"Клиент: {Customer.FullName}");
                Console.WriteLine("Перечень товаров в заказе:");

                if (OrderedItems != null)
                {
                    for (int i = 0; i < OrderedItems.Length; i++)
                    {
                        Console.WriteLine($" - {OrderedItems[i].Name}: {OrderedItems[i].Price} грн");
                    }
                }
                Console.WriteLine($"ИТОГО К ОПЛАТЕ: {TotalSum} грн");
            }
        }

        static void Task4()
        {
            Console.WriteLine("Задание 4 (Квитанция заказа)");

            Client buyer;
            buyer.ClientCode = 55;
            buyer.FullName = "Петров Петр Петрович";
            buyer.Address = "ул. Садовая, 5";
            buyer.Phone = "0931112233";
            buyer.OrderCount = 1;
            buyer.TotalOrderSum = 0;
            buyer.Type = ClientType.Common;

            Article[] items = new Article[2];
            items[0].Code = 301; items[0].Name = "Книга по C#"; items[0].Price = 450.00; items[0].Type = ArticleType.Books;
            items[1].Code = 302; items[1].Name = "Ручка"; items[1].Price = 25.50; items[1].Type = ArticleType.Office; // Используем Books/Food за неимением Office

            Request order;
            order.RequestCode = 9001;
            order.Customer = buyer;
            order.OrderDate = "01.06.2026";
            order.OrderedItems = items;
            order.Payment = PayType.Card;

            order.PrintRequest();
        }
    }
}