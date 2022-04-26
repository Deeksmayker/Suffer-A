using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInTheWall : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform attackPos;
    public int attackDamage;
    public float rangeAttack;
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
            Collider2D player = Physics2D.OverlapCircle(attackPos.position, rangeAttack, playerMask);
            //StartCoroutine(player.GetComponent<PlayerHealth>().TakeDamage());
        }
        else
        {
            enemyMove.stopTime -= Time.deltaTime;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(attackPos.position, new Vector2(rangeAttackX, rangeAttackY));
    }
}
