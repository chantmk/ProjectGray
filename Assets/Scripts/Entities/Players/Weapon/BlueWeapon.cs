using System.Collections;
using UnityEngine;

namespace Players.Weapon
{
    class BlueWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private GameObject bulletObject;

        [SerializeField] private WeaponIDEnum weaponID;

        [SerializeField] private float maxFireCooldown;

        [SerializeField] private float maxScatterAngle;

        [SerializeField] private float bulletAmount;
        
        // [SerializeField] private readonly WeaponIDEnum weaponID;
        public AudioClip shootingSound;
        public float soundVolume = 1f;
        private AudioSource audioSrc;
        [SerializeField] private float timeBetweenBullet;

        public WeaponIDEnum WeaponID
        {
            get { return weaponID; }
        }

        public float MaxFireCooldown => maxFireCooldown;

        private void Start()
        {
            // weaponTransform = GetComponent
            audioSrc = GetComponent<AudioSource>();
        }

        public void Fire(Vector2 direction)
        {
            StartCoroutine(FireSequence(direction));

        }

        private IEnumerator FireSequence(Vector2 direction)
        {
            int sign = 1;
            audioSrc.PlayOneShot(shootingSound, soundVolume);
            for (int i = 0; i < bulletAmount; i++)
            {
                sign *= -1;
                var angle = sign * Random.Range(0f, maxScatterAngle);
                var bullet = Instantiate(bulletObject, transform.position, Quaternion.Euler(Vector3.zero));
                bullet.GetComponent<PlayerProjectile>().Shoot(Quaternion.AngleAxis(angle, Vector3.forward) * direction);
                
                yield return new WaitForSeconds(timeBetweenBullet);
            }
        }
    }
}