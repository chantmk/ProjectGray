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
        public AudioClip shootingSound;
        public float soundVolume = 1f;
        private AudioSource audioSrc;

        public WeaponIDEnum WeaponID
        {
            get { return weaponID; }
        }

        public float MaxFireCooldown => maxFireCooldown;

        private void Start()
        {
            // weaponTransform = GetComponent
            audioSrc = GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<AudioSource>();
        }

        public void Fire(Vector2 direction)
        {
            var bullet = Instantiate(bulletObject, transform.position, Quaternion.Euler(Vector3.zero));
            bullet.GetComponent<PlayerProjectile>().Shoot(direction);
            audioSrc.PlayOneShot(shootingSound, soundVolume);

        }
    }
}