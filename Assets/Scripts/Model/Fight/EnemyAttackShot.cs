using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Fight;
using DefaultNamespace.TextStuff.JournalStuff;
using UnityEngine;

public class EnemyAttackShot : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttacl;
    public GameObject bullet;
    public Transform shotPoint;
    public Transform player;
    private EnemyMove enemyMove;
    private float timePeriodAttack;
    public float startTimePeriodAttacl;
    private float timePeriodMove;
    public float startTimePeriodMove;

    [SerializeField] private AudioSource audio;

    public enum bulletOptions
    {
        defoultBullets,
        homingBullets
    }

    public bulletOptions bulletOption;

    private void Start()
    {
        audio = Instantiate(audio, transform);
        
        player = GameObject.FindWithTag("Player").transform;
        enemyMove = GetComponent<EnemyMove>();
    }

    private void Update()
    {
        if (timePeriodAttack >= 0 )
        {
            ShotPlayer();
            enemyMove.StanEnemy();
            timePeriodAttack -= Time.deltaTime;
        }
        else if (timePeriodAttack < 0 && timePeriodMove >= 0)
        {
            timePeriodMove -= Time.deltaTime;
        }
        else
        {
            timePeriodAttack = startTimePeriodAttacl;
            timePeriodMove = startTimePeriodMove;
        }
    }

    private void ShotPlayer()
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
    
    private void OnDestroy()
    {
        if (GetComponent<Enemy>().health <= 0)
            JournalHandler.EnemiesKillCount["Head"]++;
    }
}
