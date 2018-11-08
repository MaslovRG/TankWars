using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWars
{
    public interface IComputer
    {
        Actions ComputerTurn(ITank EnemyTank); 
    }
}
