using System;
using System.Collections.Generic;
using System.Threading;

namespace nagaev
{
    // 1. Определение делегатов
    public delegate void RaceAction();
    public delegate void RaceLogHandler(string message);

    // 2. Абстрактный базовый класс Автомобиль
    abstract class Car
    {
        protected Random rand = new Random();

        public string Name { get; set; }
        public int Speed { get; protected set; }
        public int Position { get; protected set; }

        public Car(string name)
        {
            Name = name;
            Position = 0;
        }

        // Абстрактный метод движения (каждый тип едет со своей скоростью)
        public abstract void Drive();
    }

    // Спортивный автомобиль (быстрый, но нестабильный)
    class SportsCar : Car
    {
        public SportsCar(string name) : base(name) { }
        public override void Drive()
        {
            Speed = rand.Next(15, 25);
            Position += Speed;
        }
    }

    // Легковой автомобиль (средняя стабильная скорость)
    class PassengerCar : Car
    {
        public PassengerCar(string name) : base(name) { }
        public override void Drive()
        {
            Speed = rand.Next(10, 18);
            Position += Speed;
        }
    }

    // Грузовик (медленный, но едет уверенно)
    class Truck : Car
    {
        public Truck(string name) : base(name) { }
        public override void Drive()
        {
            Speed = rand.Next(8, 14);
            Position += Speed;
        }
    }

    // Автобус (самый неторопливый)
    class Bus : Car
    {
        public Bus(string name) : base(name) { }
        public override void Drive()
        {
            Speed = rand.Next(6, 12);
            Position += Speed;
        }
    }

    // 3. Класс управления игрой «Гонки»
    class RaceGame
    {
        private List<Car> participants = new List<Car>();
        private const int FinishLine = 100;

        // События для уведомления о ходе игры
        public event RaceLogHandler OnRaceStarted;
        public event RaceLogHandler OnStepCompleted;
        public event RaceLogHandler OnRaceFinished;

        public void AddCar(Car car)
        {
            participants.Add(car);
        }

        public void StartRace()
        {
            if (participants.Count == 0)
            {
                Console.WriteLine("Нет участников для старта!");
                return;
            }

            if (OnRaceStarted != null) OnRaceStarted("Гонка началась! Дистанция: " + FinishLine + " единиц.\n");

            // Инициализируем делегат движения всех машин
            RaceAction driveAll = null;
            foreach (Car car in participants)
            {
                driveAll += car.Drive;
            }

            bool isFinished = false;
            int step = 1;

            while (!isFinished)
            {
                Console.WriteLine($"ТАКТ {step}");

                // Вызываем делегат — все машины одновременно делают ход
                driveAll();

                string statusMessage = "";
                Car leader = participants[0];

                foreach (Car car in participants)
                {
                    statusMessage += $"{car.Name}: Позиция {car.Position} (Скорость: {car.Speed})\n";

                    if (car.Position > leader.Position)
                    {
                        leader = car;
                    }

                    // Проверка достижения финиша
                    if (car.Position >= FinishLine)
                    {
                        isFinished = true;
                    }
                }

                statusMessage += $"Текущий лидер: {leader.Name}\n";
                if (OnStepCompleted != null) OnStepCompleted(statusMessage);

                step++;
                Thread.Sleep(1000); // Задержка в 1 секунду между тактами для наглядности гонки
            }

            // Определение окончательного победителя
            Car winner = participants[0]; foreach (Car car in participants)
            {
                if (car.Position > winner.Position)
                {
                    winner = car;
                }
            }

            if (OnRaceFinished != null) OnRaceFinished($"ПОБЕДИТЕЛЬ ГОНКИ: {winner.Name}!");
        }
    }

    // 4. Точка входа в программу
    class Program
    {
        static void Main()
        {
            Console.Title = "Автомобильные гонки (Делегаты и События)";

            RaceGame game = new RaceGame();

            // Добавляем разные типы машин
            game.AddCar(new SportsCar(" Ferrari (Спорт)"));
            game.AddCar(new PassengerCar(" Toyota (Легковая)"));
            game.AddCar(new Truck(" KAMAZ (Грузовик)"));
            game.AddCar(new Bus(" Ikarus (Автобус)"));

            // Подписываем метод вывода на события игры
            game.OnRaceStarted += ShowLog;
            game.OnStepCompleted += ShowLog;
            game.OnRaceFinished += ShowLog;

            // Запуск игры
            game.StartRace();

            Console.ReadLine();
        }

        // Простой метод-обработчик сообщений гонки
        static void ShowLog(string message)
        {
            Console.WriteLine(message);
        }
    }
}