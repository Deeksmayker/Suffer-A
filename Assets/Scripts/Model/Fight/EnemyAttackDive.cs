using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
using Movement;
using UnityEngine.Events;

public class EnemyAttackDive : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask whatIsSolid;
    public Transform playerPos;
    private float timeBtwAttack;
    public float startTimeBtwAttacl;
    private Vector2 vectorPlayer = new Vector2();
    public Transform startPoint;
    private bool backToStartPoint;
    private bool hitInfo;
    private EnemyMove enemyMove;
    public Rigidbody2D physic;
    public UnityEvent OnAttacking = new UnityEvent();
    private void Start()
    {
        hitInfo = false;
        playerPos = GameObject.Find("Player").transform;
        vectorPlayer = playerPos.position - transform.position;
        timeBtwAttack = startTimeBtwAttacl;
        backToStartPoint = false;
        enemyMove = GetComponent<EnemyMove>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerPos.position);
        if (timeBtwAttack > 0 && distanceToPlayer < enemyMove.agroDistance)
        {
            timeBtwAttack -= Time.deltaTime;
            vectorPlayer = playerPos.position - transform.position;
            backToStartPoint = true;
        }
        else if (backToStartPoint)
        {
            StartCoroutine(Dive());
        }

    }

    public IEnumerator Dive()
    {
        OnAttacking.Invoke();
        backToStartPoint = false;
        for (int i = 0; i < 1000; i++)
        {
            if (hitInfo)
            {
                break;
            }
            enemyMove.stopTime = 1;
            physic.AddForce(vectorPlayer * speed );
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < 1000; i++)
        {
            enemyMove.stopTime = 1;
            if (transform.position.y > startPoint.position.y)
            {
                break;
            }

            physic.AddForce(-vectorPlayer * speed );
            yield return new WaitForSeconds(0.01f);
        }
        StopAllCoroutines();
        timeBtwAttack = startTimeBtwAttacl;
        hitInfo = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        hitInfo = true;
    }
}
