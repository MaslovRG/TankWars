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
            Tank ourTank = new Tank(5, 300, 20);
            SimpleComputerTank enemyTank = new SimpleComputerTank(10, 250, 10);

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

                // Считывание действия пользователя с проверкой правильности ввода. 
                string ioAction;
                do
                {
                    ioAction = Console.ReadLine();
                    IsParsed = Enum.TryParse(ioAction, out ourAction);
                    if (!IsParsed || (int)ourAction < 1 || (int)ourAction > 3)
                    {
                        Console.WriteLine("Введите допустимое число");
                        IsParsed = false; 
                    }
                }
                while (!IsParsed);

                Console.WriteLine(); 
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
                    Console.WriteLine(); 
                    // Генерация действия противника.
                    enmAction = enemyTank.ComputerTurn(ourTank); 

                    // Ход противника. 
                    switch (enmAction)
                    {
                        case Actions.Shoot:
                            Console.WriteLine("Противник стреляет!");
                            shootResult = enemyTank.Shoot(ourTank);
                            switch (shootResult)
                            {                                
                                case ShootResult.Usual:
                                    Console.WriteLine("Нам нанесён стандартный урон.");
                                    break;
                                case ShootResult.Critical:
                                    Console.WriteLine("Критическое попадание! Противник нанёс на 20% больше урона.");
                                    break;
                                case ShootResult.Miss:
                                    Console.WriteLine("Противник промахнулся...");
                                    break;
                                case ShootResult.NoRounds:
                                    Console.WriteLine("Противник не смог выстрелить, так как у него нет патронов.");
                                    break;
                            }
                            break;
                        case Actions.Repair:                            
                            enemyTank.Repair();
                            Console.WriteLine("Танк врага починился!");
                            break;
                        case Actions.Buy:
                            enemyTank.BuyRounds();
                            Console.WriteLine("Танк врага купил патроны!"); 
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
