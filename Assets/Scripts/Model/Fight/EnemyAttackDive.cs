using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (timeBtwAttack > 0)
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
        backToStartPoint = false;
        Debug.Log(1);
        for (int i = 0; i < 1000; i++)
        {
            if (hitInfo)
            {
                break;
            }
            enemyMove.stopTime = 1;
            physic.AddForce(vectorPlayer );
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < 1000; i++)
        {
            enemyMove.stopTime = 1;
            if (transform.position.y > startPoint.position.y)
            {
                break;
            }

            physic.AddForce(-vectorPlayer );
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
