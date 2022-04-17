using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace.Fight
{
    public class DownAbility : Ability
    {
        [SerializeField] private float duration;

        private void Awake()
        {
            PlayerInGameInput.OnDownAbility.AddListener(() => StartCoroutine(Cast()));
        }

        protected override IEnumerator Cast()
        {
            if (!_canUse)
                yield break;
            PlayerInGameInput.OnAbility.Invoke();

            prefab.SetActive(true);
            PlayerPreferences.CanMove = false;
            
            yield return new WaitForSeconds(duration);

            PlayerPreferences.CanMove = true;
            prefab.SetActive(false);
            StartCoroutine(StartCooldown());
        }
    }
}