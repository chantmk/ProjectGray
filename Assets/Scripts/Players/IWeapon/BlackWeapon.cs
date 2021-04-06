using System;
using UnityEngine;

namespace Players.IWeapon
{
    class BlackWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private GameObject bulletObject;
        private Transform weaponTransform;
        public int Weaponname { get ; }
        public float FireCooldown { get; set; }
        public float MaxFireCooldown { get; }

        private void Start()
        {
            weaponTransform = GetComponent
        }

        public void Fire()
        {
            var bullet = Instantiate(bulletObject, weaponTransform.position, Quaternion.Euler(Vector3.zero));
        }
    }
}