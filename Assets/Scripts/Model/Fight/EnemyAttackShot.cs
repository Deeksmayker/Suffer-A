using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackShot : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttacl;
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
    }
}
