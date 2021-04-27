using UnityEngine;

namespace Players.Weapon
{
    interface IWeapon
    {
        WeaponIDEnum WeaponID { get; }
        float MaxFireCooldown { get; }

        void Fire(Vector2 direction);
    }
}