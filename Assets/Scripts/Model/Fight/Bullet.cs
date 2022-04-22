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
    public enum mobOptions
    {
        flyingMob,
        defoultMob
    }
    public mobOptions mobOption;

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);  
        if (hitInfo.collider != null)
        {
            Debug.Log(hitInfo);
            if (hitInfo.collider.CompareTag("Player"))
            {
                StartCoroutine(hitInfo.collider.GetComponent<PlayerHealth>().TakeDamage()); 
            }
            Destroy(gameObject);
        }
        else if(lifeTime < 0)
        {
            Destroy(gameObject);
        }
        if (mobOptions.defoultMob == mobOption)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else if(mobOptions.flyingMob == mobOption)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        lifeTime -= Time.deltaTime;
    }
}