using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class JerkAnimation : MonoBehaviour
{
    public const string isJerk = "Jerk";
    public const string isHit = "Hit";

    private Animator _animator;
    private EnemyAttackJerk enemyAttackJerk;
    private EnemyAttack enemyAttack;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        enemyAttackJerk = GetComponentInParent<EnemyAttackJerk>();
        enemyAttackJerk.OnJerk.AddListener(() => {
            _animator.SetTrigger(isJerk);
        });
        enemyAttack = GetComponentInParent<EnemyAttack>();
        enemyAttack.EnemyAttackAnimation.AddListener(() => {
        _animator.SetTrigger(isHit);
        });
    }
}
