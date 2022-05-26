using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BallAnimation : MonoBehaviour
{
    public const string isEndRolling = "isEndRolling";
    public const string isPrepare = "isPrepare";

    private Animator _animator;
    private EnemyAttackBall enemyAttackBall;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        enemyAttackBall = GetComponentInParent<EnemyAttackBall>();
        enemyAttackBall.OnPrepare.AddListener(() => {
            _animator.SetTrigger(isPrepare);
            Debug.Log(1);
        });
        enemyAttackBall.OnEndRolling.AddListener(() => {
            _animator.SetTrigger(isEndRolling);
            Debug.Log(2);
        });
    }
}