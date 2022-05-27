using System.Collections;
using DefaultNamespace.Fight;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ControllerBossAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D physic;
    public int attackDamageBaseAttack;
    public int attackDamageShotBall;
    public int attackDamageJerk;
    public int attackDamageFloorExplosion;
    public Transform attackPosFloorExplosion;
    public Transform attackPosShotBall;
    public Transform attackPosBaseAttack;
    public Transform player;
    public float rangeAttackFloorX;
    public float rangeAttackFloorY;
    public float rangeAttackBaseAttackX;
    public float rangeAttackBaseAttackY;
    public float speedJerk;
    public LayerMask playerMask;
    private bool _startCaroutine = true;
    private EnemyMove enemyMove;
    public GameObject[] bullets;
    public UnityEvent OnBaseAttack = new UnityEvent();
    public UnityEvent OnFloorAttack = new UnityEvent();
    public UnityEvent OnJerkAttack = new UnityEvent();
    public UnityEvent OnPrepareShotAttack = new UnityEvent();
    private void Start()
    {
        player = GameObject.Find("Player").transform;
        enemyMove = GetComponent<EnemyMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_startCaroutine)
        {
            _startCaroutine = false;
            StartCoroutine(AttackBossController());
        }
    }

    public IEnumerator AttackBossController()
    {
        enemyMove.StanEnemy();
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(enemyMove.startStopTime / 100);
        }

        OnBaseAttack.Invoke();
        Collider2D playerCollider = Physics2D.OverlapBox(attackPosBaseAttack.position, new Vector2(rangeAttackBaseAttackX, rangeAttackBaseAttackY), 0, playerMask);
        if (playerCollider)
        {
            PlayerHealth.OnHitTaken.Invoke(attackDamageBaseAttack);
        }

        yield return new WaitForSeconds(5f);
        OnFloorAttack.Invoke();
        for (int i = 0; i < 60; i++)
        {
            enemyMove.StanEnemy();
            playerCollider = Physics2D.OverlapBox(attackPosFloorExplosion.position, new Vector2(rangeAttackFloorX, rangeAttackFloorY), 0, playerMask);
            if (playerCollider)
            {
                PlayerHealth.OnHitTaken.Invoke(attackDamageFloorExplosion);
            }
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(5f);
        enemyMove.StanEnemy();
        yield return new WaitForSeconds(enemyMove.startStopTime);
        OnJerkAttack.Invoke();
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(0.01f);
            physic.AddForce(new Vector2(enemyMove.agroDistance * 2 * enemyMove.motionControll, 0) * speedJerk);
        }

        yield return new WaitForSeconds(5f);
        OnPrepareShotAttack.Invoke();
        enemyMove.StanEnemy();
        yield return new WaitForSeconds(enemyMove.startStopTime);
        transform.position = new Vector2(transform.position.x, transform.position.y + 5);
        for (int i = 0; i < bullets.Length; i++)
        {
            enemyMove.StanEnemy();
            Instantiate(bullets[i], attackPosShotBall.position, transform.rotation);
        }

        for (int i = 0; i < 5; i++)
        {
            enemyMove.StanEnemy();
            yield return new WaitForSeconds(enemyMove.startStopTime);
        }

        transform.position = new Vector2(transform.position.x, transform.position.y - 5);
        _startCaroutine = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(attackPosFloorExplosion.position, new Vector2(rangeAttackFloorX, rangeAttackFloorY));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPosBaseAttack.position, new Vector2(rangeAttackBaseAttackX, rangeAttackBaseAttackY));
    }
}
