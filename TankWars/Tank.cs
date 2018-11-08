using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWars
{
    public class Tank : ITank
    {
        public int Armor { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int RoundsNum { get; set; }
        public readonly int MaxHealth;
        protected readonly Random Random; 

        /// <summary>
        /// Инициализирует класс Tank
        /// </summary>
        /// <param name="nArmor">Значение брони</param>
        /// <param name="nHealth">Значение здоровья</param>
        /// <param name="nDamage">Значение урона</param>        
        public Tank(int nArmor, int nHealth, int nDamage)
        {
            Armor = nArmor;
            Health = nHealth;
            MaxHealth = nHealth; 
            Damage = nDamage;
            RoundsNum = 5;
            // random для определения критических попаданий и промахов. 
            Random = new Random(); 
        }

        /// <summary>
        /// Стреляет по другому танку (если броня больше урона, то жизни не меняются)
        /// </summary>
        /// <param name="EnemyTank">Объект, описывающий танк, в который стреляют</param>
        /// <returns>Возвращает результат выстрела</returns>        
        public ShootResult Shoot(Tank EnemyTank)
        {
            ShootResult result = ShootResult.NoRounds;
            if (RoundsNum != 0)
            {
                result = ShootResult.Usual; 
                double SR = Random.NextDouble();
                int DamageInThisShoot = Damage; 
                // Проверяем промах (20%)
                if (SR <= 0.2)
                    result = ShootResult.Miss; 
                // Проверяем критический удар (10%)
                if (SR >= 0.9)
                {
                    result = ShootResult.Critical;
                    DamageInThisShoot += (int)Math.Round(0.2 * Damage);
                }
                // Если не промах, уменьшаем число жизней
                if (result != ShootResult.Miss)
                {
                    int DamageWithoutArmor = DamageInThisShoot - EnemyTank.Armor;
                    // Если броня больше урона, то урон не наносится, но и здоровье не добавляется
                    if (DamageWithoutArmor < 0)
                        DamageWithoutArmor = 0;
                    EnemyTank.Health -= DamageWithoutArmor; 
                }
                RoundsNum--;
            }
            return result;             
        }

        /// <summary>
        /// Чинится (не больше максимального значения)
        /// </summary>
        public void Repair()
        {
            Health += 5;
            if (Health > MaxHealth)
                Health = MaxHealth; 
        }

        /// <summary>
        /// Закупает патроны
        /// </summary>
        public void BuyRounds()
        {
            RoundsNum += 4; 
        }
    }
}
