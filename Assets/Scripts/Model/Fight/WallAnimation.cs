using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WallAnimation : MonoBehaviour
{
    public const string isUnSleep = "isUnSleep";
    public const string isNoAttack = "isNoAttack";

    private Animator _animator;
    private EnemyInTheWall enemyWall;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        enemyWall = GetComponentInParent<EnemyInTheWall>();
        enemyWall.OnNoAttack.AddListener(() => {
            _animator.SetTrigger(isNoAttack);
        });
        enemyWall.OnUnSleep.AddListener(() => {
            _animator.SetTrigger(isUnSleep);
        });
    }
}
