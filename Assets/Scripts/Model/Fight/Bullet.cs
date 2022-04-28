using System;
using DefaultNamespace.Fight;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsSolid;
    public Transform playerPos;
    private Vector2 vectorBullet = new Vector2();
    private float playerposY;
    public GameObject bulletObject;
    /*public enum mobOptions
    {
        flyingMob,
        defoultMob
    }
    public mobOptions mobOption;*/

    private void Awake()
    {
        DefaultNamespace.Fight.Enemy.OnProjectileDamaged.AddListener((obj) =>
        {
            if (obj == gameObject)
                Destroy(gameObject);
        });
        
        DefaultNamespace.Fight.Enemy.OnEnemyPowerDamaged.AddListener((obj) =>
        {
            if (obj == gameObject)
                ChangeDirectionOnPowerDamaged();
        });
    }

    private void Update()
    {
        if (playerPos == null)
        {
            playerPos = GameObject.FindWithTag("Player").transform;
            vectorBullet = (playerPos.position - transform.position).normalized;
        }
        
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                StartCoroutine(hitInfo.collider.GetComponent<PlayerHealth>().TakeDamage());
            }
            Destroy(gameObject);
        }
        else if (lifeTime < 0)
        {
            Destroy(gameObject);
        }

        transform.Translate(vectorBullet * speed * Time.deltaTime);
        
        lifeTime -= Time.deltaTime;
    }

    private void ChangeDirectionOnPowerDamaged()
    {
        vectorBullet = PlayerAttack.AttackDirection;
    }
    
    
}
