using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWars
{
    public class Tank
    {
        public int Armor;
        public int Health;
        public int Damage; 

        public Tank(int nArmor, int nHealth, int nDamage)
        {
            Armor = nArmor;
            Health = nHealth;
            Armor = nArmor; 
        }

        public void Shoot(Tank EnemyTank)
        {
            EnemyTank.Health += Armor - Damage; 
        }

        public void Repair()
        {
            Health += 5; 
        }
    }
}
