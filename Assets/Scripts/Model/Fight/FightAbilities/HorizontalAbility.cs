using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace.Fight
{
    public class HorizontalAbility : Ability
    {
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float maxDistance;

        private void Awake()
        {
            PlayerInGameInput.OnHorizontalAbility.AddListener(() => StartCoroutine(Cast()));
        }

        protected override IEnumerator Cast()
        {
            if (!_canUse || !PlayerPreferences.HorizontalAbilityAvailable)
                yield break;
            PlayerInGameInput.OnAbility.Invoke();
            _canUse = false;
            PlayerPreferences.CanMove = false;
            yield return new WaitForSeconds(timeForCast);
            PlayerPreferences.CanMove = true;
            
            StartCoroutine(StartCooldown());
            
            var missile = GameObject.Instantiate(prefab, transform.position + new Vector3(PlayerPreferences.LookDirection, 0.5f), Quaternion.identity);
            var direction = PlayerPreferences.LookDirection;
            var startPosition = missile.transform.position;
            
            while (missile != null)
            {
                if (Vector2.Distance(startPosition, missile.transform.position) >= maxDistance)
                    Destroy(missile);
                else
                    missile.transform.position += new Vector3(direction, 0) * projectileSpeed * Time.deltaTime;
                
                yield return new WaitForFixedUpdate();
            }

            yield break;
        }
    }
}