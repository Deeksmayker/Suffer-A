using DefaultNamespace.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform attackPos;
    public int attackDamage;
    public float rangeAttackX;
    public float rangeAttackY;
    public LayerMask playerMask;
    private float timeBtwAttack;
    public float startTimeBtwAttacl;

    private void Update()
    {
        if (timeBtwAttack < 0)
        {
            Collider2D player = Physics2D.OverlapBox(attackPos.position, new Vector2(rangeAttackX, rangeAttackY), 0, playerMask);
            if (player)
            {
                PlayerHealth.OnHitTaken.Invoke(attackDamage);
            }
            timeBtwAttack = startTimeBtwAttacl;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(attackPos.position, new Vector2(rangeAttackX, rangeAttackY));
    }
}
