using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class MerryGoRound : EnemyProjectile
{
    [SerializeField]
    private Vector3 horseRadius;
    [SerializeField]
    private GameObject horse;
    private BossAggroEnum bossStatus;
    public override void Start()
    {
        base.Start();
        spawnHorse(bossStatus);
    }

    public void SetBossStatus(BossAggroEnum status)
    {
        bossStatus = status;
    }
    protected override void Attack(GameObject target)
    {
        target.GetComponent<CharacterStats>().TakeDamage(damage);
    }

    private void spawnHorse(BossAggroEnum bossStatus)
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
