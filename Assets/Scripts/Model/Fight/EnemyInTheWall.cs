using DefaultNamespace.Fight;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.TextStuff.JournalStuff;
using UnityEngine;
using UnityEngine.Events;

public class EnemyInTheWall : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform attackPos;
    public int attackDamage;
    public LayerMask playerMask;
    public float rangeAttackX;
    public float rangeAttackY;
    private EnemyMove enemyMove;
    public Transform player;
    private bool isSleep = true;
    public UnityEvent OnNoAttack = new UnityEvent();
    public UnityEvent OnUnSleep = new UnityEvent();

    private ParticleSystem _particles;

    private void Start()
    {
        _particles = GetComponentInChildren<ParticleSystem>();
        _particles.Play();
        player = GameObject.FindWithTag("Player").transform;
        enemyMove = GetComponent<EnemyMove>();
        
        enemyMove.OnStun.AddListener(() =>
        {
            StopAllCoroutines();
            StartCoroutine(Stun());
        });
    }

    private bool _inStun;
    
    private void Update()
    {
        if (_inStun)
            return;
        Collider2D playerCollider = Physics2D.OverlapBox(attackPos.position, new Vector2(rangeAttackX, rangeAttackY), 0, playerMask);
        if (playerCollider)
        {
            PlayerHealth.OnHitTaken.Invoke(attackDamage);
        }
    }

    private IEnumerator Stun()
    {
        _inStun = true;
        _particles.Stop();
        yield return new WaitForSeconds(2);
        _particles.Play();

        yield return new WaitForSeconds(4);
        _inStun = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(attackPos.position, new Vector2(rangeAttackX, rangeAttackY));
    }
    
    private void OnDestroy()
    {
        if (GetComponent<Enemy>().health <= 0)
            JournalHandler.EnemiesKillCount["Wall"]++;
    }
}
