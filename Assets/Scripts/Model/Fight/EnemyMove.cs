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
    public int motionControll;
    public mobOptions mobOption;
    public float speed;
    private StandingEnemy standEnemy;
    private WalkingEnemy walkingEnemy;
    private FlyingEnemy flyingEnemy;
    public float distanceStopMove;
    public float agroDistance;
    private float _prevAgroDistance;
    public bool isAgro;
    private bool _side = true;
    private bool _side1 = true;
    public Vector2 vectorMove;

    public Transform mobPlatform;

    public float normalSpeed;
    public float startStopTime;
    public float stopTime;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        physic = GetComponent<Rigidbody2D>();
        standEnemy = new StandingEnemy();
        startPoint = transform.position.x + 5;
        endPoint = transform.position.x - 5;
        normalSpeed = speed;
        _prevAgroDistance = agroDistance;

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
            if (motionControll !=0 && agroDistance == _prevAgroDistance)
            {
                agroDistance += 1;
            }
            else if(motionControll == 0 && agroDistance != _prevAgroDistance)
            {
                agroDistance -= 1;
            }
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
        CheckEndPlatform();
        StopMoveEnemy();
        MovingEnemy();
    }

    private void StopMoveEnemy()
    {
        if (stopTime <= 0)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }
    }

    private void CheckEndPlatform()
    {
        if (transform.position.x >= mobPlatform.position.x + mobPlatform.localScale.x / 2 - 3 )
        {
            agroDistance = 0;
            Debug.Log(1);
        }
        else if (transform.position.x < startPoint - 4)
        {
            agroDistance = _prevAgroDistance;
            Debug.Log(_prevAgroDistance);
        }

        if (transform.position.x <= mobPlatform.position.x - mobPlatform.localScale.x / 2 + 3)
        {
            agroDistance = 0;
            Debug.Log(1);
        }
        else if (transform.position.x > startPoint + 4)
        {
            agroDistance = _prevAgroDistance;
            Debug.Log(_prevAgroDistance);
        }
    }

    public void StanEnemy()
    {
        stopTime = startStopTime;
    }

    private void MovingEnemy()
    {
        physic.velocity = new Vector2(motionControll * speed, 0);
        if (motionControll != 0)
        {
            if(motionControll == -1 && _side1 == true)
            {
                transform.Rotate(new Vector2(0, 180));
                _side1 = false;
                _side = true;
            }
            else if (motionControll == 1 && _side == true)
            {
                _side = false;
                _side1 = true;
                transform.Rotate(new Vector2(0, -180));
            }
        }
    }
}