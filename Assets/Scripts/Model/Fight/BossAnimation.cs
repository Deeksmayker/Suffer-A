using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BossAnimation : MonoBehaviour
{
    public const string isBaseAttack = "Hit";
    public const string isFloorAttack = "DownAbility";
    public const string isJerkAttack = "Jerk";
    public const string isPrepareShotAttack = "ProjectTileAbility";

    private Animator _animator;
    private ControllerBossAttack bossAttack;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        bossAttack = GetComponentInParent<ControllerBossAttack>();
        bossAttack.OnBaseAttack.AddListener(() => {
            _animator.SetTrigger(isBaseAttack);
        });
        bossAttack.OnFloorAttack.AddListener(() => {
            _animator.SetTrigger(isFloorAttack);
        });
        bossAttack.OnJerkAttack.AddListener(() => {
            _animator.SetTrigger(isJerkAttack);
        });
        bossAttack.OnPrepareShotAttack.AddListener(() => {
            _animator.SetTrigger(isPrepareShotAttack);
        });
    }
}
