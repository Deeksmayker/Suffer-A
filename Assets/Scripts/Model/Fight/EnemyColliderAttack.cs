using DefaultNamespace.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform attackPos;
    public int attackDamage;
    public float rangeAttackX;
    public float rangeAttackY;
    public LayerMask playerMask;

    private void Update()
    {
        Collider2D player = Physics2D.OverlapBox(attackPos.position, new Vector2(rangeAttackX, rangeAttackY), 0, playerMask);
        if (player)
        {
            PlayerHealth.OnHitTaken.Invoke(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(attackPos.position, new Vector2(rangeAttackX, rangeAttackY));
    }
}
