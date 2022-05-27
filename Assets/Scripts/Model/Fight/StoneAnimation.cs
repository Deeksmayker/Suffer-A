using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StoneAnimation : MonoBehaviour
{
    public const string isUnSleep = "isUnSleep";
    public const string isWalking = "isWalking";

    private Animator _animator;
    private EnemyMove enemyMove;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        enemyMove = GetComponentInParent<EnemyMove>();
        enemyMove.OnUnSleep.AddListener(() => {
            _animator.SetTrigger(isUnSleep);
        });
        enemyMove.OnWalkStone.AddListener(() => {
            _animator.SetTrigger(isWalking);
        });
    }
}
