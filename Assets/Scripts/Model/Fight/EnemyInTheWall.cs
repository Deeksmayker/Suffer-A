using DefaultNamespace.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyInTheWall : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform attackPos;
    public int attackDamage;
    public LayerMask playerMask;
    public float rangeAttackX;
    public float rangeAttackY;
    private EnemyMove enemyMove;
    public Transform player;
    private bool isSleep = true;
    public UnityEvent OnNoAttack = new UnityEvent();
    public UnityEvent OnUnSleep = new UnityEvent();
    private void Start()
    {
        player = GameObject.Find("Player").transform;
        enemyMove = GetComponent<EnemyMove>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (enemyMove.stopTime <= 0 && distanceToPlayer < enemyMove.agroDistance)
        {
            if (isSleep)
            {
                isSleep = false;
                OnUnSleep.Invoke();
            }
            Collider2D playerCollider = Physics2D.OverlapBox(attackPos.position, new Vector2(rangeAttackX, rangeAttackY), 0, playerMask);
            if (playerCollider)
            {
                PlayerHealth.OnHitTaken.Invoke(attackDamage);
            }
        }
        else
        {
            OnNoAttack.Invoke();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(attackPos.position, new Vector2(rangeAttackX, rangeAttackY));
    }
}
