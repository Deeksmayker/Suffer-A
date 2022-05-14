using System.Collections;
using DefaultNamespace.Fight;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackJerk : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    private float timeBtwAttack;
    public float startTimeBtwAttacl;
    public Rigidbody2D physic;
    public int attackDamage;
    public Transform attackPos;
    public Transform player;
    public float rangeAttackX;
    public float rangeAttackY;
    public LayerMask playerMask;
    private bool _startCaroutine = true;
    private EnemyMove enemyMove;
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        enemyMove = GetComponent<EnemyMove>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (timeBtwAttack <= 0 && _startCaroutine && distanceToPlayer < enemyMove.agroDistance)
        {
            _startCaroutine = false;
            StartCoroutine(Jerk());
            timeBtwAttack = startTimeBtwAttacl;
        }
        else if (_startCaroutine)
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public IEnumerator Jerk()
    {
        enemyMove.StanEnemy();
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(enemyMove.startStopTime / 100);
        }

        enemyMove.speed = enemyMove.normalSpeed;
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(0.01f);
            physic.AddForce(new Vector2(enemyMove.agroDistance * 2 * enemyMove.motionControll, 0));
        }
        Collider2D playerCollider = Physics2D.OverlapBox(attackPos.position, new Vector2(rangeAttackX, rangeAttackY), 0, playerMask);
        if (playerCollider)
        {
            PlayerHealth.OnHitTaken.Invoke(attackDamage);
        }
        enemyMove.StanEnemy();
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(enemyMove.startStopTime / 100);
        }
        StopAllCoroutines();
        _startCaroutine = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(attackPos.position, new Vector2(rangeAttackX, rangeAttackY));
    }
}
