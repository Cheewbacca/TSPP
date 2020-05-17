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
            
            public string Customer { get; set; }
            public string Subject { get; set; }
            public int Count { get; set; }
            public string Sertifikat { get; set; }

            Dictionary<int, OrderFlyweight> Orders = new Dictionary<int, OrderFlyweight>();

            public Order(string customer, string subject, int count, string sertifikat)
            {
                Customer = customer;
                Subject = subject;
                Count = count;
                Sertifikat = sertifikat;
                Orders.Add(1 , new OrderProduction(Customer, Count, Subject));
                Orders.Add(2 , new OrderDevelopment(Customer, Count, Subject, Sertifikat));
            }

            public OrderFlyweight NewOrder(int key)
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
                const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";
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

        abstract class OrderHandler
        {
            public OrderHandler Sucessor { get; set; }
            public abstract void orderHandlerRequest(int condition);
        }

        class HandlerToStorage : OrderHandler
        {
           
            public override void orderHandlerRequest(int condition)
            {
                if (condition == 1)
                {
                    Console.WriteLine("Пошук в базі даних...");
                    Console.WriteLine("Товар знайдено!");
                }
                else if (Sucessor != null)
                {
                    Sucessor.orderHandlerRequest(condition);
                }
            }
        }

        class HandlerToMilitaryIndustrialComplex : OrderHandler
        {
            public override void orderHandlerRequest(int condition)
            {
                if (condition == 2)
                {
                    Console.WriteLine("Отримуємо сертифікат на розробку...");
                }
                else if (Sucessor != null)
                {
                    Sucessor.orderHandlerRequest(condition);
                }
            }
        }

        class Client
        {
            public int makeOrder()
            {
                Console.WriteLine("Введіть 1 для замовлення існуючого товару \n");
                Console.WriteLine("Або 2 для розробки нового \n");
                int orderType = Convert.ToInt32(Console.ReadLine());

                OrderHandler h1 = new HandlerToStorage();
                OrderHandler h2 = new HandlerToMilitaryIndustrialComplex();
                h1.Sucessor = h2;
                h1.orderHandlerRequest(orderType);

                return orderType;
            }
        }

        static void makeProdiction(int orderType, Order orderForNewProduction, MilitaryIndustrialComplex developer)
        {
            OrderFlyweight production = orderForNewProduction.NewOrder(orderType);

            if (production != null)
            {
                production.MakeOrder();

                Console.WriteLine("\n");

                Console.WriteLine("\n");
                // Замовлення на створення танку
                for (int i = 0; i < production.GetCount(); i++)
                {
                    Equipment tank = developer.Create("Тигр", "Важкий танк");
                    Console.WriteLine("\n");
                }

                Console.WriteLine("\n");
            }
        }

        static void makeNewDevelopment(int orderType, Order orderForNewDevelopment, MilitaryIndustrialComplex developer)
        {
            OrderFlyweight development = orderForNewDevelopment.NewOrder(orderType);
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
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Manager Ivan = new Manager("Ivan");

            Client client = new Client();

            Order orderForNewProduction, orderForNewDevelopment;

            while (true)
            {
                int orderType = client.makeOrder();

                if (orderType == 1)
                {
                    Console.WriteLine("Введіть замовника: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Кількість продукції: ");
                    int count = Convert.ToInt32(Console.ReadLine());
                    orderForNewProduction = new Order(name, "Танк", count, Ivan.CommunicateWithControlService());
                    makeProdiction(orderType, orderForNewProduction, new TankDeveloper("ДП «Харківський завод спеціальних машин»", "EF550423"));
                }
                if (orderType == 2)
                {
                    Console.WriteLine("Введіть замовника: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Кількість продукції: ");
                    int count = Convert.ToInt32(Console.ReadLine());
                    orderForNewDevelopment = new Order(name, "Пістолет", count, Ivan.CommunicateWithControlService());
                    makeNewDevelopment(orderType, orderForNewDevelopment, new WeaponDeveloper("ВАТ «Завод 'Маяк'»", "KL449293")); 
                }
                Console.ReadLine();
            }
        }
    }
}
