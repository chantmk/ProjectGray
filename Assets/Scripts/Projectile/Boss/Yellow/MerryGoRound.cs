using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class MerryGoRound : EnemyProjectile
{
    [SerializeField]
    private float horseX;
    [SerializeField]
    private float horseY;
    [SerializeField]
    private GameObject horse;
    private BossAggroEnum bossStatus;

    private EnemyStats horseRight;
    private EnemyStats horseLeft;

    public override void Start()
    {
        base.Start();
        spawnHorse(bossStatus);
    }

    public override void Update()
    {
        if (CheckHorseAlive())
        {
            Execute();
        }
    }

    public bool CheckHorseAlive()
    {
        if (horseLeft != null && horseRight != null)
        {
            return horseLeft.Status == StatusEnum.Dead && horseRight.Status == StatusEnum.Dead;
        }
        return false;
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
        GameObject horseRightObject = Instantiate(horse, transform.position + new Vector3(horseX, horseY), Quaternion.Euler(Vector3.zero));
        horseRightObject.GetComponent<MerryGoRoundHorseMovement>().SetBossStatus(bossStatus);
        horseRight = horseRightObject.GetComponent<EnemyStats>();
        GameObject horseLeftObject = Instantiate(horse, transform.position + new Vector3(-horseX, horseY), Quaternion.Euler(Vector3.zero));
        horseLeftObject.GetComponent<MerryGoRoundHorseMovement>().SetBossStatus(bossStatus);
        horseLeft = horseLeftObject.GetComponent<EnemyStats>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position + new Vector3(horseX, horseY), 0.3f);
        Gizmos.DrawSphere(transform.position + new Vector3(-horseX, horseY), 0.3f);
    }
}
