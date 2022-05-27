using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BossAnimation : MonoBehaviour
{
    public const string isBaseAttack = "isBaseAttack";
    public const string isFloorAttack = "isFloorAttack";
    public const string isJerkAttack = "isJerkAttack";
    public const string isPrepareShotAttack = "isPrepareShotAttack";

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
