using System;
using UnityEngine;

namespace Players.Weapon
{
    class BlackWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private GameObject bulletObject;

        [SerializeField] private WeaponIDEnum weaponID;

        [SerializeField] private float maxFireCooldown;
        // [SerializeField] private readonly WeaponIDEnum weaponID;

        public WeaponIDEnum WeaponID
        {
            get { return weaponID; }
        }

        public float MaxFireCooldown => maxFireCooldown;

        private void Start()
        {
            // weaponTransform = GetComponent
        }

        public void Fire(Vector2 direction)
        {
            var bullet = Instantiate(bulletObject, transform.position, Quaternion.Euler(Vector3.zero));
        }
    }
}