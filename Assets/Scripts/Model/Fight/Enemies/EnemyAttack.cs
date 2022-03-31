using DefaultNamespace.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeBtwAttack;
    public float startTimeBtwAttacl;

    public Transform attackPos;
    public int attackDamage;
    public float rangeAttack;
    public LayerMask playerMask;
    public GameObject bullet;
    public Transform shotPoint;

    private void Update()
    {
        if (timeBtwAttack <= 0)
        {
            Instantiate(bullet, shotPoint.position, transform.rotation);
            timeBtwAttack = startTimeBtwAttacl;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
        Collider2D player = Physics2D.OverlapCircle(attackPos.position, rangeAttack, playerMask);
        //StartCoroutine(player.GetComponent<PlayerHealth>().TakeDamage());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, rangeAttack); 
    }
}
