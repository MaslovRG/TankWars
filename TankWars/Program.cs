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
            Tank ourTank = new Tank(10, 300, 20);
            Tank enemyTank = new Tank(0, 250, 10);

            Random enemyRandom = new Random();
            bool isEnd = false;
            bool IsParsed = false;
            Actions ourAction = Actions.Shoot;
            Actions enmAction = Actions.Shoot;
            ShootResult shootResult; 

            while (!isEnd)
            {
                Console.WriteLine("Новый ход!");
                Console.WriteLine($"Наш танк.        Жизни: {ourTank.Health} Количество патронов: {ourTank.RoundsNum}");
                Console.WriteLine($"Танк противника. Жизни: {enemyTank.Health} Количество патронов: {enemyTank.RoundsNum}");
                Console.WriteLine("Доступные действия: \n" +
                    " 1. Выстрел\n" +
                    " 2. Ремонт\n" +
                    " 3. Покупка патронов\n" + 
                    "Введите число, соотвествующее желаемому действию:");

                string[] stringActions = { "1", "2", "3" };

                // Считывание действия пользователя с проверкой правильности ввода. 
                string ioAction = "";
                do
                {
                    ioAction = Console.ReadLine();
                    if (!stringActions.Contains(ioAction))
                    {
                        Console.WriteLine("Введите допустимые числа!");
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
                        shootResult = ourTank.Shoot(enemyTank);
                        switch (shootResult)
                        {
                            case ShootResult.Usual:
                                Console.WriteLine("Противнику нанесён стандартный урон.");
                                break;
                            case ShootResult.Critical:
                                Console.WriteLine("Критическое попадание! Танк нанёс на 20% больше урона."); 
                                break;
                            case ShootResult.Miss:
                                Console.WriteLine("Промах...");
                                break;
                            case ShootResult.NoRounds:
                                Console.WriteLine("Выстрелить не удалось. Купите патроны!");
                                break; 
                        }
                        break; 
                    case Actions.Repair:                         
                        ourTank.Repair();
                        Console.WriteLine("Наш танк починился!");
                        break;
                    case Actions.Buy:
                        ourTank.BuyRounds(); 
                        Console.WriteLine("Наш танк купил патроны!"); 
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
                    if (ieAction > 0.5 || enemyTank.Health == enemyTank.MaxHealth)
                        enmAction = Actions.Shoot;
                    else
                        enmAction = Actions.Repair;

                    // Ход противника. 
                    switch (enmAction)
                    {
                        case Actions.Shoot:
                            shootResult = enemyTank.Shoot(ourTank);
                            switch (shootResult)
                            {
                                case ShootResult.NoRounds:
                                    enemyTank.BuyRounds();
                                    Console.WriteLine("Враг купил патроны.");
                                    break;

                                case ShootResult.Usual:
                                    Console.WriteLine("Противник стреляет!"); 
                                    Console.WriteLine("Нам нанесён стандартный урон.");
                                    break;
                                case ShootResult.Critical:
                                    Console.WriteLine("Противник стреляет!");
                                    Console.WriteLine("Критическое попадание! Противник нанёс 20% больше урона.");
                                    break;
                                case ShootResult.Miss:
                                    Console.WriteLine("Противник стреляет!");
                                    Console.WriteLine("Противник промахнулся.");
                                    break;                                
                            }
                            break;
                        case Actions.Repair:                            
                            enemyTank.Repair();
                            Console.WriteLine("Танк врага починился!");
                            break;
                    }

                    // Победа противника. 
                    if (ourTank.Health <= 0)
                    {
                        Console.WriteLine("Поражение! Наш танк уничтожен!");
                        isEnd = true; 
                    }
                }
                // Отступ перед новым ходом. 
                Console.WriteLine("\n"); 
            }

            Console.WriteLine("Нажмите Enter, чтобы завершить работу программы..."); 
            Console.ReadLine(); 
        }
    }
}
