using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMove : MonoBehaviour
{
    private Rigidbody2D physic;

    public Transform player;

    private Enemy enemyesSpeed;

    public float agroDistance;

    // Start is called before the first frame update
    void Start()
    {
        physic = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if(distanceToPlayer < agroDistance) 
        {
            StartHunting();
        }
        else if(distanceToPlayer > agroDistance) 
        {
            StopHunting();
        }
    }

    void StartHunting()
    {
        if(player.position.x < transform.position.x) 
        {
            physic.velocity = new Vector2(-enemyesSpeed.speed, 0);
            transform.localScale = new Vector2(5, 5);
        }
        else if(player.position.x > transform.position.x) 
        {
            physic.velocity = new Vector2(enemyesSpeed.speed, 0);
            transform.localScale = new Vector2(-5, 5);
        }
    }

    void StopHunting()
    {
        physic.velocity = new Vector2(0, 0);
    }
}
