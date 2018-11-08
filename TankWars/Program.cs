using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWars
{
    class Program
    {
        static void Main(string[] args)
        {
            Tank ourTank = new Tank(3, 30, 10);
            Tank enemyTank = new Tank(5, 25, 8);

            Random enemyRandom = new Random();
            bool isEnd = false;
            bool IsParsed = false;
            Actions ourAction = Actions.Shoot;
            Actions enmAction = Actions.Shoot; 

            while (!isEnd)
            {
                Console.WriteLine("\n\nНовый ход!");
                Console.WriteLine($"Наш танк.        Жизни: {ourTank.Health}");
                Console.WriteLine($"Танк противника. Жизни: {enemyTank.Health}");
                Console.WriteLine("Доступные действия: \n" +
                    " 1. Выстрел\n" +
                    " 2. Ремонт\n" +
                    "Введите число, соотвествующее желаемому действию:");

                // Считывание действия пользователя с проверкой правильности ввода. 
                string ioAction = "";
                do
                {
                    ioAction = Console.ReadLine();
                    if (ioAction != "1" && ioAction != "2")
                    {
                        Console.WriteLine("Введите 1 или 2");
                        IsParsed = false; 
                    }
                    else
                        IsParsed = true;
                }
                while (!IsParsed);
                ourAction = (Actions)int.Parse(ioAction); 
                
                // Наш ход. 
                switch (ourAction)
                {
                    case Actions.Shoot:
                        Console.WriteLine("Наш танк стреляет!"); 
                        ourTank.Shoot(enemyTank); 
                        break;
                    case Actions.Repair:
                        Console.WriteLine("Наш танк чинится!"); 
                        ourTank.Repair(); 
                        break;
                }

                // Наша победа. 
                if (enemyTank.Health <= 0)
                {
                    Console.WriteLine("Победа! Вражеский танк уничтожен!");
                    isEnd = true;
                }
                else
                {
                    // Генерация действия противника.
                    double ieAction = enemyRandom.NextDouble();
                    if (ieAction > 0.5)
                        enmAction = Actions.Shoot;
                    else
                        enmAction = Actions.Repair;

                    // Ход противника. 
                    switch (enmAction)
                    {
                        case Actions.Shoot:
                            Console.WriteLine("Танк врага стреляет!");
                            enemyTank.Shoot(ourTank);
                            break;
                        case Actions.Repair:
                            Console.WriteLine("Танк врага чинится!"); 
                            enemyTank.Repair();
                            break;
                    }

                    // Победа противника. 
                    if (ourTank.Health <= 0)
                    {
                        Console.WriteLine("Поражение! Наш танк уничтожен!");
                        isEnd = true; 
                    }
                }
                
            }
            Console.ReadLine(); 
        }
    }
}
