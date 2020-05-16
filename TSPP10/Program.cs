using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace TSPP10
{
    class Program
    {
        abstract class OrderFlyweight
        {
            protected string Subject;

            protected int Count;

            public string Customer;

            public abstract void MakeOrder();

            public abstract int GetCount();
        }

        class OrderProduction : OrderFlyweight
        {
            public OrderProduction(string customer, int count, string subject)
            {
                Customer = customer;
                Subject = subject;
                Count = count;
            }

            public override void MakeOrder()
            {
                Console.WriteLine("Замовлення на виготовлення прийнято");
                Console.WriteLine("Замовник: " + Customer);
                Console.WriteLine("Замовлено: " + Subject);
                Console.WriteLine("Кількість: " + Count);
            }

            public override int GetCount()
            {
                return Count;
            }
        }

        class OrderDevelopment : OrderFlyweight
        {
            public OrderDevelopment(string customer, int count, string subject)
            {
                Customer = customer;
                Subject = subject;
                Count = count;
            }

            public override void MakeOrder()
            {
                Console.WriteLine("Замовлення на розробку прийнято");
                Console.WriteLine("Замовник: " + Customer);
                Console.WriteLine("Замовлено: " + Subject);
                Console.WriteLine("Кількість: " + Count);
            }

            public override int GetCount()
            {
                return Count;
            }
        }

        class Order
        {
            Dictionary<string, OrderFlyweight> Orders = new Dictionary<string, OrderFlyweight>();

            public Order(string customer, string subject, int count)
            {
                Orders.Add("Виготовлення", new OrderProduction(customer, count, subject));
                Orders.Add("Розробка", new OrderDevelopment(customer, count, subject));
            }

            public OrderFlyweight NewOrder(string key)
            {
                if (Orders.ContainsKey(key))
                {
                    return Orders[key];
                }
                else
                {
                    return null;
                }
            }
        }

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
                Console.WriteLine("Назва: " + name);
                Console.WriteLine("Тип: " + type);
            }
        }
        class Weapon : Equipment // зброя, яка є виробом
        {
            // Виробництво зброї
            public Weapon(string name, string type) : base(name, type)
            {
                Console.WriteLine("Виготовлено нову зброю");
                Console.WriteLine("Назва: " + name);
                Console.WriteLine("Тип: " + type);
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            MilitaryIndustrialComplex developer;

            Order orderForNewProduction = new Order("Президент України", "Танк", 3);
            OrderFlyweight production = orderForNewProduction.NewOrder("Виготовлення");
            if (production != null)
            {
                production.MakeOrder();

                Console.WriteLine("\n");

                // Створення обь'єкта-ВПК - виробника танків 
                developer = new TankDeveloper("ДП «Харківський завод спеціальних машин»", "EF550423");
                Console.WriteLine("\n");
                // Замовлення на створення танку
                for (int i = 0; i < production.GetCount(); i++)
                {
                    Equipment tank = developer.Create("Тигр", "Важкий танк");
                    Console.WriteLine("\n");
                }

                Console.WriteLine("\n");
            }

            Order orderForNewDevelopment = new Order("Міністр оборони України", "Пістолет", 4);
            OrderFlyweight development = orderForNewDevelopment.NewOrder("Розробка");
            if (development != null)
            {
                development.MakeOrder();

                Console.WriteLine("\n");

                // Створення обь'єкта-ВПК - виробника зброї 
                developer = new WeaponDeveloper("ВАТ «Завод 'Маяк'»", "KL449293");
                Console.WriteLine("\n");
                // Замовлення на створення зброї
                for (int i = 0; i < development.GetCount(); i++)
                {
                    Equipment weapon = developer.Create("Макаров", "Пістолет");
                    Console.WriteLine("\n");
                }
            }

            Console.ReadLine();
        }
    }
}
