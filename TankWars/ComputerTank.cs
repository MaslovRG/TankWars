using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWars
{
    // Класс Tank с особыми реализациями Shoot(), Repair() и BuyRounds(). 
    public class ComputerTank : ITank, IComputer
    {
        public int Armor { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int RoundsNum { get; set; }

        public ComputerTank(int nArmor, int nHealth, int nDamage)
        {
            Armor = nArmor;
            Health = nHealth;
            Damage = nDamage;
            RoundsNum = 6; 
        }

        /// <summary>
        /// Стреляет без промахов и критических ударов по танку противника (игнорируя броню)
        /// </summary>
        /// <param name="EnemyTank">Танк противника</param>
        /// <returns>Результат стрельбы</returns>
        public ShootResult Shoot(ITank EnemyTank)
        {
            ShootResult result = ShootResult.NoRounds;
            if (RoundsNum > 1)
            {
                result = ShootResult.Usual; 
                EnemyTank.Health -= Damage;
                RoundsNum--; 
            }
            return result; 
        }

        /// <summary>
        /// Чинится на величину брони. Игнорирует максимальное здоровье
        /// </summary>
        public void Repair()
        {
            Health += Armor; 
        }

        /// <summary>
        /// Добавляет пять патронов, если патронов нет. Иначе удваивает их количество
        /// </summary>
        public void BuyRounds()
        {
            if (RoundsNum == 0)
                RoundsNum = 5;
            else
                RoundsNum *= 2; 
        }

        /// <summary>
        /// Определяет действие, которое нужно совершить танку
        /// </summary>
        /// <param name="EnemyTank">Танк-противник</param>
        /// <returns>Действие, которое нужно совершить</returns>
        public Actions ComputerTurn(ITank EnemyTank)
        {
            // По-умолчанию, стреляем.
            Actions action = Actions.Shoot;

            // Если нас, вероятно, убивают на следующем ходу, то лечимся. 
            if (EnemyTank.Damage > Health)
                action = Actions.Repair;

            // Если патронов меньше 10 и нас не убивают, то покупаем патроны.
            if (action != Actions.Repair && RoundsNum < 10)
                action = Actions.Buy; 

            // Если мы убиваем на этом ходу и у нас есть патроны, то стреляем. 
            if (EnemyTank.Health < Damage && RoundsNum > 0)
                action = Actions.Shoot;

            return action; 
        }
    }        
}
