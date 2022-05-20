using System.Collections;
using Movement;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Fight
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerHealth : MonoBehaviour//, IDamageble
    {
        [SerializeField] private float timeStopDuration;
        [SerializeField] private float damageImmunityDuration;
        [SerializeField] private float healDuration;
        [SerializeField] private ParticleSystem healParticles;
        [SerializeField] private ParticleSystem hitParticles;

        private ParticleSystem _particles;
        private Coroutine _healingCoroutine;

        public static UnityEvent OnHeal = new UnityEvent();
        public static UnityEvent<int> OnHitTaken = new UnityEvent<int>();
        public static UnityEvent<int> OnDamageTaken = new UnityEvent<int>();

        public static UnityEvent OnHealStart = new UnityEvent();
        public static UnityEvent OnHealEnd = new UnityEvent();
        public static UnityEvent OnDie = new UnityEvent();
        public static UnityEvent OnRespawn = new UnityEvent();
        
        private void Awake()
        {
            OnHitTaken.AddListener((value) =>
            {
                if (!PlayerPreferences.CanTakeDamage)
                    return;
                StartCoroutine(TakeDamage(value));
                OnDamageTaken.Invoke(value);
            });
            
            PlayerInGameInput.OnHealDown.AddListener(() =>
            {
                if (!PlayerPreferences.IsGrounded)
                    return;
                _healingCoroutine = StartCoroutine(Heal());
            });
            
            PlayerInGameInput.OnHealUp.AddListener(() =>
            {
                StopHeal();
            });
            PlayerInGameInput.OnAbility.AddListener(() => SpendBlood());
            OnHeal.AddListener(SpendBlood);
            PlayerAttack.OnHit.AddListener(AddBlood);
        }
        
        public IEnumerator TakeDamage(int value = 1)
        {
            Instantiate(hitParticles, transform.position, Quaternion.identity);
            StopHeal();
            
            PlayerPreferences.CanTakeDamage = false;
            
            PlayerPreferences.CurrentHealth -= value;
            if (PlayerPreferences.CurrentHealth == 0)
            {
                Die();
                yield break;
            }

            StartCoroutine(Utils.StopTimeForWhile(timeStopDuration));
            GetComponent<PlayerController>().Bounce(new Vector2(PlayerPreferences.FaceRight ? -1 : 1, 0.3f) * 10);

            PlayerPreferences.DisableControl();

            yield return new WaitForSeconds(0.1f);
            
            PlayerPreferences.EnableControl();
            
            yield return new WaitForSeconds(damageImmunityDuration);
            PlayerPreferences.CanTakeDamage = true;
        }

        public void Die()
        {
            OnDie.Invoke();
        }

        private IEnumerator Heal()
        {
            OnHealStart.Invoke();
            _particles = Instantiate(healParticles, transform.position, Quaternion.identity);
            PlayerPreferences.CanMove = false;
            yield return new WaitForSeconds(healDuration);
            OnHeal.Invoke();
            OnHealEnd.Invoke();
            PlayerPreferences.CurrentHealth += 1;
            PlayerPreferences.CanMove = true;
        }

        private void StopHeal()
        {
            if (_healingCoroutine == null)
                return;
            OnHealEnd.Invoke();
            StopCoroutine(_healingCoroutine);
            PlayerPreferences.CanMove = true;
            if (_particles != null)
                _particles.Stop();
        }

        private void SpendBlood()
        {
            PlayerPreferences.CurrentBlood -= PlayerPreferences.BloodSpend;
        }

        private void AddBlood(float value)
        {
            PlayerPreferences.CurrentBlood += value;
        }
    }
}