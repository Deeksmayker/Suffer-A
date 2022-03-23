using System;
using System.Collections;
using DefaultNamespace.View.SoundEffects;
using DefaultNamespace.View.VisualEffects;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Fight
{
    public class PlayerAttack : MonoBehaviour
    {
        public static UnityEvent OnEnemyHorizontalHit = new UnityEvent();
        public static UnityEvent OnEnemyVerticalHit = new UnityEvent();
        public static UnityEvent OnVerticalCanAttack = new UnityEvent();


        [SerializeField] protected LayerMask hitLayer;

        [SerializeField] protected float horizontalRangeX, horizontalRangeY;
        [SerializeField] protected float verticalRangeX, verticalRangeY;

        [SerializeField] private AudioSource preparedChargedAudio;
        [SerializeField] private Transform preparedChargedCenter;
        [SerializeField] private ParticleSystem preparedChargedParticle;
        
        [SerializeField] private Transform horizontalAttackCenter;
        [SerializeField] private Transform upAttackCenter, downAttackCenter;
        [SerializeField] protected GameObject horizontalAttackEffect;
        [SerializeField] protected GameObject upAttackEffect, downAttackEffect;

        protected bool CanAttack = true;

        [SerializeField] protected float attackCooldown = 0.4f;
        [SerializeField] private float chargedAttackTime;
        [SerializeField] private float maxChargeTime;
        [SerializeField] private float timeForKeyUp;
        private float _chargeDuration = 0f;

        public static UnityEvent<bool> OnHorizontalCanAttack = new UnityEvent<bool>();

        private Coroutine _chargeAttackCoroutine;
        
        private void Awake()
        {
            PlayerInGameInput.OnAttackKeyDown.AddListener(() => _chargeAttackCoroutine = StartCoroutine(ChargeAttack()));
            PlayerInGameInput.OnAttackKeyUp.AddListener(() =>
            {
                StopCoroutine(_chargeAttackCoroutine);
                DecideAttackDirection();
            });
        }

        private IEnumerator ChargeAttack()
        {
            var flag = true;
            
            while (_chargeDuration < maxChargeTime)
            {
                if (_chargeDuration >= chargedAttackTime - timeForKeyUp && flag)
                {
                    Debug.Log(1);
                    ParticlesEffects.StartParticle(preparedChargedParticle, preparedChargedCenter);
                    SoundEffects.PlayPreparedChargedAttackSound(preparedChargedAudio, preparedChargedCenter);
                    flag = false;
                }
                _chargeDuration += Time.deltaTime;
                yield return null;
            }

            DecideAttackDirection();
        }

        private void DecideAttackDirection()
        {
            if (PlayerInGameInput.VerticalRaw == 0 ||
                PlayerInGameInput.VerticalRaw == -1 && PlayerPreferences.IsGrounded)
            {
                StartCoroutine(Attack(horizontalRangeX, horizontalRangeY, horizontalAttackCenter,
                    horizontalAttackEffect, OnEnemyHorizontalHit));
            }
            else
            {
                var isUpAttack = PlayerInGameInput.VerticalRaw == 1;
                
                StartCoroutine(Attack(verticalRangeX, verticalRangeY,
                    isUpAttack ? upAttackCenter : downAttackCenter,
                    isUpAttack ? upAttackEffect : downAttackEffect,
                    OnEnemyVerticalHit));
            }
        }
        
        private IEnumerator Attack(float rangeX, float rangeY, Transform attackCenter, GameObject attackEffect, UnityEvent hitEvent)
        {
            if (_chargeDuration == 0 || !CanAttack)
            {
                _chargeDuration = 0;
                yield break;
            }

            var damage = PlayerPreferences.Damage;
            CanAttack = false;
            //OnHorizontalCanAttack.Invoke(false);

            CheckPowerAttack(ref damage, attackEffect);

            _chargeDuration = 0f;
            
            DealDamageInRange(attackCenter, rangeX, rangeY, hitEvent, damage);
           
            StartCoroutine(ShowAttackLine(attackEffect));
            yield return new WaitForSeconds(attackCooldown);
            CanAttack = true;

            //OnHorizontalCanAttack.Invoke(true);
        }

        private void DealDamageInRange(Transform attackCenter, float rangeX, float rangeY, UnityEvent hitEvent, int damage)
        {
            var enemiesInRange =
                Physics2D.OverlapBoxAll(attackCenter.position, new Vector2(rangeX, rangeY), 0, hitLayer);

            if (enemiesInRange.Length != 0)
            {
                hitEvent.Invoke();
            }

            foreach (var enemy in enemiesInRange)
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
        }

        private void CheckPowerAttack(ref int damage, GameObject attackEffect)
        {
            attackEffect.GetComponent<LineRenderer>().startColor = Color.green;
            
            if (Math.Abs(chargedAttackTime - _chargeDuration) <= timeForKeyUp)
            {
                damage *= 2;
                attackEffect.GetComponent<LineRenderer>().startColor = Color.red;
            }
        }
        
        private IEnumerator ShowAttackLine(GameObject attackEffect)
        {
            attackEffect.SetActive(true);
            yield return new WaitForSeconds(0.15f);
            attackEffect.SetActive(false);
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(horizontalAttackCenter.position, new Vector3(horizontalRangeX, horizontalRangeY, 1));
            Gizmos.DrawWireCube(upAttackCenter.position, new Vector3(verticalRangeX, verticalRangeY, 1));
            Gizmos.DrawWireCube(downAttackCenter.position, new Vector3(verticalRangeX, verticalRangeY, 1));
        }

    }
}