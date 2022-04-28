using System.Collections;
using Movement;
using UnityEngine;

namespace DefaultNamespace.Fight
{
    public class UpAbility : Ability
    {
        [SerializeField] private float flyUpSpeed;
        [SerializeField] private float flyTime;
        [SerializeField] private float inAirHeightDeceleration;
        [SerializeField] private float spawnTrailOverTime;
        [SerializeField] private float powerAttackCooldown;

        private void Awake()
        {
            PlayerInGameInput.OnUpAbility.AddListener(() => StartCoroutine(Cast()));
        }

        protected override IEnumerator Cast()
        {
            if (!_canUse || !PlayerPreferences.UpAbilityAvailable)
                yield break;
            PlayerInGameInput.OnAbility.Invoke();
            _canUse = false;

            var flyCoroutine = GetComponent<PlayerController>().FlyUp(flyUpSpeed, flyTime, inAirHeightDeceleration);
            StartCoroutine(flyCoroutine);
            var duration = 0f;
            while (flyCoroutine.MoveNext())
            {
                duration += Time.deltaTime;
                if (duration - spawnTrailOverTime > 0)
                {
                    duration -= spawnTrailOverTime;
                    Instantiate(prefab, transform.position, Quaternion.identity);
                }
                yield return null;
            }

            StartCoroutine(StartPowerAttackCooldown());
            
            StartCoroutine(StartCooldown());
        }

        private IEnumerator StartPowerAttackCooldown()
        {
            GetComponent<PlayerAttack>().PowerAttack = true;
            yield return new WaitForSeconds(powerAttackCooldown);
            GetComponent<PlayerAttack>().PowerAttack = false;
        }
    }
}