using DefaultNamespace.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInTheWall : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform attackPos;
    public int attackDamage;
    public LayerMask playerMask;
    public float rangeAttackX;
    public float rangeAttackY;
    private EnemyMove enemyMove;
    private void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
    }

    private void Update()
    {
        if(enemyMove.stopTime <= 0)
        {
            Collider2D playerCollider = Physics2D.OverlapBox(attackPos.position, new Vector2(rangeAttackX, rangeAttackY), 0, playerMask);
            if (playerCollider)
            {
                PlayerHealth.OnHitTaken.Invoke(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(attackPos.position, new Vector2(rangeAttackX, rangeAttackY));
    }
}
