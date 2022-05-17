using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDive : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeBtwAttack;
    public float startTimeBtwAttacl;
    public Rigidbody2D physic;
    public int attackDamage;
    public Transform attackPos;
    public Transform player;
    public LayerMask playerMask;
    private bool _startCaroutine = true;
    private EnemyMove enemyMove;
    

    private void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (timeBtwAttack <= 0 && _startCaroutine && distanceToPlayer < enemyMove.agroDistance)
        {
            _startCaroutine = false;
            StartCoroutine(Jump());
            timeBtwAttack = startTimeBtwAttacl;
        }
        else if (_startCaroutine)
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public IEnumerator Jump()
    {
        enemyMove.StanEnemy();
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(enemyMove.startStopTime / 100);
        }
        enemyMove.StanEnemy();
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(0.01f);
            physic.AddForce(new Vector2(10 * enemyMove.motionControll, -10));
        }
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(0.01f);
            physic.AddForce(new Vector2(10 * enemyMove.motionControll, 10));
        }
        enemyMove.StanEnemy();
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(enemyMove.startStopTime / 100);
        }
        StopAllCoroutines();
        _startCaroutine = true;
    }
}
