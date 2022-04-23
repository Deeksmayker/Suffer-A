using DefaultNamespace.Fight;
using System.Collections;
using System.Collections.Generic;
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
    public enum mobOptions
    {
        flyingMob,
        defoultMob
    }
    public mobOptions mobOption;

    private void Start()
    {
        playerPos = GameObject.Find("Player").transform;
        vectorBullet = playerPos.position - transform.position;
    }

    private void Update()
    {
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
        Debug.Log(playerPos.position.x);
        if (mobOptions.defoultMob == mobOption)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime * 10);
        }
        else if (mobOptions.flyingMob == mobOption)
        {
            transform.Translate(vectorBullet * speed * Time.deltaTime);
        }
        lifeTime -= Time.deltaTime;
    }
}
