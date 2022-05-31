using System;
using DefaultNamespace.Fight;
using Movement;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Animator))]
    public class DiveAnimation : MonoBehaviour
    {
        public const string isAttackBat = "isAttacking";

        private Animator _animator;
        private EnemyAttackDive enemyAttackDive;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            enemyAttackDive = GetComponentInParent<EnemyAttackDive>();
            enemyAttackDive.OnAttacking.AddListener(() => {
                _animator.SetTrigger(isAttackBat);
            });
        }
    }
}