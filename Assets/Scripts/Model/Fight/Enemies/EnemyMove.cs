using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMove : MonoBehaviour
{
    public enum mobOptions
    {
        flyingMob,
        walkingMob,
        standingMob
    }
    private Rigidbody2D physic;
    public Transform player;
    public float startPoint;
    public float endPoint;
    private int motionControll;
    public mobOptions mobOption;
    public float speed;
    private StandingEnemy standEnemy;
    private WalkingEnemy walkingEnemy;
    private FlyingEnemy flyingEnemy;
    public float distanceStopMove;
    public float agroDistance;
    public bool isAgro;
    public Vector2 vectorMove;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").transform;
        
        physic = GetComponent<Rigidbody2D>();
        standEnemy = new StandingEnemy();
        startPoint = transform.position.x + 5;
        endPoint = transform.position.x - 5;

        if (mobOption == mobOptions.standingMob)
        {
            standEnemy = new StandingEnemy();
        }
        else if (mobOption == mobOptions.walkingMob)
        {
            walkingEnemy = new WalkingEnemy();
            motionControll = -1;
        }
        else if (mobOption == mobOptions.flyingMob)
        {
            flyingEnemy = new FlyingEnemy();
            motionControll = -1;
            transform.position = new Vector2(transform.position.x, 5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (mobOption == mobOptions.standingMob)
        {
            motionControll = standEnemy.GenerateStandingMob(distanceToPlayer, agroDistance, player.position.x, transform.position.x, distanceStopMove);
            vectorMove = new Vector2(motionControll, 0);
        }
        else if (mobOption == mobOptions.walkingMob)
        {
            motionControll = walkingEnemy.GenerateWalkingMob(distanceToPlayer, agroDistance, player.position.x, transform.position.x, distanceStopMove, startPoint, endPoint, motionControll);
            vectorMove = new Vector2(motionControll, 0);
        }
        else if (mobOption == mobOptions.flyingMob)
        {
            motionControll = flyingEnemy.GenerateFlyingMob(transform.position.x, startPoint, endPoint, motionControll);
            vectorMove = new Vector2(motionControll, -1);
        }
        if (distanceToPlayer < agroDistance)
        {
            isAgro = true;
        }
        else isAgro = false;

        MovingEnemy();
    }

    private void MovingEnemy()
    {
        physic.velocity = new Vector2(motionControll * speed, 0);
        if (motionControll != 0)
        {
            transform.localScale = new Vector2(motionControll * 1, 1);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            motionControll = 0;
        }
    }
}
