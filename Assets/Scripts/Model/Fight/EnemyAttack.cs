using DefaultNamespace.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform attackPos;
    public int attackDamage;
    public float rangeAttack;
    public LayerMask playerMask;

    private void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(attackPos.position, rangeAttack, playerMask);
        //StartCoroutine(player.GetComponent<PlayerHealth>().TakeDamage());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, rangeAttack); 
    }
}
