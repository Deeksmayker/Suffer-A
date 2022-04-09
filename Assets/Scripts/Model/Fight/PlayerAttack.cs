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
        public static UnityEvent<bool> OnEnemyHorizontalHit = new UnityEvent<bool>();
        public static UnityEvent<bool> OnEnemyDownHit = new UnityEvent<bool>();
        public static UnityEvent<bool> OnEnemyUpHit = new UnityEvent<bool>();
        public static UnityEvent OnVerticalCanAttack = new UnityEvent();


        [SerializeField] protected LayerMask hitLayer;

        [SerializeField] protected float horizontalRangeX, horizontalRangeY;
        [SerializeField] protected float verticalRangeX, verticalRangeY;

        [SerializeField] private AudioSource chargedPreparingAudio;
        [SerializeField] private AudioSource preparedChargedAudio;
        [SerializeField] private Transform preparedChargedCenter;
        [SerializeField] private ParticleSystem preparedChargedParticle;
        
        [SerializeField] private Transform horizontalAttackCenter;
        [SerializeField] private Transform upAttackCenter, downAttackCenter;
        [SerializeField] protected GameObject horizontalAttackLine, bigHorizontalAttackLine;
        [SerializeField] protected GameObject upAttackLine, downAttackLine, bigUpAttackLine, bigDownAttackLine;

        private bool _canAttack = true;
        [NonSerialized] public bool PowerAttack = false;

        [SerializeField] protected float attackCooldown = 0.4f;
        [SerializeField] private float chargedAttackTime;
        [SerializeField] private float maxChargeTime;
        [SerializeField] private float timeForKeyUp;
        private float _chargeDuration = 0f;

        public static UnityEvent<bool> OnHorizontalCanAttack = new UnityEvent<bool>();

        private Coroutine _chargeAttackCoroutine;
        private AudioSource _preparingSound;
        
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
            _preparingSound = Instantiate(chargedPreparingAudio, preparedChargedCenter);
            
            var flag = true;
            
            while (_chargeDuration < maxChargeTime)
            {
                if (_chargeDuration >= chargedAttackTime && _chargeDuration - chargedAttackTime <= timeForKeyUp && flag)
                {
                    ParticlesEffects.StartParticle(preparedChargedParticle, preparedChargedCenter);
                    Instantiate(preparedChargedAudio, preparedChargedCenter);
                    flag = false;
                }
                _chargeDuration += Time.deltaTime;
                yield return null;
            }

            DecideAttackDirection();
        }

        private void DecideAttackDirection()
        {
            if (_chargeDuration < chargedAttackTime)
                Destroy(_preparingSound);
            
            if (PlayerInGameInput.VerticalRaw == 0 ||
                PlayerInGameInput.VerticalRaw == -1 && PlayerPreferences.IsGrounded)
            {
                var x = PowerAttack ? horizontalRangeX * 2 : horizontalRangeX;
                var y = PowerAttack ? horizontalRangeY * 2 : horizontalRangeY;
                var line = PowerAttack ? bigHorizontalAttackLine : horizontalAttackLine;
                StartCoroutine(Attack(x, y, horizontalAttackCenter,
                    line, OnEnemyHorizontalHit));
            }
            else
            {
                var isUpAttack = PlayerInGameInput.VerticalRaw == 1;
                var x = PowerAttack ? verticalRangeX * 2 : verticalRangeX;
                var y = PowerAttack ? verticalRangeY * 2 : verticalRangeY;
                var line = PowerAttack
                    ? (isUpAttack ? bigUpAttackLine : bigDownAttackLine)
                    : (isUpAttack ? upAttackLine : downAttackLine);
                StartCoroutine(Attack(x, y,
                    isUpAttack ? upAttackCenter : downAttackCenter,
                    line,
                    isUpAttack ? OnEnemyUpHit : OnEnemyDownHit));
            }
        }
        
        private IEnumerator Attack(float rangeX, float rangeY, Transform attackCenter, GameObject attackEffect, UnityEvent<bool> hitEvent)
        {
            if (_chargeDuration == 0 || !_canAttack)
            {
                _chargeDuration = 0;
                yield break;
            }

            var damage = PowerAttack ? PlayerPreferences.HitDamage * 2 : PlayerPreferences.HitDamage;
            _canAttack = false;
            //OnHorizontalCanAttack.Invoke(false);

            CheckStrongAttack(ref damage, attackEffect);

            _chargeDuration = 0f;
            
            DealDamageInRange(attackCenter, rangeX, rangeY, hitEvent, damage);
           
            StartCoroutine(ShowAttackLine(attackEffect));
            yield return new WaitForSeconds(attackCooldown);
            _canAttack = true;

            //OnHorizontalCanAttack.Invoke(true);
            PowerAttack = false;
        }

        private void DealDamageInRange(Transform attackCenter, float rangeX, float rangeY, UnityEvent<bool> hitEvent, int damage)
        {
            var enemiesInRange =
                Physics2D.OverlapBoxAll(attackCenter.position, new Vector2(rangeX, rangeY), 0, hitLayer);

            if (enemiesInRange.Length != 0)
            {
                var isStrongAttack = damage > PlayerPreferences.HitDamage;
                hitEvent.Invoke(isStrongAttack);
            }

            foreach (var enemy in enemiesInRange)
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
        }

        private void CheckStrongAttack(ref int damage, GameObject attackEffect)
        {
            attackEffect.GetComponent<LineRenderer>().startColor = Color.green;
            
            if (_chargeDuration >= chargedAttackTime && _chargeDuration - chargedAttackTime <= timeForKeyUp)
            {
                damage *= 5;
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