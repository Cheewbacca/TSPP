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

            public string Sertifikat;

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
            public OrderDevelopment(string customer, int count, string subject, string sertifikat)
            {
                Customer = customer;
                Subject = subject;
                Count = count;
                Sertifikat = sertifikat;
            }

            public override void MakeOrder()
            {
                Console.WriteLine("Замовлення на розробку прийнято");
                Console.WriteLine("Замовник: " + Customer);
                Console.WriteLine("Замовлено: " + Subject);
                Console.WriteLine("Кількість: " + Count);
                Console.WriteLine("Розробку дозволено ліцензією: " + Sertifikat);
            }

            public override int GetCount()
            {
                return Count;
            }
        }

        class Order
        {
            Dictionary<string, OrderFlyweight> Orders = new Dictionary<string, OrderFlyweight>();

            public Order(string customer, string subject, int count, string sertifikat)
            {
                Orders.Add("Виготовлення", new OrderProduction(customer, count, subject));
                Orders.Add("Розробка", new OrderDevelopment(customer, count, subject, sertifikat));
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

        abstract class Command
        {
            public abstract string Execute();
        }

        class Sertifikat : Command
        {
            ControlService service;

            public Sertifikat(ControlService r)
            {
                service = r;
            }

            public override string Execute()
            {
                return service.Operation();
            }

        }

        class ControlService
        {
            public string Name { get; set; }
            public ControlService(string name)
            {
                Name = name;
            }

            public string Operation()
            {
                Random rd = new Random();
                const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@$?_-";
                char[] chars = new char[5];

                for (int i = 0; i < 5; i++)
                {
                    chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
                }

                return new string(chars);
            }
        }

        class Invoker
        {
            Command command;
            public void SetCommand(Command c)
            {
                command = c;
            }

            public string Run()
            {
                return command.Execute();
            }

        }

        class Manager
        {
            public string Name { get; set; }

            public Manager(string name)
            {
                Name = name;
            }

            public string CommunicateWithControlService()
            {
                Invoker invoker = new Invoker();
                ControlService receiver = new ControlService("Служба контролю");
                Sertifikat sertifikat = new Sertifikat(receiver);
                invoker.SetCommand(sertifikat);
                return invoker.Run();
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Manager Ivan = new Manager("Ivan");

            MilitaryIndustrialComplex developer;

            Order orderForNewProduction = new Order("Президент України", "Танк", 3, Ivan.CommunicateWithControlService());
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

            Order orderForNewDevelopment = new Order("Міністр оборони України", "Пістолет", 4, Ivan.CommunicateWithControlService());
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
