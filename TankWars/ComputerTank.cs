using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWars
{
    public class ComputerTank : Tank, IComputer
    {
        public ComputerTank(int nArmor, int nHealth, int nDamage) 
            : base(nArmor, nHealth, nDamage)
        {
            // Нет необходимости в дополнении базового конструктора.
        }

        /// <summary>
        /// Определяет, что нужно сделать танку, управляемому компьютером
        /// </summary>
        /// <returns></returns>
        public Actions ComputerTurn()
        {
            Actions action = Actions.Shoot;
            if (Health < 0.1 * MaxHealth)
                action = Actions.Repair;
            if (action == Actions.Shoot && RoundsNum == 0)
                action = Actions.Buy;
            return action; 
        }
    }
}
