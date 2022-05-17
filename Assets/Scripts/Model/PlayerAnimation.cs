using System;
using DefaultNamespace.Fight;
using Movement;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour
    {
        public const string VelocityX = "velocityX";
        public const string VelocityY = "velocityY";
        public const string Grounded = "grounded";
                                  
        public const string SimpleHorizontal = "simpleHorizontal";
        public const string StrongHorizontal = "strongHorizontal";
        public const string FailedHorizontal = "failedHorizontal";

        public const string SimpleUp = "simpleUp";
        public const string StrongUp = "strongUp";
        public const string FailedUp = "failedUp";
        
        public const string SimpleDown = "simpleDown";
        public const string StrongDown = "strongDown";
        public const string FailedDown = "failedDown";
        
        public const string Roll = "roll";
        public const string AirJerk = "airJerk";
        public const string Damaged = "damaged";
        public const string TakeItem = "takeItem";
        public const string Healing = "healing";
        public const string TakedSword = "takedSword";
        public const string Horizontal = "horizontal";
        public const string Up = "up";
        public const string Down = "down";
        public const string Die = "die";
        

        private PlayerController _player;
        private Animator _animator;

        private void Awake()
        {
            _player = GetComponentInParent<PlayerController>();
            _animator = GetComponent<Animator>();
            
            PlayerInGameInput.OnHorizontalAbility.AddListener(() => _animator.SetTrigger(Horizontal));
            PlayerInGameInput.OnUpAbility.AddListener(() => _animator.SetTrigger(Up));
            PlayerInGameInput.OnDownAbility.AddListener(() => _animator.SetTrigger(Down));
            
            PlayerAttack.OnStrongHorizontal.AddListener(() => _animator.SetTrigger(StrongHorizontal));
            PlayerAttack.OnSimpleHorizontal.AddListener(() => _animator.SetTrigger(SimpleHorizontal));
            PlayerAttack.OnFailedHorizontal.AddListener(() => _animator.SetTrigger(FailedHorizontal));
            
            PlayerAttack.OnStrongUp.AddListener(() => _animator.SetTrigger(StrongUp));
            PlayerAttack.OnSimpleUp.AddListener(() => _animator.SetTrigger(SimpleUp));
            PlayerAttack.OnFailedUp.AddListener(() => _animator.SetTrigger(FailedUp));
            
            PlayerAttack.OnStrongDown.AddListener(() => _animator.SetTrigger(StrongDown));
            PlayerAttack.OnSimpleDown.AddListener(() => _animator.SetTrigger(SimpleDown));
            PlayerAttack.OnFailedDown.AddListener(() => _animator.SetTrigger(FailedDown));
            
            PlayerController.OnAirJerk.AddListener(() => _animator.SetTrigger(AirJerk));
            PlayerController.OnRoll.AddListener(() => _animator.SetTrigger(Roll));
            
            PlayerHealth.OnDamageTaken.AddListener((a) => _animator.SetTrigger(Damaged));
            PlayerHealth.OnHealStart.AddListener(() => _animator.SetBool(Healing, true));
            PlayerHealth.OnHealEnd.AddListener(() => _animator.SetBool(Healing, false));
            PlayerHealth.OnDie.AddListener(() => _animator.SetTrigger(Die));
            
            GlobalEvents.OnPickup.AddListener(() => _animator.SetTrigger(TakeItem));
            GlobalEvents.OnSwordPickup.AddListener(() => _animator.SetTrigger(TakedSword));
        }

        private void LateUpdate()
        {
            _animator.SetFloat(VelocityX, Math.Abs(_player.GetCurrentMove().x));
            _animator.SetFloat(VelocityY, _player.GetCurrentVelocity().y);
            _animator.SetBool(Grounded, PlayerPreferences.IsGrounded);
        }
    }
}