using System;
using DefaultNamespace.Fight;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;


public class BulletBoss : MonoBehaviour
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
    public enum mobOptions
    {
        left,
        right,
        leftDown,
        rightDown,
        leftTop,
        rightTop
    }
    public mobOptions mobOption;

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
                PlayerHealth.OnHitTaken.Invoke(3);
            }
            Destroy(gameObject);
        }
        else if (lifeTime < 0)
        {
            Destroy(gameObject);
        }

        switch (mobOption)
        {
            case mobOptions.left:
                {
                    transform.Translate(new Vector2(-2,0) * speed * Time.deltaTime);
                    break;
                }
            case mobOptions.right:
                {
                    transform.Translate(new Vector2(2, 0) * speed * Time.deltaTime);
                    break;
                }
            case mobOptions.leftDown:
                {
                    transform.Translate(new Vector2(-2, -1) * speed * Time.deltaTime);
                    break;
                }
            case mobOptions.leftTop:
                {
                    transform.Translate(new Vector2(-2, 1) * speed * Time.deltaTime);
                    break;
                }
            case mobOptions.rightDown:
                {
                    transform.Translate(new Vector2(2, -1) * speed * Time.deltaTime);
                    break;
                }
            case mobOptions.rightTop:
                {
                    transform.Translate(new Vector2(2, 1) * speed * Time.deltaTime);
                    break;
                }
            default:
                break;
        }

        transform.Translate(vectorBullet * speed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
    }

    private void ChangeDirectionOnPowerDamaged()
    {
        vectorBullet = PlayerAttack.AttackDirection;
    }
}
