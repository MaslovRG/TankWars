using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWars
{
    enum Actions
    {
        Shoot = 1,
        Repair
    }

    public class Tank
    {
        public int Armor;
        public int Health;
        public int Damage; 

        public Tank(int nArmor, int nHealth, int nDamage)
        {
            Armor = nArmor;
            Health = nHealth;
            Damage = nDamage; 
        }

        public void Shoot(Tank EnemyTank)
        {
            EnemyTank.Health += EnemyTank.Armor - Damage; 
        }

        public void Repair()
        {
            Health += 5; 
        }
    }
}
