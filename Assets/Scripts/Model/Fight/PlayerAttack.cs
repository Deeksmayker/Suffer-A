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
        public static UnityEvent<float> OnHit = new UnityEvent<float>();
        public static UnityEvent OnStrongHit = new UnityEvent();
        public static UnityEvent OnVerticalCanAttack = new UnityEvent();

        public static UnityEvent OnSimpleAttack = new UnityEvent();
        public static UnityEvent OnStrongAttack = new UnityEvent();
        public static UnityEvent OnFailedStrong = new UnityEvent();
        
        public static UnityEvent OnSimpleHorizontal = new UnityEvent();
        public static UnityEvent OnStrongHorizontal = new UnityEvent();
        public static UnityEvent OnFailedHorizontal = new UnityEvent();
        
        public static UnityEvent OnSimpleUp = new UnityEvent();
        public static UnityEvent OnStrongUp = new UnityEvent();
        public static UnityEvent OnFailedUp = new UnityEvent();
        
        public static UnityEvent OnSimpleDown = new UnityEvent();
        public static UnityEvent OnStrongDown = new UnityEvent();
        public static UnityEvent OnFailedDown = new UnityEvent();

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

        public static Vector2 AttackDirection = Vector2.right;

        public static UnityEvent<bool> OnHorizontalCanAttack = new UnityEvent<bool>();

        private Coroutine _chargeAttackCoroutine;

        private void Awake()
        {
            chargedPreparingAudio = Instantiate(chargedPreparingAudio, preparedChargedCenter);
            preparedChargedAudio = Instantiate(preparedChargedAudio, preparedChargedCenter);
            
            PlayerInGameInput.OnAttackKeyDown.AddListener(() => _chargeAttackCoroutine = StartCoroutine(ChargeAttack()));
            PlayerInGameInput.OnAttackKeyUp.AddListener(() =>
            {
                if (_chargeAttackCoroutine != null)
                    StopCoroutine(_chargeAttackCoroutine);
                DecideAttackDirection();
            });
        }

        private IEnumerator ChargeAttack()
        {
            if (!PlayerPreferences.AttackAvailable)
                yield break;
            
            chargedPreparingAudio.Play();
            
            var flag = true;
            
            while (_chargeDuration < maxChargeTime)
            {
                if (_chargeDuration >= chargedAttackTime && _chargeDuration - chargedAttackTime <= timeForKeyUp && flag)
                {
                    ParticlesEffects.StartParticle(preparedChargedParticle, preparedChargedCenter);
                    preparedChargedAudio.Play();
                    flag = false;
                }
                _chargeDuration += Time.deltaTime;
                yield return null;
            }

            DecideAttackDirection();
        }

        private void DecideAttackDirection()
        {
            if (!PlayerPreferences.AttackAvailable)
                return;
            
            if (_chargeDuration < chargedAttackTime)
                chargedPreparingAudio.Stop();
            
            if (PlayerInGameInput.VerticalRaw == 0 ||
                PlayerInGameInput.VerticalRaw == -1 && PlayerPreferences.IsGrounded)
            {
                AttackDirection = PlayerPreferences.FaceRight ? Vector2.right : Vector2.left;
                
                var x = PowerAttack ? horizontalRangeX * 2 : horizontalRangeX;
                var y = PowerAttack ? horizontalRangeY * 2 : horizontalRangeY;
                var line = PowerAttack ? bigHorizontalAttackLine : horizontalAttackLine;
                StartCoroutine(Attack(x, y, horizontalAttackCenter,
                    line, OnEnemyHorizontalHit));
            }
            else
            {
                var isUpAttack = PlayerInGameInput.VerticalRaw == 1;
                AttackDirection = isUpAttack ? Vector2.up : Vector2.down;
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
                OnHit.Invoke(damage > PlayerPreferences.HitDamage ? damage*3.35f : damage * 5);
                if (damage > PlayerPreferences.HitDamage)
                    OnStrongHit.Invoke();
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
                OnStrongAttack.Invoke();
                
                if (IsHorizontal())
                    OnStrongHorizontal.Invoke();

                else
                {
                    if (IsUp())
                        OnStrongUp.Invoke();

                    else
                        OnStrongDown.Invoke();
                }
            }

            else
            {
                if (_chargeDuration < chargedAttackTime)
                {
                    OnSimpleAttack.Invoke();
                    
                    if (IsHorizontal())
                        OnSimpleHorizontal.Invoke();
                    else
                    {
                        if (IsUp())
                            OnSimpleUp.Invoke();
                        else
                            OnSimpleDown.Invoke();
                    }
                }
                else
                {
                    OnFailedStrong.Invoke();
                    
                    if (IsHorizontal())
                        OnFailedHorizontal.Invoke();
                    else
                    {
                        if (IsUp())
                            OnFailedUp.Invoke();
                        else
                            OnFailedDown.Invoke();
                    }
                }
            }
        }

        private bool IsHorizontal() => PlayerInGameInput.VerticalRaw == 0 ||
                                       PlayerInGameInput.VerticalRaw == -1 && PlayerPreferences.IsGrounded;

        private bool IsUp() => PlayerInGameInput.VerticalRaw == 1;
        
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