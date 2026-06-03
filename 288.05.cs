using System;
using System.Collections.Generic;

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
                Console.WriteLine("1 - Задание 1 (Геометрические фигуры и составная фигура)");
                Console.WriteLine("2 - Задание 2 (Иерархия классов логистики товаров)");
                Console.WriteLine("0 - Выход");
                Console.WriteLine("___");
                Console.Write("Выберите пункт меню: ");

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

        // ЗАДАНIЕ 1: Иерархия геометрических фигур и составная фигура
        abstract class Shape
        {
            public abstract double GetArea();
            public abstract double GetPerimeter();
        }

        class Triangle : Shape
        {
            private double a, b, c;
            public Triangle(double sideA, double sideB, double sideC) { a = sideA; b = sideB; c = sideC; }
            public override double GetPerimeter() { return a + b + c; }
            public override double GetArea()
            {
                double p = GetPerimeter() / 2;
                return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
            }
        }

        class Square : Shape
        {
            private double side;
            public Square(double s) { side = s; }
            public override double GetArea() { return side * side; }
            public override double GetPerimeter() { return 4 * side; }
        }

        class Rhombus : Shape
        {
            private double side, height;
            public Rhombus(double s, double h) { side = s; height = h; }
            public override double GetArea() { return side * height; }
            public override double GetPerimeter() { return 4 * side; }
        }

        class Rectangle : Shape
        {
            private double width, height;
            public Rectangle(double w, double h) { width = w; height = h; }
            public override double GetArea() { return width * height; }
            public override double GetPerimeter() { return 2 * (width + height); }
        }

        class Parallelogram : Shape
        {
            private double baseSide, side, height;
            public Parallelogram(double b, double s, double h) { baseSide = b; side = s; height = h; }
            public override double GetArea() { return baseSide * height; }
            public override double GetPerimeter() { return 2 * (baseSide + side); }
        }

        class Trapezoid : Shape
        {
            private double a, b, c, d, height;
            public Trapezoid(double sideA, double sideB, double sideC, double sideD, double h)
            { a = sideA; b = sideB; c = sideC; d = sideD; height = h; }
            public override double GetArea() { return 0.5 * (a + b) * height; }
            public override double GetPerimeter() { return a + b + c + d; }
        }

        class Circle : Shape
        {
            private double radius;
            public Circle(double r) { radius = r; }
            public override double GetArea() { return Math.PI * radius * radius; }
            public override double GetPerimeter() { return 2 * Math.PI * radius; }
        }

        class Ellipse : Shape
        {
            private double majorAxis, minorAxis;
            public Ellipse(double a, double b) { majorAxis = a; minorAxis = b; }
            public override double GetArea() { return Math.PI * majorAxis * minorAxis; }
            public override double GetPerimeter()
            {
                return Math.PI * (3 * (majorAxis + minorAxis) - Math.Sqrt((3 * majorAxis + minorAxis) * (majorAxis + 3 * minorAxis)));
            }
        }

        // Класс «Составная Фигура»
        class CompositeShape
        {
            private List<Shape> shapes = new List<Shape>();

            public void AddShape(Shape shape)
            {
                shapes.Add(shape);
            }

            public double GetTotalArea()
            {
                double totalArea = 0;
                foreach (Shape shape in shapes)
                {
                    totalArea += shape.GetArea();
                }
                return totalArea;
            }
        }

        static void Task1()
        {
            Console.WriteLine("Задание 1 (Геометрические фигуры)\n");

            CompositeShape composite = new CompositeShape();
            composite.AddShape(new Square(5));
            composite.AddShape(new Circle(3));
            composite.AddShape(new Rectangle(4, 6));
            composite.AddShape(new Triangle(3, 4, 5));

            Console.WriteLine("Общая площадь составной фигуры: " + composite.GetTotalArea());
        }

        // ЗАДАНIЕ 2: Архитектура иерархии товаров для дистрибьюторской компании
        abstract class Product
        {
            public string Name { get; set; }
            public string SKU { get; set; }
            public double Price { get; set; }
            public double Weight { get; set; }

            public Product(string name, string sku, double price, double weight)
            {
                Name = name;
                SKU = sku;
                Price = price;
                Weight = weight;
            }

            public abstract void DisplayLogisticsInfo();
        }

        // Скоропортящиеся продукты
        class PerishableProduct : Product
        {
            public string ExpirationDate { get; set; }
            public double RequiredTemperature { get; set; }

            public PerishableProduct(string name, string sku, double price, double weight, string expDate, double temp)
                : base(name, sku, price, weight)
            {
                ExpirationDate = expDate;
                RequiredTemperature = temp;
            }

            public override void DisplayLogisticsInfo()
            {
                Console.WriteLine($"Товар: {Name} (SKU: {SKU}) | Тип: Скоропортящийся");
                Console.WriteLine($"Условия транспортировки: Срок до {ExpirationDate}, Температура: {RequiredTemperature}°C");
            }
        }

        // Хрупкие товары
        class FragileProduct : Product
        {
            public string PackagingMaterial { get; set; }
            public int MaxStackHeight { get; set; }

            public FragileProduct(string name, string sku, double price, double weight, string packaging, int maxStack)
                : base(name, sku, price, weight)
            {
                PackagingMaterial = packaging;
                MaxStackHeight = maxStack;
            }

            public override void DisplayLogisticsInfo()
            {
                Console.WriteLine($"Товар: {Name} (SKU: {SKU}) | Тип: Хрупкий груз");
                Console.WriteLine($"Условия транспортировки: Упаковка: {PackagingMaterial}, Макс. ярусов: {MaxStackHeight}");
            }
        }

        // Опасные грузы
        class HazardousProduct : Product
        {
            public string HazardClass { get; set; }
            public string UN_Number { get; set; }

            public HazardousProduct(string name, string sku, double price, double weight, string hazardClass, string unNumber)
                : base(name, sku, price, weight)
            {
                HazardClass = hazardClass; UN_Number = unNumber;
            }

            public override void DisplayLogisticsInfo()
            {
                Console.WriteLine($"Товар: {Name} (SKU: {SKU}) | Тип: Опасный груз");
                Console.WriteLine($"Условия транспортировки: Класс опасности: {HazardClass}, Код ООН: {UN_Number}");
            }
        }

        // Крупногабаритные товары
        class OversizedProduct : Product
        {
            public string Dimensions { get; set; }
            public bool RequiresSpecialVehicle { get; set; }

            public OversizedProduct(string name, string sku, double price, double weight, string dimensions, bool specVehicle)
                : base(name, sku, price, weight)
            {
                Dimensions = dimensions;
                RequiresSpecialVehicle = specVehicle;
            }

            public override void DisplayLogisticsInfo()
            {
                Console.WriteLine($"Товар: {Name} (SKU: {SKU}) | Тип: Крупногабаритный");
                Console.WriteLine($"Условия транспортировки: Размеры: {Dimensions}, Спец. транспорт: {RequiresSpecialVehicle}");
            }
        }

        static void Task2()
        {
            Console.WriteLine("Задание 2 (Управление потоками товаров)\n");

            List<Product> warehouse = new List<Product>();
            warehouse.Add(new PerishableProduct("Молоко", "PER-001", 80, 1.0, "10.06.2026", 4.0));
            warehouse.Add(new FragileProduct("Зеркало", "FRG-552", 4500, 12.5, "Пузырчатая пленка", 2));
            warehouse.Add(new HazardousProduct("Краска Аэрозоль", "HAZ-991", 600, 0.4, "Класс 2.1", "UN1950"));
            warehouse.Add(new OversizedProduct("Шкаф собранный", "OVZ-303", 18000, 85.0, "2.2x1.5x0.6м", true));

            foreach (Product prod in warehouse)
            {
                prod.DisplayLogisticsInfo();
                Console.WriteLine();
            }
        }
    }
}