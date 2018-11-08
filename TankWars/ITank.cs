using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWars
{
    // Возможные действия танка
    public enum Actions
    {
        Shoot = 1,
        Repair,
        Buy
    }

    // Результаты выстрела танка
    public enum ShootResult
    {
        Usual = 1,
        Critical,
        Miss,
        NoRounds
    }

    public interface ITank
    {
        int Armor { get; set; }
        int Health { get; set; }
        int Damage { get; set; }
        int RoundsNum { get; set; }

        ShootResult Shoot(Tank EnemyTank);
        void Repair();
        void BuyRounds(); 
    }
}
