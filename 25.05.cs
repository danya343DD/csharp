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
                Console.WriteLine("-ГЛАВНОЕ МЕНЮ (НАСЛЕДОВАНИЕ)-");
                Console.WriteLine("1 - Задание 1 (Класс Human и профессии)");
                Console.WriteLine("2 - Задание 2 (Паспорта)");
                Console.WriteLine("3 - Задание 3 (Животные)");
                Console.WriteLine("4 - Задание 4 (Абстрактные фигуры)");
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

        // ЗАДАНИЕ 1: Иерархия классов Human, Builder, Sailor, Pilot
        class Human
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public Human(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public virtual void PrintInfo()
            {
                Console.WriteLine($"Имя: {Name}, Возраст: {Age}");
            }
        }

        class Builder : Human
        {
            public string ObjectType { get; set; }

            public Builder(string name, int age, string objectType) : base(name, age)
            {
                ObjectType = objectType;
            }

            public override void PrintInfo()
            {
                base.PrintInfo();
                Console.WriteLine($"Профессия: Строитель, Объект: {ObjectType}");
            }

            public void Build() { Console.WriteLine($"{Name} строит объект..."); }
        }

        class Sailor : Human
        {
            public string ShipName { get; set; }

            public Sailor(string name, int age, string shipName) : base(name, age)
            {
                ShipName = shipName;
            }

            public override void PrintInfo()
            {
                base.PrintInfo();
                Console.WriteLine($"Профессия: Моряк, Корабль: {ShipName}");
            }

            public void Sail() { Console.WriteLine($"{Name} уходит в плавание..."); }
        }

        class Pilot : Human
        {
            public string AircraftType { get; set; }

            public Pilot(string name, int age, string aircraftType) : base(name, age)
            {
                AircraftType = aircraftType;
            }

            public override void PrintInfo()
            {
                base.PrintInfo();
                Console.WriteLine($"Профессия: Летчик, Самолет: {AircraftType}");
            }

            public void Fly() { Console.WriteLine($"{Name} взлетает..."); }
        }

        static void Task1()
        {
            Console.WriteLine("Задание 1 (Профессии)\n");
            Builder builder = new Builder("Алексей", 35, "Жилой комплекс");
            Sailor sailor = new Sailor("Дмитрий", 28, "Аврора");
            Pilot pilot = new Pilot("Иван", 42, "Боинг-737");

            builder.PrintInfo(); builder.Build(); Console.WriteLine();
            sailor.PrintInfo(); sailor.Sail(); Console.WriteLine();
            pilot.PrintInfo(); pilot.Fly();
        }

        // ЗАДАНИЕ 2: Паспорт и Загранпаспорт
        class Passport
        {
            public string Number { get; set; }
            public string HolderName { get; set; }
            public string Country { get; set; }

            public Passport(string number, string holderName, string country)
            {
                Number = number;
                HolderName = holderName;
                Country = country;
            }

            public virtual void Show()
            {
                Console.WriteLine($"Паспорт гражданина страны: {Country}");
                Console.WriteLine($"Номер: {Number}, Владелец: {HolderName}");
            }
        }

        class ForeignPassport : Passport
        {
            public string ForeignNumber { get; set; }
            public string Visas { get; set; }

            public ForeignPassport(string number, string holderName, string country, string foreignNumber, string visas)
                : base(number, holderName, country)
            {
                ForeignNumber = foreignNumber;
                Visas = visas;
            }

            public override void Show()
            {
                base.Show();
                Console.WriteLine($"Загранпаспорт №: {ForeignNumber}, Открытые визы: {Visas}");
            }
        }

        static void Task2()
        {
            Console.WriteLine("Задание 2 (Паспорта)\n");
            ForeignPassport fp = new ForeignPassport("4508-123456", "Петров П.П.", "РФ", "75№9876543", "Шенген, США");
            fp.Show();
        }

        // ЗАДАНИЕ 3: Животные (Тигр, Крокодил, Кенгуру)
        class Animal
        {
            public string Name { get; set; }
            public string Characteristic { get; set; }

            public Animal(string name, string characteristic)
            {
                Name = name;
                Characteristic = characteristic;
            }

            public virtual void ShowInfo()
            {
                Console.WriteLine($"Животное: {Name}, Особенность: {Characteristic}");
            }
        }

        class Tiger : Animal
        {
            public Tiger(string name, string characteristic) : base(name, characteristic) { }
            public void Roar() { Console.WriteLine($"{Name} громко рычит!"); }
        }

        class Crocodile : Animal
        {
            public Crocodile(string name, string characteristic) : base(name, characteristic) { }
            public void Swim() { Console.WriteLine($"{Name} бесшумно плывет."); }
        }

        class Kangaroo : Animal
        {
            public Kangaroo(string name, string characteristic) : base(name, characteristic) { }
            public void Jump() { Console.WriteLine($"{Name} высоко прыгает."); }
        }

        static void Task3()
        {
            Console.WriteLine("Задание 3 (Животные)\n");
            Tiger tiger = new Tiger("Шерхан", "Полосатый окрас, грозный хищник");
            Crocodile croc = new Crocodile("Гена", "Зеленый чешуйчатый кожный покров");
            Kangaroo kanga = new Kangaroo("Джек", "Наличие сумки на животе для потомства");

            tiger.ShowInfo(); tiger.Roar(); Console.WriteLine();
            croc.ShowInfo(); croc.Swim(); Console.WriteLine();
            kanga.ShowInfo(); kanga.Jump();
        }

        // ЗАДАНИЕ 4: Геометрические фигуры (Полиморфизм)
        abstract class Figure
        {
            public abstract double GetArea();
            public abstract void Show();
        }

        class Rectangle : Figure
        {
            private double width, height;
            public Rectangle(double w, double h) { width = w; height = h; }
            public override double GetArea() { return width * height; }
            public override void Show() { Console.WriteLine($"Прямоугольник ({width}x{height}) | Площадь: {GetArea():F2}"); }
        }

        class Circle : Figure
        {
            private double radius;
            public Circle(double r) { radius = r; }
            public override double GetArea() { return Math.PI * radius * radius; }
            public override void Show() { Console.WriteLine($"Круг (Радиус: {radius}) | Площадь: {GetArea():F2}"); }
        }

        class RightTriangle : Figure
        {
            private double baseSide, height;
            public RightTriangle(double b, double h) { baseSide = b; height = h; }
            public override double GetArea() { return 0.5 * baseSide * height; }
            public override void Show() { Console.WriteLine($"Прямоугольный треугольник ({baseSide}x{height}) | Площадь: {GetArea():F2}"); }
        }

        class Trapezoid : Figure
        {
            private double a, b, height;
            public Trapezoid(double sideA, double sideB, double h) { a = sideA; b = sideB; height = h; }
            public override double GetArea() { return 0.5 * (a + b) * height; }
            public override void Show() { Console.WriteLine($"Трапеция (Основания: {a}, {b}; Высота: {height}) | Площадь: {GetArea():F2}"); }
        }

        static void Task4()
        {
            Console.WriteLine("Задание 4 (Геометрические фигуры)\n");

            Figure[] figures = new Figure[]
            {
                new Rectangle(5, 4),
                new Circle(3),
                new RightTriangle(6, 4),
                new Trapezoid(3, 7, 4)
            };

            foreach (Figure fig in figures)
            {
                fig.Show();
            }
        }
    }
}