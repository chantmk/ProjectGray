using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChange : EnemyChase
{
    protected override void chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, enemy.Speed * Time.deltaTime);
    }
}
