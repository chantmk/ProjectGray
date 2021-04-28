using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerryGoRound : Projectile
{
    [SerializeField]
    private Vector3 horseRadius;
    [SerializeField]
    private GameObject horse;
    private BossStatus bossStatus;
    public override void Start()
    {
        base.Start();
        spawnHorse(bossStatus);
    }

    public void SetBossStatus(BossStatus status)
    {
        bossStatus = status;
    }
    protected override void Attack(GameObject target)
    {
        target.GetComponent<CharacterStats>().TakeDamage(damage);
    }

    private void spawnHorse(BossStatus bossStatus)
    {
        GameObject horse1 = Instantiate(horse, transform.position, Quaternion.Euler(Vector3.zero));
        horse1.GetComponent<MerryGoRoundHorseMovement>().SetBossStatus(bossStatus);
        GameObject horse2 = Instantiate(horse, transform.position, Quaternion.Euler(Vector3.zero));
        horse2.GetComponent<MerryGoRoundHorseMovement>().SetBossStatus(bossStatus);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position + horseRadius, 0.3f);
    }
}
