using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackShot : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttacl;
    public GameObject bullet;
    public Transform shotPoint;
    public Transform player;
    private EnemyMove enemyMove;
    public enum bulletOptions
    {
        defoultBullets,
        homingBullets
    }

    public bulletOptions bulletOption;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        enemyMove = GetComponent<EnemyMove>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (timeBtwAttack <= 0 && distanceToPlayer < enemyMove.agroDistance)
        {
            if (bulletOption == bulletOptions.defoultBullets)
            {
                Instantiate(bullet, shotPoint.position, transform.rotation);
                timeBtwAttack = startTimeBtwAttacl;
            }
            else if (bulletOption == bulletOptions.homingBullets)
            {
                Instantiate(bullet, shotPoint.position, bullet.transform.rotation);
                timeBtwAttack = startTimeBtwAttacl;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
}
