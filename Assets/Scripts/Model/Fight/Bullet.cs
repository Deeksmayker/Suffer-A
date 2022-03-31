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
    public int directionX;
    public int directionY;

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
        else if(lifeTime < 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(new Vector2(directionX, directionY) * speed * Time.deltaTime);
        lifeTime -= Time.deltaTime;
    }
}
