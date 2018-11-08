using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWars
{
    // Полностью идентичен классу Tank, 
    // но добавлено определение действия противника согласно условиям задания.
    public class SimpleComputerTank : Tank, IComputer
    {
        public SimpleComputerTank(int nArmor, int nHealth, int nDamage)
            : base(nArmor, nHealth, nDamage)
        {
            // Нет необходимости в дополнении базового конструктора.
        }

        /// <summary>
        /// Определяет, что нужно сделать танку, управляемому компьютером
        /// </summary>
        /// <returns>Возвращает действие</returns>
        public Actions ComputerTurn(ITank EnemyTank)
        {
            Actions action;
            double ieAction = Random.NextDouble();
            if (ieAction > 0.5 || Health == MaxHealth)
                action = Actions.Shoot;
            else
                action = Actions.Repair;

            if (action == Actions.Shoot && RoundsNum == 0)
                action = Actions.Buy;
            return action;
        }
    }
}
