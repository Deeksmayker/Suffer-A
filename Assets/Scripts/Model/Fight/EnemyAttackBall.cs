using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBall : MonoBehaviour
{
    // Start is called before the first frame update
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
    private bool hitInfo;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
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
            if (hitInfo)
            {
                break;
            }    
            yield return new WaitForSeconds(0.01f);
            physic.AddForce(new Vector2(enemyMove.agroDistance * 2 * enemyMove.motionControll, 0));
        }

        if (hitInfo)
        {
            turnEnemy();
            for (int i = 0; i < 60; i++)
            {
                yield return new WaitForSeconds(0.01f);
                physic.AddForce(new Vector2(-enemyMove.agroDistance * 2 * enemyMove.motionControll, 0));
            }
            turnEnemy();
        }
        enemyMove.StanEnemy();
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(enemyMove.startStopTime / 100);
        }
        hitInfo = false;
        StopAllCoroutines();
        _startCaroutine = true;
    }

    private void turnEnemy()
    {
        if (transform.rotation.y < 0)
        {
            transform.Rotate(new Vector2(0, 180));
        }
        else
        {
            transform.Rotate(new Vector2(0, -180));
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(1);
        hitInfo = true;
    }
}
