using System;
using System.Text;

namespace TSPP10
{
    class Program
    {
        abstract class MilitaryIndustrialComplex // абстрактний клас ВПК

        {
            public string Name { get; set; }
            public string Sertificat { get; set; }

            public MilitaryIndustrialComplex(string n, string s)
            {
                Name = n;
                Sertificat = s;
            }

            // використання фабричного методу
            abstract public Equipment Create(string name, string type);
        }


        class TankDeveloper : MilitaryIndustrialComplex // клас виробника танків, який є частиною ВПК
        {
            // Назва виробника 
            public TankDeveloper(string n, string s) : base(n, s)
            {
                Console.WriteLine("Виробник: " + n);
                Console.WriteLine("Сертифіковано: " + s);
            }

            // Перегрузка для створення нового виробу: танк 
            public override Equipment Create(string name, string type)
            {
                return new Tank(name, type);
            }
        }

        class WeaponDeveloper : MilitaryIndustrialComplex // клас виробника зброї, який є частиною ВПК
        {
            // Назва виробника 
            public WeaponDeveloper(string n, string s) : base(n, s)
            {
                Console.WriteLine("Виробник: " + n);
                Console.WriteLine("Сертифіковано: " + s);
            }

            // Перегрузка для створення нового виробу: зброя 
            public override Equipment Create(string name, string type)
            {
                return new Weapon(name, type);
            }
        }

        abstract class Equipment // абстрактний клас виробу 
        {
            public string Name { get; set; }

            public string Type { get; set; }

            public Equipment(string name, string type)
            {
                Name = name;
                Type = type;
            }
        }

        class Tank : Equipment // танк, який є виробом
        {
            // Виробництво танку
            public Tank(string name, string type) : base(name, type)
            {
                Console.WriteLine("Виготовлено новий танк");
                Console.WriteLine("Назва " + name);
                Console.WriteLine("Тип " + type);
            }
        }
        class Weapon : Equipment // зброя, яка є виробом
        {
            // Виробництво зброї
            public Weapon(string name, string type) : base(name, type)
            {
                Console.WriteLine("Виготовлено нову зброю");
                Console.WriteLine("Назва " + name);
                Console.WriteLine("Тип " + type);
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; 

            // Створення обь'єкта-ВПК - виробника танків 
            MilitaryIndustrialComplex developer = new TankDeveloper("ДП «Харківський завод спеціальних машин»", "EF550423");
            // Замовлення на створення танку
            Equipment tank = developer.Create("Тигр", "Важкий танк");

            Console.WriteLine("\n");

            // Створення обь'єкта-ВПК - виробника зброї 
            developer = new WeaponDeveloper("ВАТ «Завод 'Маяк'»", "KL449293");
            // Замовлення на створення зброї
            Equipment weapon = developer.Create("Макаров", "Пістолет");

            Console.ReadLine();
        }
    }
}
